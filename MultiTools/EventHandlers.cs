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
			if (ev.Player == Plugin.Instance.BlockDoorList)
            {
				if (ev.Door.IsLocked)
                {
					ev.Door.ChangeLock(Exiled.API.Enums.DoorLockType.None);
                }
				if (!ev.Door.IsLocked)
                {
					if (ev.Door.IsFullyClosed)
                    {
						ev.Player.ShowHint("Please keep door open for use it!", 5f);
                    }
					if (ev.Door.IsMoving)
					{
						ev.Player.ShowHint("Please keep door open for use it!", 5f);
					}
					if (ev.Door.IsFullyOpen)
                    {
						ev.Door.Lock(99999999, Exiled.API.Enums.DoorLockType.AdminCommand);
                    }
					
                }
            }
        }
	}
}
