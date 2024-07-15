using System.ComponentModel;
using Exiled.API.Interfaces;

namespace MultiTools
{
    public class Config : IConfig
    {
        [Description("Is the plugin enabled?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Are debug messages displayed?")]
        public bool Debug { get; set; } = false;

        [Description("Logs Language? (RU, EN, PL)")]
        public string Lang { get; set; } = "EN";
        [Description("How much time does a cheater have before being banned?")]
        public float CheatTime { get; set; } = 30;
        [Description("Cheat Ban Reason?")]
        public string BanReason { get; set; } = "You are banned for cheating [MultiTools]";
    }
}
