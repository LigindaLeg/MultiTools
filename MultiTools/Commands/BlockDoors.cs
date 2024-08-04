using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using System.Linq;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class BlockDoors : ICommand
    {
        public string Command { get; } = "blockdoors";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Block the used door.";

        Player BlockDoorUser;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            

            if (!sender.CheckPermission("mt.blockdoor"))
            {
                response = "You do not have permission to use this command";
                return false;
            }

            else if (arguments.Count == 0 || arguments.Count > 1)
            {
                response = "Usage: blockdoors (ID)";
                return false;
            }

            BlockDoorUser = Player.Get(arguments.ElementAt(0));

            if (BlockDoorUser == null)
            {
                response = $"Player with ID {arguments.At(0)} not found";
                return false;
            }
            
            else if (BlockDoorUser != null && arguments.Count == 1)
            {
                Log.Info(BlockDoorUser);
                if (Plugin.Instance.BlockDoorList.Contains(BlockDoorUser))
                {
                    Log.Info(BlockDoorUser + " listed");
                    Plugin.Instance.BlockDoorList.Remove(BlockDoorUser);
                    response = $"New block door status: off\nPlayer: {BlockDoorUser.Nickname}";
                    return true;
                }
                else if (!Plugin.Instance.BlockDoorList.Contains(BlockDoorUser))
                {
                    Log.Info(BlockDoorUser + " not listed");
                    Plugin.Instance.BlockDoorList.Add(BlockDoorUser);
                    response = $"New block door status: on\nPlayer: {BlockDoorUser.Nickname}";
                    return true;
                }
            }
            response = "Usage: blockdoors (ID)";
            return false;
        }
    }
}
