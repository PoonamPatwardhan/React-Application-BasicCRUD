using AutoMapper;
using PlanningPokerWebAPI.ApplicationLayer.Mappings;
using PlanningPokerWebAPI.Models;

namespace PlanningPokerWebAPI.ApplicationLayer.DTOs
{
    public class RoomReadDto : IMapFrom<Room>
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap(typeof(Room), GetType());
        }
    }
}
