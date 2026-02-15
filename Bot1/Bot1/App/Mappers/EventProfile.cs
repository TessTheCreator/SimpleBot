using Bot1.Domain.Models;
using AutoMapper;
using Bot1.App.Models;

namespace Bot1.App.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventApiModel, EventModel>();
        }
    }
}
