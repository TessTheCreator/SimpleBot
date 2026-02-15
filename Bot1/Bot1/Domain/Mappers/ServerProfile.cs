using AutoMapper;
using Bot1.App.Models;
using Bot1.Data.Entities;
using Bot1.Domain.Models;

namespace Bot1.Domain.Mappers
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<ServerModel, ServerEntity>();

            CreateMap<ServerEntity, ServerModel>();
        }
    }
}
