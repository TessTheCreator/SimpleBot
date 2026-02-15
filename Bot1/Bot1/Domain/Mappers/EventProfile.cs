using Bot1.Domain.Models;
using AutoMapper;
using Bot1.App.Models;
using Bot1.Data.Entities;

namespace Bot1.Domain.Mappers
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<EventModel, EventEntity>();
            CreateMap<EventEntity, EventModel>();
        }
    }
}
