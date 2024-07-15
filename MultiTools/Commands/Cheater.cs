using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;
using MEC;
using System.Collections.Generic;

namespace MultiTools
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Cheater : ICommand
    {
        public string Command { get; } = "cheater";

        public string[] Aliases { get; } = new[] { "cheat" };

        public string Description { get; } = "Forces the cheater to go to the cheat checking";

        Player Admin;
        Player Cheater1;
        /* NOT USED */
        static Player[] CheatersList;
        static int index;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Admin = Player.Get(sender);
            int cheaterID = Convert.ToInt32(arguments.At(0));
            Cheater1 = Player.Get(cheaterID);
            if (!sender.CheckPermission("mt.cheater"))
            {
                response = "You do not have permission to use this command!";
                return false;
            }
            else if (Cheater1 == null)
            {
                response = $"Player with ID {arguments.At(0)} not found";
                return false;
            }
            else if (arguments.Count < 2)
            {
                response = "Using: \ncheater add (ID)\ncheater remove (ID)\ncheater stop (ID)";
                return false;
            }
            else if (sender.CheckPermission("mt.Cheater") && Cheater1 != null)
            {
                switch (arguments.At(0))
                {
                    case "add":
                        Plugin.Instance.Cheater = Cheater1;
                        Cheater1.Role.Set(PlayerRoles.RoleTypeId.Tutorial);
                        Timing.RunCoroutine(CheaterHint());
                        response = $"Player {Cheater1.Nickname} was forced to cheat checker";
                        return true;
                    case "remove":
                        Plugin.Instance.Cheater = null;
                        Timing.KillCoroutines();
                        response = $"Player {Cheater1.Nickname} was escaped from cheat checker";
                        return true;
                    case "stop":
                        Timing.KillCoroutines();
                        response = $"Player {Cheater1.Nickname} was stoping in cheat checker";
                        return true;
                    default:
                        response = "Using: \ncheater add (ID)\ncheater remove (ID)\ncheater stop (ID)";
                        return false;
                }


                /*Cheater.Role.Set(PlayerRoles.RoleTypeId.Tutorial);
                Timing.RunCoroutine(CheaterHint());
                response = $"Player {Cheater.Nickname} was forced to cheat checker";
                return true;*/
            }
            else
            {
                response = "Using: \ncheater add (ID)\ncheater stop (ID)\ncheater pause (ID)";
                return false;
            }
        }
        public IEnumerator<float> CheaterHint()
        {
            if (Cheater1.Role == PlayerRoles.RoleTypeId.Tutorial)
            {
                float f11 = Plugin.Instance.Config.CheatTime;
                for (int i = 0; i <= Plugin.Instance.Config.CheatTime; i++)
                {
                    string hint = Plugin.Instance.Translation.CheaterHint.Replace("{Admin.Nickname}", Admin.Nickname).Replace("{Admin.DisplayNickname}", Admin.DisplayNickname).Replace("{Time}", f11.ToString());
                    Cheater1.ShowHint(hint);
                    f11 = f11 - 1;
                    yield return Timing.WaitForSeconds(1f);
                }
            }
        }
    }
}
