using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlanningPokerWebAPI.Models
{
    public class Feature
    {
        //[Key] no need, EF Core will automatically consider ID field as being primary
        public int Id { get; set; }

        //[Required]
        public int FeatureId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public int Priority { get; set; }

        public int StoryPoints { get; set; }
    }

    public enum PriorityLevel
    {
        None = 0,
        Low = 1,
        Medium = 2,
        High = 3
    }
}
