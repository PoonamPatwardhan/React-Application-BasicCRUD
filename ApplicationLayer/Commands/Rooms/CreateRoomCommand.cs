using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Commands.Rooms
{
    public class CreateRoomCommand : IRequest<Room>
    {
        public Room newRoomName { get; }
        public User adminUserId { get; }

        public UserManager<User> userManager { get; }

        public CreateRoomCommand(Room newRoom, User adminUser)
        {
            this.newRoomName = newRoom;
            this.adminUserId = adminUser;
            //this.userManager = userManager;
        }
    }

    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Room>
    {
        private readonly PokerAppDbContext context;
        //private readonly IMapper mapper;

        public CreateRoomCommandHandler(PokerAppDbContext context)
        {
            this.context = context;
        }
        public async Task<Room> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var newRoom = new Room()
                {
                    TeamName = request.newRoomName.TeamName
                };               

                //if (newRoom.TeamMembers == null)
                //    newRoom.TeamMembers = new List<User>();
                //newRoom.TeamMembers.Add(request.adminUser);
                //if (request.adminUser.Teams == null)
                //    request.adminUser.Teams = new List<Room>();
                //request.adminUser.Teams.Add(newRoom);
                var userId = request.adminUserId;
                var userFromDb = context.ApplicationUsers.FirstOrDefault(x => x.Id == userId.Id);
                newRoom.Admin = userFromDb;
                //var userFromDb = context.ApplicationUsers.Find(user);
                if (userFromDb.Teams == null)
                    userFromDb.Teams = new List<Room>();
                userFromDb.Teams.Add(newRoom);
                //context.UserRooms.Add(new UsersInTeams() { User = userFromDb, Team = newRoom });

                context.Rooms.Add(newRoom);

                await context.SaveChangesAsync(cancellationToken); // if await is not used, execution will continue without waiting for this action to complete

                return newRoom;
            }
            catch(Exception ex)
            {
                Trace.WriteLine(ex.Message);
                
            }
            return null;
            //return mapper.Map<FeatureReadDto>(newFeature);
        }
    }
}

