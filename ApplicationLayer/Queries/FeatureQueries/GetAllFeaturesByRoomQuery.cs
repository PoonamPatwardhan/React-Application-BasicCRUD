using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Queries.FeatureQueries
{
    public class GetAllFeaturesByRoom : IRequest<IEnumerable<FeatureReadDto>>
    {
        public Room room { get; }
        public GetAllFeaturesByRoom(Room room)
        {
            this.room = room;
        }
    }

    // handler - business logic for executing query
    public class GetAllFeaturesByRoomQueryHandler : IRequestHandler<GetAllFeaturesByRoom, IEnumerable<FeatureReadDto>>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        // use IDbContext or similar i/f to instead of exposing Infrastructure layer class
        public GetAllFeaturesByRoomQueryHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FeatureReadDto>> Handle(GetAllFeaturesByRoom request, CancellationToken cancellationToken)
        {
            //var roomsForGivenUser = context.Rooms.
            //                  Where(x => x.Admin.UserName == request.userName);

            var room = context.Rooms.Include(x => x.Features).FirstOrDefault(x => x.TeamName == request.room.TeamName);
            var featuresForGivenRoom = room.Features;
            return (featuresForGivenRoom as IQueryable<Feature>).ProjectTo<FeatureReadDto>(mapper.ConfigurationProvider);
        }
    }
}
