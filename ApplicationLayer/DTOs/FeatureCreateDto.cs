using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using PlanningPokerWebAPI.ApplicationLayer.Mappings;
using PlanningPokerWebAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.ApplicationLayer.DTOs
{
    public class FeatureCreateDto : IMapFrom<FeatureCreateDto>
    {
        [Required]
        public int FeatureId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        [Required]
        public int Priority { get; set; }

        public int StoryPoints { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(FeatureCreateDto), typeof(Feature));
        }
    }
}
