using MediatR;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Commands.Rooms
{  
    public class AddUsersToRoomCommand : IRequest
    {
        public Room room { get; }
        public List<User> users { get; }

        public AddUsersToRoomCommand(Room room, List<User> users)
        {
            this.room = room;
            this.users = users;
        }
    }

    public class AddUsersToRoomCommandHandler : IRequestHandler<AddUsersToRoomCommand>
    {
        private readonly PokerAppDbContext context;
        //private readonly IMapper mapper;

        public AddUsersToRoomCommandHandler(PokerAppDbContext context)
        {
            this.context = context;
        }

        public async Task<Unit> Handle(AddUsersToRoomCommand request, CancellationToken cancellationToken)
        {
            var room = context.Rooms.FirstOrDefault( room => room.RoomId == request.room.RoomId);
            if (room.TeamMembers == null)
                room.TeamMembers = new List<User>();
            room.TeamMembers?.ToList()?.AddRange(request.users);                      

            await context.SaveChangesAsync(cancellationToken); // if await is not used, execution will continue without waiting for this action to complete
            return Unit.Value;
        }
    }
}
