using CommandSystem;
using Exiled.API.Features;
using System;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    public class Call : ICommand
    {
        public string Command { get; } = "call";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Calls admin to teleport to you";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            int admincount = 0;
            foreach (Player admin in Player.List)
            {
                Player player = Player.Get(sender);
                if (admin.RemoteAdminAccess)
                {
                    admin.Broadcast(15, Plugin.Instance.Translation.CallBroadcast.Replace("[player]", player.Nickname));
                    admincount++;
                }
            }
            response = $"Succesfully called {admincount} admins!";
            return true;
        }
    }
}
