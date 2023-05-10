using AutoMapper;
using PlanningPokerWebAPI.ApplicationLayer.Mappings;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.DTOs
{
    public class FeatureUpdateDto : IMapFrom<FeatureUpdateDto>
    {
        [Required]
        public int FeatureId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
        
        [Required]
        public int Priority { get; set; }

        public int StoryPoints { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(FeatureUpdateDto), typeof(Feature));
        }
    }
}
