using MenuChanger.Attributes;

namespace CombatRandomizer.Settings
{
    public class NailDamageSettings
    {
        [MenuRange(0, 21)]
        public int BaseDamage;
        [MenuRange(0, 21)]
        public int NailItems;
    }
}