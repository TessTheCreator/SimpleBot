namespace Bot1.App.Config.Settings
{
    public class DiscordSettings
    {
        public const string Section = "DiscordSettings";

        public DiscordSettings() { }

        public ulong GuildId { get; set; }
        public ulong ServerRoleId { get; set; }
        public ulong King { get; set; }
    }
}
