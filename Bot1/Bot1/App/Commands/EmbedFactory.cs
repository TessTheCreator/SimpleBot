using Bot1.App.Modals;
using Bot1.App.Models;
using Bot1.Domain.Models;
using Discord;
using Discord.WebSocket;

namespace MyBot.Services
{
    public static class EmbedFactory
    {
        public static Embed CreateEmbed(string title, string description)
        {
            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(Color.Green);

            return builder.Build();
        }

        public static Embed CreateEmbed(string title, string description, Color color)
        {
            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color);

            return builder.Build();
        }

        public static Embed CreateStandardEmbed(string title, string description, Color color)
        {
            var builder = new EmbedBuilder()
                .WithTitle(title)
                .WithDescription(description)
                .WithColor(color)
                .WithCurrentTimestamp()
                .WithFooter(footer => {
                    footer.Text = "Bot System Notification";
                    footer.IconUrl = "https://example.com/logo.png";
                });

            return builder.Build();
        }

        public static Embed CreateErrorEmbed(string errorMessage)
        {
            return new EmbedBuilder()
                .WithTitle("Error")
                .WithDescription(errorMessage)
                .WithColor(Color.Red)
                .Build();
        }

        public static Embed CreateServerSuccessEmbed(ServerModel model, ulong user)
        {
            return new EmbedBuilder()
                .WithTitle("Town Created")
                .WithDescription($"<@{user}> created a new town")
                .WithColor(Color.Green)

                .AddField("Town ID", $"`{model.ServerId}`", inline: true)
                .AddField("Password", $"`{model.ServerPassword}`", inline: true)
                .AddField("Hosted By", $"`{model.Host}`", inline: true)
                .AddField("Expires", $"{model.ExpiresAtHoursStr} hrs", inline: true)
                .Build();
        }

        public static Embed UpdateServerSuccessEmbed(ServerApiModel model, ulong user)
        {
            return new EmbedBuilder()
                .WithTitle("Town Updated")
                .WithDescription($"<@{user}> updated the town {model.ServerId}")
                .WithColor(Color.Green)

                .AddField("Password", $"`{model.ServerPassword}`", inline: true)
                .AddField("Hosted By", $"`{model.Host}`", inline: true)
                .AddField("Expires", $"{model.ExpiresAtHoursStr} hrs", inline: true)
                .Build();
        }
    }
}
