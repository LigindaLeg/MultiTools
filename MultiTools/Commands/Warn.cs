using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Warn : ICommand
    {
        public string Command { get; } = "warn";

        /// <inheritdoc/>
        public string[] Aliases { get; } = new[] { "w", "addwarn", "warning", "newwarn" };

        /// <inheritdoc/>
        public string Description { get; } = "Givе warn to player.";

        private static readonly string FilePath = $@"{Paths.Plugins}/MultiTools/{Server.Port}/BadList.txt";

        Player admin;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!sender.CheckPermission("mt.warn"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }

            if (arguments.Count < 2)
            {
                response = "Usage: warn <add/delete/list> <ID> [Reason]";
                return false;
            }

            string subCommand = arguments.At(0).ToLower();
            string playerId = arguments.At(1);
            string reason = "";
            for (int a = 2; a < arguments.Count; a++)
            {
                reason = reason + arguments.At(a) + " ";
            }

            switch (subCommand)
            {
                case "add":
                    return AddWarning(playerId, reason, sender, out response);
                case "delete":
                    return DeleteWarning(playerId, sender, out response);
                case "list":
                    return ListWarnings(playerId, out response);
                default:
                    response = "Invalid sub-command. Usage: warn <add/delete/list> <ID> [Reason]";
                    return false;
            }
        }

        private bool AddWarning(string playerId, string reason, ICommandSender sender, out string response)
        {
            Player bad = Player.Get(playerId);
            if (bad == null)
            {
                response = $"Player with ID {playerId} not found.";
                return false;
            }

            string steamId = bad.UserId;
            string steamName = bad.Nickname;

            string logEntry = $"Warn: {steamId} Nickname: {steamName} Reason: {reason}";

            Player admin = Player.Get(sender);

            try
            {
                File.AppendAllText(FilePath, logEntry + Environment.NewLine);
                bad.ShowHint($"{steamName}, you have been warned by <color=red>{admin.Nickname}</color>.\nReason: {reason}", 30f);
                response = $"Player {bad.Nickname} (SteamID: {steamId}) warned. Reason: {reason}";
                return true;
            }
            catch (Exception ex)
            {
                response = $"Error: {ex.Message}";
                return false;
            }
        }

        private bool DeleteWarning(string playerId, ICommandSender sender, out string response)
        {
            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"Player with ID {playerId} not found.";
                return false;
            }

            string steamId = player.UserId;

            try
            {
                List<string> lines = File.ReadAllLines(FilePath).ToList();
                int initialCount = lines.Count;
                lines.RemoveAll(line => line.Contains($"Warn: {steamId} "));

                if (lines.Count == initialCount)
                {
                    response = $"No warnings found for player with SteamID: {steamId}.";
                    return false;
                }

                File.WriteAllLines(FilePath, lines);
                response = $"All warnings for player {player.Nickname} (SteamID: {steamId}) have been removed.";
                return true;
            }
            catch (Exception ex)
            {
                response = $"Error: {ex.Message}";
                return false;
            }
        }

        private bool ListWarnings(string playerId, out string response)
        {
            Player player = Player.Get(playerId);
            if (player == null)
            {
                response = $"Player with ID {playerId} not found.";
                return false;
            }

            string steamId = player.UserId;

            try
            {
                List<string> lines = File.ReadAllLines(FilePath).ToList();
                List<string> playerWarnings = lines.Where(line => line.Contains($"Warn: {steamId} ")).ToList();

                if (playerWarnings.Count == 0)
                {
                    response = $"No warnings found for player with SteamID: {steamId}.";
                    return false;
                }

                response = $"Warnings for player {player.Nickname} (SteamID: {steamId}):\n" + string.Join("\n", playerWarnings);
                return true;
            }
            catch (Exception ex)
            {
                response = $"Error: {ex.Message}";
                return false;
            }
        }
    }
}
