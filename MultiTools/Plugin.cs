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
        private EventHandlers _handlers;
        public Player Cheater;

        public override void OnEnabled()
        {
            Instance = this;
            Exiled.Events.Handlers.Player.Left += new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(this._handlers.OnCheaterLeave);
            if (Plugin.Instance.Config.Lang.ToLower() == "en")
            {
                Log.Info("MultiTools has been enabled.");
            }
            else if (Plugin.Instance.Config.Lang.ToLower() == "ru")
            {
                Log.Info("MultiTools был включен.");
            }
            else if (Plugin.Instance.Config.Lang.ToLower() == "pl")
            {
                Log.Info("MultiTools zostało włączone.");
            }
            else
            {
                Log.Error("The config specifies the wrong language for logging. By default, English will be used.");
                Log.Info("MultiTools has been enabled.");
            }

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Instance = null;
            Exiled.Events.Handlers.Player.Left -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(this._handlers.OnCheaterLeave);
            if (Plugin.Instance.Config.Lang.ToLower() == "en")
            {
                Log.Info("MultiTools has been disabled.");
            }
            else if (Plugin.Instance.Config.Lang.ToLower() == "ru")
            {
                Log.Info("MultiTools был выключен.");
            }
            else if (Plugin.Instance.Config.Lang.ToLower() == "pl")
            {
                Log.Info("MultiTools zostało wyłączone.");
            }
            else
            {
                Log.Info("MultiTools has been disabled.");
            }

            base.OnDisabled();
        }
    }
}
