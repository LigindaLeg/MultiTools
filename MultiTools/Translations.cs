namespace MultiTools
{
    using Exiled.API.Interfaces;

    public sealed class Translations : ITranslation
    {
        public string CheaterHint { get; private set; } = $"You are being checked for cheats!\nYou must report the player [Admin.Nickname]([Admin.DisplayNickname]) on your discord.\nYou have [time] seconds left before an automatic ban!\nWhen you leave the server you will receive a ban.";
    }
}
