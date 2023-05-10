using AutoMapper;
using PlanningPokerWebAPI.ApplicationLayer.Mappings;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.DTOs
{
    public class FeatureReadDto : IMapFrom<Feature>
    {        
        public int FeatureId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Feature), GetType());
        }
    }
}
