namespace MultiTools
{
    using Exiled.API.Interfaces;

    public sealed class Translations : ITranslation
    {
        public string CheaterHint { get; private set; } = $"You are being checked for cheats!\nYou must report the player [Admin.Nickname]([Admin.DisplayNickname]) on your discord.\nYou have [time] seconds left before an automatic ban!\nWhen you leave the server you will receive a ban.";
        public string AdminReportHint { get; private set; } = $"The player [target] received a complaint from [player] for the reason:\n[reason]!!!";
        public string ReportHint { get; private set; } = $"Successfully sent a report to player [target]!";
        public string TargetCuffHint { get; private set; } = $"You are tied up, do not move for [time] seconds!";
        public string PlayerCuffHint { get; private set; } = $"You cuff player [target] do not move for [time] seconds!";
        public string CuffError { get; private set; } = $"You need to hold a weapon in your hands to do this.";
        public string CallBroadcast { get; private set; } = $"Admins!\n Player [player] calling you!";
        public string NTFCall { get; private set; } = "Mobile Task Force Unit Epsilon 11 Designated .g5 .g5 .g5 **** has entered the facility about t minus 2 minutes";
        public string ChaosHint { get; private set; } = "You were spawned as a Chaos Insurgency Spy";
    }
}
