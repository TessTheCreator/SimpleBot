
using AutoMapper;
using Bot1.App.Modals;
using Bot1.App.Modals.Bot1.App.Modals;
using Bot1.App.Models;
using Bot1.Domain.Interfaces;
using Bot1.Domain.Models;
using Discord.Interactions;
using Discord.WebSocket;
using MyBot.Services;


namespace Bot1.App.Commands
{
    public class ModalCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private IServerService _serverService;
        private IMapper _mapper;

        public ModalCommands(IServerService serverService, IMapper mapper)
        {
            _serverService = serverService;
            _mapper = mapper;
        }

        [ModalInteraction("create_server_modal")]
        public async Task HandleCreateServerModal(CreateServerModal modal)
        {
            await DeferAsync();
            var server = new ServerApiModel
            {
                ServerId = modal.ServerId,
                ServerPassword = modal.ServerPassword,
                Host = modal.Host,
                MadeBy = Context.User.Id,
                GuildId = Context.Guild.Id,
                ExpiresAtHoursStr = modal.ExpiresAtHoursStr,
                CreatedAt = DateTime.UtcNow
            };

            var model = _mapper.Map<ServerModel>(server);
            if (model.Host == null) model.Host = Context.User.GlobalName;
            var updatedModel = await _serverService.CreateServerAsync(model, IsAdmin(Context.User));

            var embed = EmbedFactory.CreateServerSuccessEmbed(updatedModel, Context.User.Id);
            var embed1 = EmbedFactory.CreateEmbed("Added Town", $"Town {modal.ServerId} Added");
            await FollowupAsync(embed:embed);
        }

        [ModalInteraction("update_server_modal:*")]
        public async Task HandleUpdateServerModal(int rowId, UpdateServerModal modal)
        {
            try
            {
                await DeferAsync();

                var server = new ServerApiModel
                {
                    ServerId = modal.ServerId,
                    ServerPassword = modal.ServerPassword,
                    Host = modal.Host,
                    MadeBy = Context.User.Id,
                    GuildId = Context.Guild.Id,
                    ExpiresAtHoursStr = modal.ExpiresAtHoursStr,
                };

                var model = _mapper.Map<ServerModel>(server);
                model.Id = rowId;
                await _serverService.UpdateServerAsync(model, IsAdmin(Context.User));

                var embed = EmbedFactory.UpdateServerSuccessEmbed(server, Context.User.Id);
                var embed1 = EmbedFactory.CreateEmbed("Updated Town", $"Town {modal.ServerId} has been updated!");
                await FollowupAsync(embed: embed);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Exception: {ex}");
                var embed = EmbedFactory.CreateErrorEmbed("An error has occured, please try again or contact the idiot that made this");
                await RespondAsync(embed: embed, ephemeral: true);
            }
        }

        private bool IsAdmin(SocketUser user)
        {
            if (user is SocketGuildUser guildUser)
            {
                return guildUser.GuildPermissions.Administrator;
            }
            return false;
        }

    }
}

