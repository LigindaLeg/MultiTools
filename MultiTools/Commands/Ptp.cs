using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Ptp : ICommand
    {
        public string Command { get; } = "ptp";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Teleports Player to Player";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player p1;
            Player p2;
            if (!sender.CheckPermission("mt.ptp"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            else if (arguments.Count != 2)
            {
                response = "Using: ptp (ID) (ID)";
                return false;
            }
            else if (sender.CheckPermission("mt.ptp") && arguments.Count == 2)
            {
                p1 = Player.Get(Convert.ToInt32(arguments.At(0)));
                p2 = Player.Get(Convert.ToInt32(arguments.At(1)));
                p1.Teleport(p2);
                response = "Succesfully teleported!";
                return true;
            }
            else
            {
                response = "Using: ptp (ID) (ID)";
                return false;
            }
        }
    }
}
