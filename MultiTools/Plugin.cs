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
        public override Version Version => new Version(1, 0, 7);
        public override Version RequiredExiledVersion => new Version(8, 9, 6);

        public static Plugin Instance;
        public Player Cheater;
        public EventHandlers eventHandlers;
        public List<Player> BlockDoorList = new List<Player>();
        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();

            Log.Info("MultiTools has been enabled.");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            UnregisterEvents();

            Log.Info("MultiTools has been disabled.");

            base.OnDisabled();
        }


        public void RegisterEvents()
        {
            eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundStarted += new CustomEventHandler(eventHandlers.ChaosMystery);
            Exiled.Events.Handlers.Player.Left += new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor += new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting += new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.TriggeringTesla += new CustomEventHandler<Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs>(eventHandlers.TeslaManager);
            Exiled.Events.Handlers.Player.Banned += new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= new CustomEventHandler(eventHandlers.ChaosMystery);
            Exiled.Events.Handlers.Player.Left -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor -= new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.TriggeringTesla -= new CustomEventHandler<Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs>(eventHandlers.TeslaManager);
            Exiled.Events.Handlers.Player.Banned -= new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);

            eventHandlers = null;
        }
    }
}
