using MenuChanger.Attributes;

namespace CombatRandomizer.Settings
{
    public class NotchFragmentSettings
    {
        [MenuRange(2, 4)]
        public int FragmentsPerNotch;
        [MenuRange(1, 11)]
        public int MaxNotches;
    }
}