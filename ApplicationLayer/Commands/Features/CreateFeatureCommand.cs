using AutoMapper;
using MediatR;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Features.Commands
{
    public class CreateFeatureCommand : IRequest<FeatureReadDto>
    {
        public FeatureCreateDto newFeatureDto { get;}
        
        public CreateFeatureCommand(FeatureCreateDto newFeatureDto)
        {
            this.newFeatureDto = newFeatureDto;
        }
    }

    public class CreateFeatureCommandHandler : IRequestHandler<CreateFeatureCommand, FeatureReadDto>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        public CreateFeatureCommandHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<FeatureReadDto> Handle(CreateFeatureCommand request, CancellationToken cancellationToken)
        {            
            var featureCreateDto = request.newFeatureDto;            
            var newFeature = mapper.Map<Feature>(featureCreateDto);
            
            context.Features.Add(newFeature);
            
            await context.SaveChangesAsync(cancellationToken); // if await is not used, execution will continue without waiting for this action to complete

            return mapper.Map<FeatureReadDto>(newFeature);
        }
    }
}
