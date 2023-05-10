using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.InfrastructureLayer.TableRelations;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Queries.UserQueries
{
    public class GetAllRoomsOfUser : IRequest<IEnumerable<Room>>
    {
        public string user { get; }
        public GetAllRoomsOfUser(string user)
        {
            this.user = user;
        }
    }

    // handler - business logic for executing query
    public class GetRoomByIdQueryHandler : IRequestHandler<GetAllRoomsOfUser, IEnumerable<Room>>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        // use IDbContext or similar i/f to instead of exposing Infrastructure layer class
        public GetRoomByIdQueryHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<Room>> Handle(GetAllRoomsOfUser request, CancellationToken cancellationToken)
        {
            //var roomsForGivenUser = context.Rooms.
            //                  Where(x => x.Admin.UserName == request.userName);
            var userWithTeams = context.ApplicationUsers.Include(x => x.Teams).FirstOrDefault(x => x.UserName == request.user);
            //var user = context.ApplicationUsers.FirstOrDefault(x => x.UserName == request.user.UserName);
            var roomsForGivenUser = userWithTeams.Teams;
            return roomsForGivenUser;
            //return (roomsForGivenUser as IQueryable<Room>).ProjectTo<RoomReadDto>(mapper.ConfigurationProvider);
        }
    }
}
