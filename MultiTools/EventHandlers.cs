using System;
using System.IO;
using System.Net.Http;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.Events.EventArgs.Server;
using MEC;
using System.Collections.Generic;
using Exiled.API.Features;

namespace MultiTools
{
	public class EventHandlers
	{
        public string message;
        public string webhookUrl;

        public void OnCheaterLeave(LeftEventArgs ev)
        {
			Player MultiTool = Player.Get("[MultiTools]");
			if (ev.Player == Plugin.Instance.Cheater)
            {
				ev.Player.Ban(1577836800, Plugin.Instance.Config.BanReason, MultiTool);
				Plugin.Instance.Cheater = null;
			}
        }
		public void DoorBlock(InteractingDoorEventArgs ev)
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
		public void Reporting(LocalReportingEventArgs ev)
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

        public void OnPlayerBanned(BannedEventArgs ev)
        {
            string steamId = ev.Target.UserId;
            string reason = ev.Details.Reason;
            string time = DateTime.Today.TimeOfDay.Hours.ToString() + ":" + DateTime.Today.TimeOfDay.Minutes.ToString();

            string logEntry = $"Ban: {steamId} Reason:{reason} Time: {time} min.";
            message = Plugin.Instance.Config.DSMessage.Replace("{bantime}", DateTime.Today.TimeOfDay.Hours.ToString() + ":" + DateTime.Today.TimeOfDay.Minutes.ToString()).Replace("{admin}", ev.Player.Nickname).Replace("{bad}", ev.Target.Nickname).Replace("{reason}", ev.Details.Reason);
            webhookUrl = Plugin.Instance.Config.WebhookNotifyBan;

            try
            {
                SendDiscordMessage(webhookUrl, message);
                File.AppendAllText($@"{Paths.Plugins}/MultiTools/{Server.Port}/BadList.txt", logEntry + Environment.NewLine);
                Log.Info($"Ban saved: {logEntry}");
            }
            catch (Exception ex)
            {
                Log.Error($"Error: {ex.Message}");
            }
        }

        static async Task SendDiscordMessage(string webhookUrl, string message)
        {
            using (var client = new HttpClient())
            {
                var payload = new
                {
                    content = message
                };

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(webhookUrl, content);
            }
        }
        public void OnRoundStarted()
        {
            
        }
        public void EndFix(RoundEndedEventArgs ev)
        {
            Timing.KillCoroutines();
            Timing.RunCoroutine(Round(ev));
        }
        public IEnumerator<float> Round(Exiled.Events.EventArgs.Server.RoundEndedEventArgs ev)
        {
            yield return Timing.WaitForSeconds(ev.TimeToRestart);
            Exiled.API.Features.Round.RestartSilently();
        }
        public void EndFix1()
        {
            Timing.KillCoroutines();
        }
    }
}
