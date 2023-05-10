using MediatR;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Queries.RoomQueries
{
    public class GetRoomById : IRequest<Room>
    {
        public int RoomId { get; }
        public GetRoomById(int id)
        {
            RoomId = id;
        }
    }

    // handler - business logic for executing query
    public class GetRoomByIdQueryHandler : IRequestHandler<GetRoomById, Room>
    {
        private readonly PokerAppDbContext context;
        //private readonly IMapper mapper;

        // use IDbContext or similar i/f to instead of exposing Infrastructure layer class
        public GetRoomByIdQueryHandler(PokerAppDbContext context)
        {
            this.context = context;
            //this.mapper = mapper;
        }

        public async Task<Room> Handle(GetRoomById request, CancellationToken cancellationToken)
        {
            var roomWithGivenId = context.Rooms.FirstOrDefault(x => x.RoomId == request.RoomId);
            return roomWithGivenId;
        }
    }
}
