using System;
using System.Collections.Generic;
using System.IO;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Check : ICommand
    {
        public string Command { get; } = "p";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "pcheck", "pch", "playercheck" };

        /// <inheritdoc/>
        public string Description { get; } = "Check player's violations.";

        private static readonly string FilePath = $@"{Paths.Plugins}/MultiTools/{Server.Port}/BadList.txt";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("mt.check"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            else if (arguments.Count != 1)
            {
                response = "Using: p (ID)";
                return false;
            }
            string playerId = arguments.At(0);
            Player bad = Player.Get(playerId);
            if (bad == null)
            {
                response = $"Player ID {playerId} don't found.";
                return false;
            }

            string steamId = bad.UserId;
            List<string> warnings = new List<string>();
            List<string> bans = new List<string>();

            if (File.Exists(FilePath))
            {
                foreach (var line in File.ReadAllLines(FilePath))
                {
                    if (line.StartsWith($"Warn: {steamId}"))
                    {
                        warnings.Add(line);
                    }
                    if (line.StartsWith($"Ban: {steamId}"))
                    {
                        bans.Add(line);
                    }
                }
            }
            else
            {
                response = "Badlist don't found.";
                return false;
            }

            if (warnings.Count == 0 && bans.Count > 0)
            {
                response = $"Player {bad.Nickname} (SteamID: {steamId}) has not warns." + "\n\n" + $"Player bans {bad.Nickname} (SteamID: {steamId}):\n" + string.Join("\n", bans);
                return true;
            }
            else if (warnings.Count > 0 && bans.Count == 0)
            {
                response = $"Warnings for {bad.Nickname} (SteamID: {steamId}):\n" + string.Join("\n", warnings) + "\n\n" + $"Player {bad.Nickname} (SteamID: {steamId}) has not bans!";
                return true;
            }
            else if (warnings.Count == 0 && bans.Count == 0)
            {
                response = $"Player {bad.Nickname} (SteamID: {steamId}) don't has violationы!";
                return true;
            }

            else
            {
                response = $"Warnins for {bad.Nickname} (SteamID: {steamId}):\n" + string.Join("\n", warnings) + "\n\n" + $"Player bans {bad.Nickname} (SteamID: {steamId}):\n" + string.Join("\n", bans);
                return true;
            }
        }
    }
}
