using Discord.Interactions;
using Discord;

namespace Bot1.App.Modals
{
    public class CreateServerModal : IModal
    {
        public string Title => "Create Server";

        [InputLabel("Town")]
        [ModalTextInput("server_id", TextInputStyle.Short, placeholder: "Enter Town Id", maxLength: 50)]
        public string ServerId { get; set; }

        [InputLabel("Town Password")]
        [ModalTextInput("server_pass", TextInputStyle.Short, placeholder: "Enter Password", maxLength: 50)]
        public string ServerPassword { get; set; }

        [InputLabel("Host")]
        [RequiredInput(false)]
        [ModalTextInput("host", TextInputStyle.Short, placeholder: "Who made the town? (optional)", maxLength: 50 )]
        public string? Host { get; set; }

        [InputLabel("Expires At (hours)")]
        [ModalTextInput("server_expires", TextInputStyle.Short)]
        public string ExpiresAtHoursStr { get; set; }
    }
}
