using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class BlockDoors : ICommand
    {
        public string Command { get; } = "blockdoors";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Block the used door.";

        Player BlockDoorUser;
        static int index;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("mt.blockdoor"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            if (arguments.Count == 0 || arguments.Count > 1)
            {
                response = "Usage: blockdoors (ID)";
                return false;
            }

            BlockDoorUser = Player.Get(Convert.ToInt32(arguments.At(0)));

            if (BlockDoorUser == null)
            {
                response = $"Player with ID {arguments.At(0)} not found";
                return false;
            }
            
            if (BlockDoorUser != null)
            {
                
                if (Plugin.Instance.BlockDoorList == BlockDoorUser)
                {
                    Plugin.Instance.BlockDoorList = null;
                    response = $"New block door status: off\nPlayer: {BlockDoorUser.Nickname}";
                    return true;
                }
                if (Plugin.Instance.BlockDoorList != BlockDoorUser)
                {
                    Plugin.Instance.BlockDoorList = BlockDoorUser;
                    response = $"New block door status: on\nPlayer: {BlockDoorUser.Nickname}";
                    return true;
                }
            }
            response = "Usage: blockdoors (ID)";
            return false;
        }
    }
}
