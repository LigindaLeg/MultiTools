using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.Features;
using MEC;
using PlayerRoles;
using MultiTools.Enums;

namespace MultiTools
{
    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "MultiTools";
        public override string Author => "Liginda & kaczka";
        public override Version Version => new Version(1, 0, 6);
        public override Version RequiredExiledVersion => new Version(8, 9, 6);

        public static Plugin Instance;
        private CoroutineHandle updateCoroutine;
        public DateTime roundStartTime;
        public bool roundStarted = false;
        public Dictionary<Player, int> playerKills = new Dictionary<Player, int>();
        public Player Cheater;
        public EventHandlers eventHandlers;
        public List<Player> BlockDoorList = new List<Player>();

        public override void OnEnabled()
        {
            Instance = this;
            this.eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Left +=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor +=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers
                    .DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting +=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.TriggeringTesla +=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs>(eventHandlers
                    .TeslaManager);
            Exiled.Events.Handlers.Player.Banned +=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Exiled.Events.Handlers.Server.RoundStarted += new CustomEventHandler(eventHandlers.ChaosMystery);
            updateCoroutine = Timing.RunCoroutine(UpdateRoutine());
            Log.Info("MultiTools has been enabled.");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            this.eventHandlers = null;
            Exiled.Events.Handlers.Player.Left -=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor -=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers
                    .DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting -=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.TriggeringTesla -=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs>(eventHandlers
                    .TeslaManager);
            Exiled.Events.Handlers.Player.Banned -=
                new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Timing.KillCoroutines(updateCoroutine);
            Log.Info("MultiTools has been disabled.");
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            eventHandlers = new EventHandlers();

            Exiled.Events.Handlers.Server.RoundStarted += eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.Died += eventHandlers.OnPlayerDied;
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= eventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Player.Died -= eventHandlers.OnPlayerDied;

            eventHandlers = null;
        }
            private string GetRoleColor(RoleTypeId role)
        {
            var translations = RoleTypeTranslations.GetTranslations(Translation);
            if (translations.TryGetValue(role, out var translatedRole))
            {
                var colorCodeMatch = System.Text.RegularExpressions.Regex.Match(translatedRole, @"<color=(.*?)>");
                if (colorCodeMatch.Success)
                {
                    return colorCodeMatch.Groups[1].Value;
                }
            }

            return "{cargoCor}";
        }

        private IEnumerator<float> UpdateRoutine()
        {
            while (true)
            {
                yield return Timing.WaitForSeconds(Config.UpdateInterval);

                if (roundStarted)
                {
                    TimeSpan elapsedTime = DateTime.Now - roundStartTime;
                    string roundTimeFormatted =
                        string.Format(Config.RoundTimeFormat, elapsedTime.Minutes, elapsedTime.Seconds);
                    var translations = RoleTypeTranslations.GetTranslations(Translation);

                    foreach (var player in Player.List)
                    {
                        if (player.IsAlive)
                        {
                            string PlayerRole = translations.TryGetValue(player.Role.Type, out var translatedRole)
                                ? translatedRole
                                : player.Role.Type.ToString();
                            string cargoCor = GetRoleColor(player.Role.Type);
                            int spectatorCount = player.CurrentSpectatingPlayers
                                .Where(spectator => spectator.Role.Type != RoleTypeId.Overwatch).Count();
                            int playerKillCount = playerKills.TryGetValue(player, out var kills) ? kills : 0;
                            string hintText = string.Format(Config.HintText, cargoCor, player.DisplayNickname,
                                PlayerRole, spectatorCount, playerKillCount, Config.ServerName);
                            player.ShowHint(hintText, 2.5f);
                        }
                    }
                }
            }
        }
    }
}
