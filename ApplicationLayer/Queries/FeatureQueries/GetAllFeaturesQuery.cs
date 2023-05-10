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

namespace PlanningPokerWebAPI.ApplicationLayer.Features.Queries
{
    public class GetAllFeaturesQuery : IRequest<IEnumerable<FeatureReadDto>>
    {
        public int Id { get; }
    }

    // handler - business logic for executing query
    public class GetAllFeaturesQueryHandler : IRequestHandler<GetAllFeaturesQuery, IEnumerable<FeatureReadDto>>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        // use IDbContext or similar i/f to instead of exposing Infrastructure layer class
        public GetAllFeaturesQueryHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<FeatureReadDto>> Handle(GetAllFeaturesQuery request, CancellationToken cancellationToken)
        {
            return context.Features.ProjectTo<FeatureReadDto>(mapper.ConfigurationProvider);
        }
    }

    // Mediator - thing that triggers the action/handler 

}
