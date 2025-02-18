using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using YamlDotNet.Core.Tokens;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class CustomPrefix : ICommand
    {
        public string Command { get; } = "customprefix";
        public string[] Aliases { get; } = new[] { "cp", "cprefix", "customp" };
        public string Description { get; } = "Set custom prefix to player";

        public string[] Usage { get; } = new string[] { "%player%", "Color", "Text" };
        private static readonly string FilePath = $@"{Paths.Plugins}/MultiTools/{Server.Port}/Prefix.txt";
        public List<string> Colors { get; } = new List<string>() 
        { 
            "red", "pink", "brown", "silver", "light_green", "crimson", "cyan", "aqua", "deep_pink", "tomato", "yellow", "magenta", "blue_green", "orange", "lime", "green", "emerald", "carmine", "nickel", "mint", "army_green", "pumpkin", "gold", "teal", "purple", "light_red", "silver_blue", "police_blue"
        };

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            
            if (!sender.CheckPermission("mt.customprefix"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            
            else if (arguments.Count < 3 && arguments.At(0) != "clear" || (arguments.At(0) == "clear" && arguments.Count < 2))
            {
                response = "Usage: cp (id) (color) (prefix)";
                return false;
            }
            else
            {
                if (arguments.At(0) == "clear")
                {
                    Player player2 = Player.Get(arguments.At(0));
                    if (File.Exists(FilePath))
                    {
                        foreach (var line in File.ReadAllLines(FilePath))
                        {
                            if (line.StartsWith($"{player2.UserId}"))
                            {
                                List<string> lines = File.ReadAllLines(FilePath).ToList();
                                lines.RemoveAll(line1 => line1.Contains($"{player2.UserId}"));
                                File.WriteAllLines(FilePath, lines);
                            }
                        }
                    }
                    response = "Succesfully!";
                    return true;
                }
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
                string text = $"{player.UserId}:{prefix}:{color}";
                if (File.Exists(FilePath))
                {
                    foreach (var line in File.ReadAllLines(FilePath))
                    {
                        if (line.StartsWith($"{player.UserId}"))
                        {
                            List<string> lines = File.ReadAllLines(FilePath).ToList();
                            lines.RemoveAll(line1 => line1.Contains($"{player.UserId}"));
                            File.WriteAllLines(FilePath, lines);
                        }
                    }
                }
                File.AppendAllText(FilePath, text + Environment.NewLine);
                response = "Succesfully change prefix!";
                return true;

            }
        }
    }
}
