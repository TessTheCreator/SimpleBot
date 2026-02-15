using AutoMapper;
using Bot1.Data.Entities;
using Bot1.Data.Interfaces;
using Bot1.Domain.Exceptions;
using Bot1.Domain.Interfaces;
using Bot1.Domain.Models;
using System.Dynamic;
using System.Text;

namespace Bot1.Services
{
    public class ServerService : IServerService
    {
        private readonly IServerRepository _repo;
        private readonly IMapper _mapper;

        public ServerService(IServerRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<string> GetServersAsync(ulong guildId)
        {
            var serverEntities = await _repo.GetServersAsync(guildId);

            if (serverEntities.Count <= 0)
                throw new InvalidServerException();

            var models = _mapper.Map<List<ServerModel>>(serverEntities);
            StringBuilder sb = new StringBuilder();
            foreach (var model in models)
            {
                if (model.ExpiresAt.HasValue)
                {
                    double hoursRemaining = (model.ExpiresAt.Value - DateTime.UtcNow).TotalHours;
                    int wholeHours = (int)hoursRemaining;

                    sb.AppendLine($"=================================" +
                        $"\n **#{model.Id}) |**" +
                        $"\n **Town Id:** \u200b\u200b\u200b\u200b\u200b {model.ServerId}" +
                        $"\n **Password:** \u200b\u200b {model.ServerPassword}" +
                        $"\n **Made By:** \u200b\u200b\u200b\u200b\u200b {model.Host} " +
                        $"\n (Expires: In {wholeHours}hrs) \n");
                }
                else
                {
                    sb.AppendLine($"=================================" +
                        $"\n **#{model.Id}) |** " +
                        $"\n **Town Id:** \u200b\u200b\u200b\u200b\u200b {model.ServerId}" +
                        $"\n **Password:** \u200b\u200b {model.ServerPassword}" +
                        $"\n **Made By:** \u200b\u200b\u200b\u200b\u200b {model.Host}" +
                        $"\n (Expires: Unknown) \n ");
                }
            }
            return sb.ToString();
        }

        public async Task<string> GetAllServers()
        {
            var serverEntities = await _repo.GetAllAsync();

            if (serverEntities.Count <= 0)
                throw new InvalidServerException();

            var models = _mapper.Map<List<ServerModel>>(serverEntities);
            StringBuilder sb = new StringBuilder();
            foreach (var model in models)
            {
                sb.AppendLine($"=================================" +
                    $"\n **#{model.Id}) |**" +
                    $"\n **Town Id:** {model.ServerId}" +
                    $"\n **Password:** {model.ServerPassword}" +
                    $"\n **Made By:** {model.Host} " +
                    $"\n (Expires: {model.ExpiresAt}) \n)" +
                    $"\n (Created: {model.CreatedAt} \n)" +
                    $"\n (GuildId: {model.GuildId} \n");
            }
            return sb.ToString();
        }

        public async Task<ServerModel> GetServerAsync(ulong guildId, ulong userId, int rowId)
        {
            var serverEntity = await _repo.GetServerAsync(rowId, guildId, userId);
            if (serverEntity == null)
                throw new InvalidServerException();
            var model = _mapper.Map<ServerModel>(serverEntity);
            return model;
        }

        public async Task<ServerModel> CreateServerAsync(ServerModel server, bool isAdmin)
        {
            var serverWithTime = UpdateHours(server);
            var serverEntity = _mapper.Map<ServerEntity>(serverWithTime);
            await _repo.AddAsync(serverEntity);
            return serverWithTime;
        }

        public async Task RemoveServerAsync(int rowId, ulong userId, ulong guildId, bool isAdmin)
        {
            var server = await _repo.GetServerAsync(rowId, guildId, userId);

            if (server == null)
                throw new InvalidServerException();

            await _repo.DeleteAsync(server);
        }

        public async Task UpdateServerAsync(ServerModel server, bool isAdmin)
        {
            var serverEntity = _mapper.Map<ServerEntity>(server);
            double hours = string.IsNullOrEmpty(server.ExpiresAtHoursStr) ? 24.0 : Convert.ToDouble(server.ExpiresAtHoursStr);
            serverEntity.ExpiresAt = DateTime.UtcNow.AddHours(hours);
            await _repo.UpdateAsync(serverEntity);
        }

        public async Task CleanupExpiredEntriesAsync()
        {
            await _repo.DeleteOutdatedServers();
        }

        private ServerModel UpdateHours(ServerModel server)
        {
            var expStr = server.ExpiresAtHoursStr;
            if (string.IsNullOrEmpty(expStr))
            {
                server.ExpiresAt = DateTime.UtcNow.AddHours(24.0);
                server.ExpiresAtHoursStr = "24.0";
                return server;
            }
            if (!double.TryParse(expStr, out double hours))
            {
                server.ExpiresAt = DateTime.UtcNow.AddHours(24.0);
                server.ExpiresAtHoursStr = "24.0";
                return server;
            }

            server.ExpiresAt = DateTime.UtcNow.AddHours(Convert.ToDouble(expStr));
            return server;
        }
    }
}
