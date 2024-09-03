using MenuChanger.Attributes;

namespace CombatRandomizer.Settings
{
    public class SoulGainSettings
    {
        [MenuRange(0, 11)]
        public int BaseGain;
        [MenuRange(0, 11)]
        public int SoulGainItems;
    }
}