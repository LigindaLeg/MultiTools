using System;
using Exiled.API.Features;
using Exiled.Events.Features;

namespace MultiTools
{
    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "MultiTools";
        public override string Author => "Liginda";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(8, 9, 6);

        public static Plugin Instance;
        public Player Cheater;
        public EventHandlers eventHandlers;
        public Player BlockDoorList;
        public override void OnEnabled()
        {
            Instance = this;
            this.eventHandlers = new EventHandlers();
            Exiled.Events.Handlers.Player.Left += new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor += new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Log.Info("MultiTools has been enabled.");

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            this.eventHandlers = null;
            Exiled.Events.Handlers.Player.Left -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor -= new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Log.Info("MultiTools has been disabled.");

            base.OnDisabled();
        }
    }
}
