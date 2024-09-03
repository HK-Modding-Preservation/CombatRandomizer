using MenuChanger.Attributes;

namespace CombatRandomizer.Settings
{
    public class SoulDrainSettings
    {
        [MenuRange(0, 10)]
        public int BaseDrain;
        [MenuRange(0, 10)]
        public int PlugItems;
    }
}