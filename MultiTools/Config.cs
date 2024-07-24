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
        [Description("How much time does a cheater have before being banned?")]
        public float CheatTime { get; set; } = 30;
        [Description("Cheat Ban Reason?")]
        public string BanReason { get; set; } = "You are banned for cheating [MultiTools]";
        [Description("Cuff Command?")]
        public bool CuffEnabled { get; set; } = true;
        [Description("Cuff Range?")]
        public int CuffRange { get; set; } = 3;
        [Description("Cuff Process Delay?")]
        public int CuffDel { get; set; } = 1;
    }
}
