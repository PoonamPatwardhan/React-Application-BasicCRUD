using AutoMapper;
using MediatR;
using PlanningPokerWebAPI.ApplicationLayer.DTOs;
using PlanningPokerWebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.Features.Commands
{
    public class UpdateFeatureCommand : IRequest
    {
        public int FeatureId { get; }
        public FeatureUpdateDto featureUpdateDto { get; }

        public UpdateFeatureCommand(int id ,FeatureUpdateDto featureToUpdate)
        {
            FeatureId = id;
            featureUpdateDto = featureToUpdate;
        }
    }

    public class UpdateFeatureCommandHandler : IRequestHandler<UpdateFeatureCommand>
    {
        private readonly PokerAppDbContext context;
        private readonly IMapper mapper;

        public UpdateFeatureCommandHandler(PokerAppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }        

        public async Task<Unit> Handle(UpdateFeatureCommand request, CancellationToken cancellationToken)
        {
            var featureFromDb = context.Features.FirstOrDefault(x => x.FeatureId == request.FeatureId);

            mapper.Map(request.featureUpdateDto, featureFromDb);

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;

        }
    }   
}
