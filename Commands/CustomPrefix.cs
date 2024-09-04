using System;
using System.Collections.Generic;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CustomPrefix : ICommand
    {
        public string Command { get; } = "customprefix";

        
        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "cp", "cprefix", "customp" };

        /// <inheritdoc/>
        public string Description { get; } = "Set custom prefix to player";

        public List<string> Colors { get; } = new List<string>() 
        { 
            "pink", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin", "gold", "teal", "blue", "purple", "light_red", "silver_blue", "police_blue"
        };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            
            if (!sender.CheckPermission("mt.customprefix"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            
            else if (arguments.Count < 3)
            {
                response = "Using: cp (id) (color) (prefix)";
                return false;
            }
            
            else
            {
                Player player = Player.Get(arguments.At(0));
                string color = arguments.At(1);
                string prefix = "";
                if (player == null)
                {
                    response = $"Player with id {arguments.At(0)} don't found!";
                    return false;
                }
                else if (!Colors.Contains(color))
                {
                    response = $"{color} is not a valid color";
                    return false;
                }
                for (int a = 2; a < arguments.Count; a++)
                {
                    prefix = prefix + arguments.At(a) + " ";
                }
                player.RankName = prefix;
                player.RankColor = color;
                response = "Succesfully change prefix!";
                return true;
            }
        }
    }
}
