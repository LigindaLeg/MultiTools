using CommandSystem;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using MEC;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    public class NtfCall : ICommand
    {
        public string Command { get; } = "ntfcall";

        public string[] Aliases { get; } = { };

        public string Description { get; } = "Call MTF squad";

        static bool MTFS;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (!Plugin.Instance.Config.rpEnabled)
            {
                response = "RP Mode disabled on this server";
                return false;
            }
            else if (Respawn.TimeUntilSpawnWave.TotalSeconds <= 120)
            {
                response = "Time to next respawn wave is too little";
                return false;
            }
            else if (MTFS)
            {
                response = "MTF Force are currently spawned";
                return false;
            }
            else if (player.CurrentRoom.Type != Exiled.API.Enums.RoomType.EzIntercom)
            {
                response = "You must be in Intercom to use this command";
                return false;
            }
            else
            {
                Cassie.Message(Plugin.Instance.Translation.NTFCall, false, true, true);
                Timing.RunCoroutine(MTF());
                response = "MTF will respawn in 2 minutes";
                return true;
            }
        }

        public IEnumerator<float> MTF()
        {
            MTFS = true;
            yield return Timing.WaitForSeconds(120f);
            Respawn.ForceWave(Respawning.SpawnableTeamType.NineTailedFox);
            yield return Timing.WaitForSeconds(300f);
            MTFS = false;
        }
    }
}
