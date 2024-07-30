using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

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
        [Description("RP Mode?")]
        public bool rpEnabled { get; set; } = true;
        [Description("Cuff Range?")]
        public int CuffRange { get; set; } = 3;
        [Description("Cuff Process Delay?")]
        public int CuffDel { get; set; } = 1;
        [Description("Tesla Ignore Roles?")]
        public List<RoleTypeId> IgnoreTeslaRoles { get; set; } = new List<RoleTypeId>() 
        { 
            RoleTypeId.NtfCaptain, RoleTypeId.NtfPrivate, RoleTypeId.NtfSergeant, RoleTypeId.NtfSpecialist, RoleTypeId.FacilityGuard 
        };

        [Description("Discord Webhook to Ban-Notify?")]
        public string WebhookNotifyBan { get; set; } = "Paste your webhook here";

        [Description("Discord Message Template?")]
        public string DSMessage { get; set; } = "{bantime} \n```html\n<Выдал:> {admin} \n<Нарушитель:> {bad} \n<Причина:> {reason} \n```";
    }
}
