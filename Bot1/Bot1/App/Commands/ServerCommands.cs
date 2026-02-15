using Bot1.App.Config.Settings;
using Bot1.App.Modals;
using Bot1.App.Modals.Bot1.App.Modals;
using Bot1.Domain.Exceptions;
using Bot1.Domain.Interfaces;
using Discord.Interactions;
using Discord.WebSocket;
using MyBot.Services;

namespace Bot1.App.Commands
{
    [Group("town", "Manage towns")]
    public class ServerSubCommands : InteractionModuleBase<SocketInteractionContext>
    {
        private readonly IServerService _serverService;
        private ulong _serverAccessRole;

        public ServerSubCommands(IServerService serverService, DiscordSettings settings)
        {
            _serverService = serverService;
            _serverAccessRole = settings.ServerRoleId;
        }

        [SlashCommand("list", "List all currently existing towns")]
        public async Task GetServers()
        {
            await DeferAsync();
            try
            {
                ulong guildId = Context.Guild.Id;
                var servers = await _serverService.GetServersAsync(guildId);

                var embed = EmbedFactory.CreateEmbed("Available Towns List", servers);
                await FollowupAsync(embed:embed);
            }
            catch (InvalidServerException ex)
            {
                Console.WriteLine(ex.Message);
                var embed = EmbedFactory.CreateErrorEmbed("There are no existing towns at the moment");
                await FollowupAsync(embed: embed);
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                var embed = EmbedFactory.CreateErrorEmbed("An error has occurred, sorry");
                await FollowupAsync(embed: embed);
            }
        }

        [SlashCommand("add", "Add a new town")]
        public async Task AddServer()
        {
            if (!IsAuthorized(Context.User, _serverAccessRole))
            {
                var embed = EmbedFactory.CreateErrorEmbed("You are not authorized to create a server!");
                await RespondAsync(embed: embed, ephemeral: true);
            }
            else
            {
                await RespondWithModalAsync<CreateServerModal>("create_server_modal");
            }
        }

        [SlashCommand("delete", "Remove a town")]
        public async Task RemoveServer([Summary("row_id", "Id of the row to remove")] int rowId)
        {
            await DeferAsync();
            if (!IsAuthorized(Context.User, _serverAccessRole))
            {
                var embed = EmbedFactory.CreateErrorEmbed("You are not authorized to delete this town!");
                await FollowupAsync(embed: embed, ephemeral: true);
            }
            else
            {
                try
                {
                    ulong guildId = Context.Guild.Id;
                    ulong userId = Context.User.Id;
                    await _serverService.RemoveServerAsync(rowId, userId, guildId, IsAdmin(Context.User));
                    var embed = EmbedFactory.CreateEmbed("Town Removed", "Town has been removed");
                    await FollowupAsync(embed: embed);
                }
                catch (InvalidServerException ex)
                {
                    Console.WriteLine(ex.Message);
                    var embed = EmbedFactory.CreateErrorEmbed("The town does not exist or you dont have access to delete it!");
                    await FollowupAsync(embed: embed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    var embed = EmbedFactory.CreateErrorEmbed("An error has occured, please try again later");
                    await FollowupAsync(embed: embed);
                }
            }
        }

        [SlashCommand("update", "Update an existing town")]
        public async Task UpdateServer([Summary("row_id", "Row Id")] int rowId)
        {
            try
            {
                ulong guildId = Context.Guild.Id;
                ulong userId = Context.User.Id;

                if (!IsAuthorized(Context.User, _serverAccessRole))
                {
                    var embed = EmbedFactory.CreateErrorEmbed("You are not authorized to update this town!");
                    await RespondAsync(embed: embed, ephemeral: true);
                }
                else
                {
                    var server = await _serverService.GetServerAsync(guildId, userId, rowId);

                    var modal = new UpdateServerModal
                    {
                        ServerId = server.ServerId,
                        ServerPassword = server.ServerPassword,
                        Host = server.Host,
                        ExpiresAtHoursStr = (server.ExpiresAt.Value - DateTime.UtcNow).Hours.ToString(),
                    };

                    await RespondWithModalAsync($"update_server_modal:{rowId}", modal);
                }
            }
            catch(InvalidServerException ex)
            {
                Console.WriteLine(ex.Message);
                var embed = EmbedFactory.CreateErrorEmbed("This town does not exist, or you have no access updating this town");
                await RespondAsync(embed: embed, ephemeral: true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var embed = EmbedFactory.CreateErrorEmbed("An error has occured, please try again or contact the idiot that made this");
                await RespondAsync(embed: embed, ephemeral: true);
            }
        }
         
        private bool IsAuthorized(SocketUser user, ulong accessRole)
        {
            if (user is not SocketGuildUser guildUser)
                return false;

            return guildUser.GuildPermissions.Administrator ||
                   guildUser.Roles.Any(r => r.Id == accessRole);
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




