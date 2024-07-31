using PlayerRoles;
using System.Collections.Generic;

namespace MultiTools.Enums
{
    public static class RoleTypeTranslations
    {
        public static Dictionary<RoleTypeId, string> GetTranslations(Translations translations)
        {
            return new Dictionary<RoleTypeId, string>
            {
                { RoleTypeId.ClassD, translations.ClassD },
                { RoleTypeId.Scientist, translations.Scientist },
                { RoleTypeId.FacilityGuard, translations.FacilityGuard },
                { RoleTypeId.ChaosConscript, translations.ChaosConscript },
                { RoleTypeId.ChaosMarauder, translations.ChaosMarauder },
                { RoleTypeId.ChaosRepressor, translations.ChaosRepressor },
                { RoleTypeId.ChaosRifleman, translations.ChaosRifleman },
                { RoleTypeId.NtfPrivate, translations.NtfPrivate },
                { RoleTypeId.NtfSergeant, translations.NtfSergeant },
                { RoleTypeId.NtfSpecialist, translations.NtfSpecialist },
                { RoleTypeId.NtfCaptain, translations.NtfCaptain },
                { RoleTypeId.Scp173, translations.Scp173 },
                { RoleTypeId.Scp106, translations.Scp106 },
                { RoleTypeId.Scp049, translations.Scp049 },
                { RoleTypeId.Scp3114, translations.Scp3114 },
                { RoleTypeId.Scp939, translations.Scp939 },
                { RoleTypeId.Scp0492, translations.Scp0492 },
                { RoleTypeId.Scp079, translations.Scp079 },
                { RoleTypeId.Scp096, translations.Scp096 },
                { RoleTypeId.Tutorial, translations.Tutorial },
            };
        }
    }
}