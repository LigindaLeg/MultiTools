using System;
using Exiled.API.Features;
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
	}
}
