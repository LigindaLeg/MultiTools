using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using InventorySystem.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace MultiTools.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]

    public class CuffCommand : ICommand
    {

        public string Command { get; } = "cuff";

        public string[] Aliases { get; } = {};

        public string Description { get; } = "Allows you to cuff teammates";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            
            Player player = Player.Get(sender);
            var ray = new Ray(player.CameraTransform.position + (player.CameraTransform.forward * 0.1f), player.CameraTransform.forward);
            if (!Physics.Raycast(ray, out RaycastHit hit, Plugin.Instance.Config.CuffRange))
            {
                response = "";
                return false;
            }
            var target = Player.Get(hit.collider);
            if (!Plugin.Instance.Config.CuffEnabled)
            {
                response = "Command disabled on this server";
                return false;
            }
            Timing.RunCoroutine(Cuff(target, player));
            response = "";
            return true;
        }
        public IEnumerator<float> Cuff(Player target, Player player)
        {
            if (player.CurrentItem.Category == ItemCategory.Firearm)
            {
                float f11 = Plugin.Instance.Config.CuffDel;
                for (float i = 0; i <= Plugin.Instance.Config.CuffDel; i++)
                {
                    target.ShowHint(Plugin.Instance.Translation.TargetCuffHint.Replace("[time]", f11.ToString()), 1f);
                    player.ShowHint(Plugin.Instance.Translation.PlayerCuffHint.Replace("[target]", target.Nickname).Replace("[time]", f11.ToString()), 1f);
                    f11--;
                    yield return Timing.WaitForSeconds(1f);
                }
                target.Handcuff();
                target.DropItems();
            }
            else
            {
                player.ShowHint(Plugin.Instance.Translation.CuffError, 3f);
            }
        }
    }
}
