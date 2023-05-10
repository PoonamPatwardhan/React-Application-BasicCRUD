using AutoMapper;

namespace PlanningPokerWebAPI.ApplicationLayer.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
