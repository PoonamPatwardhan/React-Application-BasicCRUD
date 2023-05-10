using AutoMapper;
using MediatR;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Features.Queries
{
    public class GetFeatureByIdQuery : IRequest<FeatureReadDto>
    {
        public int FeatureId { get; }
        public GetFeatureByIdQuery(int id)
        {
            FeatureId = id;
        }        
    }

    // handler - business logic for executing query
    public class GetFeatureByIdQueryHandler : IRequestHandler<GetFeatureByIdQuery, FeatureReadDto>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        // use IDbContext or similar i/f to instead of exposing Infrastructure layer class
        public GetFeatureByIdQueryHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<FeatureReadDto> Handle(GetFeatureByIdQuery request, CancellationToken cancellationToken)
        {
            var featureWithGivenId = context.Features.FirstOrDefault(x => x.FeatureId == request.FeatureId);
            return mapper.Map<FeatureReadDto>(featureWithGivenId);
        }
    }
}

