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
        
        [Description("Update interval for the hint in seconds.")] // interwal aktualizacji hint.
        public float UpdateInterval { get; set; } = 1.0f;

        [Description("Server name to be displayed in the hint.")]
        public string ServerName { get; set; } = "YOU SERVER NAME"; // Nazwa serwera.

        [Description("Round time format. Use placeholders: {0} - minutes, {1} - seconds. DONT TOUCH!")] // NIE DOTYKAC!
        public string RoundTimeFormat { get; set; } = "The round runs: {0} minutes, {1} seconds.";

        [Description("Hint text format. Use placeholders: {0} - player role color, {1} - player nickname, {2} - player role, {3} - spectator count, {4} - kill count, {5} - server name")] // informacja o skrocie kodu
        public string HintText { get; set; } = @"
    <voffset=-0.2em><pos=-300><size=15><align=left><voffset=0><color={0}>👤</color> <color={0}>| You name:</color> {1}
    <voffset=-0.2em><pos=-300><color={0}>🎭</color> <color={0}>| You play for:</color> {2}
    <voffset=-0.2em><pos=-300><color={0}>👥</color> <color={0}>| You're being watched:</color> <color={0}>{3}</color></voffset></pos>
    <size=20><align=center><voffset=-32em><u><pos=-0>{5}</pos></u></voffset>\n<pos=-0></pos></align></size>";
    }
}
