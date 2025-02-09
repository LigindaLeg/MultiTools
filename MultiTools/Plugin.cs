using System;
using System.Collections.Generic;
using System.IO;
using Exiled.API.Features;
using Exiled.Events.Features;

namespace MultiTools
{
    public class Plugin : Plugin<Config, Translations>
    {
        public override string Name => "MultiTools";
        public override string Author => "Liginda";
        public override Version Version => new Version(1, 1, 2);
        public override Version RequiredExiledVersion => new Version(9,0,0);
        public override string Prefix => "MultiTools";

        public static Plugin Instance;
        public Player Cheater;
        public EventHandlers eventHandlers;
        public List<Player> BlockDoorList = new List<Player>();
        internal static bool warningsent = false;
        static string multiToolsPath = Path.Combine(Paths.Plugins, "MultiTools");
        static string portPath = Path.Combine(multiToolsPath, Server.Port.ToString());
        static string badListFilePath = Path.Combine(portPath, "BadList.txt");
        static string prefixFilePath = Path.Combine(portPath, "Prefix.txt");
        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            try
            {
                if (!Directory.Exists(multiToolsPath))
                {
                    Directory.CreateDirectory(multiToolsPath);
                    Log.Info("Папка MultiTools успешно создана.");
                }

                // Проверяем и создаем папку {Server.Port}
                if (!Directory.Exists(portPath))
                {
                    Directory.CreateDirectory(portPath);
                    Log.Info($"Папка {Server.Port} успешно создана в MultiTools.");
                }

                // Проверяем и создаем файл BadList.txt
                if (!File.Exists(badListFilePath))
                {
                    File.Create(badListFilePath).Dispose(); // Создаем файл и освобождаем ресурсы
                    Log.Info("Файл BadList.txt успешно создан.");
                }

                // Проверяем и создаем файл Prefix.txt
                if (!File.Exists(prefixFilePath))
                {
                    File.Create(prefixFilePath).Dispose(); // Создаем файл и освобождаем ресурсы
                    Log.Info("Файл Prefix.txt успешно создан.");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Произошла ошибка: {ex.Message}");
            }
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
            Exiled.Events.Handlers.Server.LocalReporting += new CustomEventHandler<Exiled.Events.EventArgs.Server.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.Banned += new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Exiled.Events.Handlers.Warhead.Detonated += new CustomEventHandler(eventHandlers.OnDetonated);
            Exiled.Events.Handlers.Player.EarningAchievement += new CustomEventHandler<Exiled.Events.EventArgs.Player.EarningAchievementEventArgs>(eventHandlers.OnEarningAchievement);
            Exiled.Events.Handlers.Player.Verified += new CustomEventHandler<Exiled.Events.EventArgs.Player.VerifiedEventArgs>(eventHandlers.OnJoined);
        }

        public void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= new CustomEventHandler(eventHandlers.OnRoundStarted);
            Exiled.Events.Handlers.Player.Left -= new CustomEventHandler<Exiled.Events.EventArgs.Player.LeftEventArgs>(eventHandlers.OnCheaterLeave);
            Exiled.Events.Handlers.Player.InteractingDoor -= new CustomEventHandler<Exiled.Events.EventArgs.Player.InteractingDoorEventArgs>(eventHandlers.DoorBlock);
            Exiled.Events.Handlers.Server.LocalReporting -= new CustomEventHandler<Exiled.Events.EventArgs.Server.LocalReportingEventArgs>(eventHandlers.Reporting);
            Exiled.Events.Handlers.Player.Banned -= new CustomEventHandler<Exiled.Events.EventArgs.Player.BannedEventArgs>(eventHandlers.OnPlayerBanned);
            Exiled.Events.Handlers.Warhead.Detonated -= new CustomEventHandler(eventHandlers.OnDetonated);
            Exiled.Events.Handlers.Player.EarningAchievement -= new CustomEventHandler<Exiled.Events.EventArgs.Player.EarningAchievementEventArgs>(eventHandlers.OnEarningAchievement);
            Exiled.Events.Handlers.Player.Verified -= new CustomEventHandler<Exiled.Events.EventArgs.Player.VerifiedEventArgs>(eventHandlers.OnJoined);

            eventHandlers = null;
        }
    }
}
