using AutoMapper;
using Bot1.App.Models;
using Bot1.Domain.Models;

namespace Bot1.App.Mappers
{
    public class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<ServerApiModel, ServerModel>();
        }
    }
}
