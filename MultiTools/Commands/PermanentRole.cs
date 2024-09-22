using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class PermanentRole : ICommand
    {
        public string Command { get; } = "permrole";

        public string[] Aliases { get; } = new[] { "pr", "permanentrole" };

        public string Description { get; } = "Giving permanent role to player";

        Player Admin;
        Player player1;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Admin = Player.Get(sender);
            if (!sender.CheckPermission("mt.permanentrole"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            int playerID = Convert.ToInt32(arguments.At(0));
            player1 = Player.Get(playerID);
            string role = arguments.At(1);
            if (player1 == null)
            {
                response = $"Player with ID {arguments.At(0)} not found";
                return false;
            }
            else if (arguments.Count < 2)
            {
                response = "Using: pr (ID) (role)";
                return false;
            }
            else if (!ServerStatic.GetPermissionsHandler().GetAllGroupsNames().Contains(role))
            {
                response = $"Group with name {role} not found";
                return false;
            }
            else if (sender.CheckPermission("mt.permanentrole") && player1 != null)
            {
                ServerStatic.RolesConfig.SetStringDictionaryItem("Members", player1.UserId, role);
                ServerStatic.PermissionsHandler = new PermissionsHandler(ref ServerStatic.RolesConfig, ref ServerStatic.SharedGroupsConfig, ref ServerStatic.SharedGroupsMembersConfig);
                response = $"Group permitted to player {player1.Nickname}";
                return false;
            }
            else
            {
                response = "Using: pr (ID) (role)";
                return false;
            }
        }
    }
}
