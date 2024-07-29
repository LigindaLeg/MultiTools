using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using UnityEngine;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Reverse : ICommand
    {
        public string Command { get; } = "reverse";

        public string[] Aliases { get; } = {};

        public string Description { get; } = "Reverse player.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!sender.CheckPermission("mt.reverse"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            else if (arguments.Count < 1)
            {
                if (player.Scale != new Vector3(-1, -1, -1))
                {
                    player.Scale = new Vector3(-1, -1, -1);
                    response = "Reversed!";
                    return true;
                }
                else if (player.Scale == new Vector3(-1, -1, -1))
                {
                    player.Scale = new Vector3(1, 1, 1);
                    response = "Reversed!";
                    return true;
                }
            }
            else if (arguments.At(0) == player.Id.ToString())
            {
                if (player.Scale != new Vector3(player.Scale.x, -1, player.Scale.z))
                {
                    player.Scale = new Vector3(player.Scale.x, -1, player.Scale.z);
                    response = "Reversed!";
                    return true;
                }
                else if (player.Scale == new Vector3(player.Scale.x, -1, player.Scale.z))
                {
                    player.Scale = new Vector3(player.Scale.x, 1, player.Scale.z);
                    response = "Reversed!";
                    return true;
                }
            }
            else if (arguments.At(0) != player.Id.ToString())
            {
                player = Player.Get(arguments.At(0));
                if (player == null)
                {
                    response = $"Player with ID {arguments.At(0)} not found";
                    return false;
                }
                else if (player.Scale != new Vector3(player.Scale.x, -1, player.Scale.z))
                {
                    player.Scale = new Vector3 (player.Scale.x, -1, player.Scale.z);
                    response = $"Reversed {player.Nickname}!";
                    return true;
                }
                else if (player.Scale == new Vector3(player.Scale.x, -1, player.Scale.z))
                {
                    player.Scale = new Vector3(player.Scale.x, 1, player.Scale.z);
                    response = $"Reversed {player.Nickname}!";
                    return true;
                }
            }
            response = "Using: reverse [ID]";
            return true;
        }
    }
}
