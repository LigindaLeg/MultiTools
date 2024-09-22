using System;
using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.Features;

namespace MultiTools
{
    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "MultiTools";
        public override string Author => "Liginda";
        public override Version Version => new Version(1, 0, 9);
        public override Version RequiredExiledVersion => new Version(8, 9, 6);
        public override string Prefix => "MultiTools";

        public static Plugin Instance;
        public Player Cheater;
        public EventHandlers eventHandlers;
        public List<Player> BlockDoorList = new List<Player>();
        internal static bool warningsent = false;
        public class PluginInfo
        {
            public string Name { get; set; }
            public string GitHubRepoUrl { get; set; }
        }
        public readonly PluginInfo pluginToUpdate = new PluginInfo { Name = "MultiTools", GitHubRepoUrl = "https://github.com/LigindaLeg/MultiTools" };
        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();

            Log.Info
            (
                     "\n" +
                     "███╗░░░███╗██╗░░░██╗██╗░░░░░████████╗██╗████████╗░█████╗░░█████╗░██╗░░░░░░██████╗\n" +
                     "████╗░████║██║░░░██║██║░░░░░╚══██╔══╝██║╚══██╔══╝██╔══██╗██╔══██╗██║░░░░░██╔════╝\n" +
                     "██╔████╔██║██║░░░██║██║░░░░░░░░██║░░░██║░░░██║░░░██║░░██║██║░░██║██║░░░░░╚█████╗░\n" +
                     "██║╚██╔╝██║██║░░░██║██║░░░░░░░░██║░░░██║░░░██║░░░██║░░██║██║░░██║██║░░░░░░╚═══██╗\n" +
                     "██║░╚═╝░██║╚██████╔╝███████╗░░░██║░░░██║░░░██║░░░╚█████╔╝╚█████╔╝███████╗██████╔╝\n" +
                     "╚═╝░░░░░╚═╝░╚═════╝░╚══════╝░░░╚═╝░░░╚═╝░░░╚═╝░░░░╚════╝░░╚════╝░╚══════╝╚═════╝░" +
                     "\n"
            );
            AutoUpdater.CreatePLuginUpdaterFiles();
            AutoUpdater.UpdatePlugins();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            UnregisterEvents();

            base.OnDisabled();
        }


        public void RegisterEvents()
        {
            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundStarted += new CustomEventHandler(eventHandlers.OnRoundStarted);
            Exiled.Events.Handlers.Player.Left += new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor += new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting += new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.Banned += new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Exiled.Events.Handlers.Server.RoundEnded += new CustomEventHandler<Exiled.Events.EventArgs.Server.RoundEndedEventArgs>(eventHandlers.EndFix);
            Exiled.Events.Handlers.Server.RestartingRound += new CustomEventHandler(eventHandlers.EndFix1);
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= new CustomEventHandler(eventHandlers.OnRoundStarted);
            Exiled.Events.Handlers.Player.Left -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor -= new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.Banned -= new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Exiled.Events.Handlers.Server.RoundEnded -= new CustomEventHandler<Exiled.Events.EventArgs.Server.RoundEndedEventArgs>(eventHandlers.EndFix);
            Exiled.Events.Handlers.Server.RestartingRound -= new CustomEventHandler(eventHandlers.EndFix1);

            eventHandlers = null;
        }
    }
}
