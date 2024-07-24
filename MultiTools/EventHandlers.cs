using System;
using Exiled.API.Features;
using Exiled.API.Features.Doors;
using Exiled.Events;
namespace MultiTools
{
	public class EventHandlers
	{
		public void OnCheaterLeave(Exiled.Events.EventArgs.Player.LeftEventArgs ev)
        {
			Player MultiTool = Player.Get("[MultiTools]");
			if (ev.Player == Plugin.Instance.Cheater)
            {
				ev.Player.Ban(1577836800, Plugin.Instance.Config.BanReason, MultiTool);
				Plugin.Instance.Cheater = null;
			}
        }
		public void DoorBlock(Exiled.Events.EventArgs.Player.InteractingDoorEventArgs ev)
        {
			if (Plugin.Instance.BlockDoorList.Contains(ev.Player))
            {
				if (ev.Door.IsLocked)
                {
					Log.Info(ev.Door + "is locked");
					ev.IsAllowed = true;
					ev.Door.Unlock();
				}
				else if (!ev.Door.IsLocked)
                {
					Log.Info(ev.Door + "is unlocked");
					ev.IsAllowed = false;
					ev.Door.Lock(99999, Exiled.API.Enums.DoorLockType.SpecialDoorFeature);
				}
            }
			else
            {
				return;
            }
        }
		public void Reporting(Exiled.Events.EventArgs.Player.LocalReportingEventArgs ev)
		{
			string reason = Plugin.Instance.Translation.AdminReportHint;
			ev.Player.ShowHint(Plugin.Instance.Translation.ReportHint.Replace("[target]", ev.Target.Nickname), 10f);
			foreach (Player player in Player.List)
            {
				if (player.RemoteAdminAccess)
                {
					player.ShowHint(reason.Replace("[target]", ev.Target.Nickname).Replace("[player]", ev.Player.Nickname).Replace("[reason]", ev.Reason), 15f);
                }
            }
		}
	}
}
