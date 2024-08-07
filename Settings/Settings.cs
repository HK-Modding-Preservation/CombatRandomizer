namespace CombatRandomizer.Settings
{
    public class CombatSettings
    {
        public bool Enabled { get; set; } = false;
        public Difficulty NailDamage { get; set; } = Difficulty.Standard;
        public Difficulty NotchFragments { get; set; } = Difficulty.Standard;
        public Difficulty SoulGain { get; set; } = Difficulty.Standard;
        public Difficulty SoulPlugs { get; set; } = Difficulty.Standard;
        public bool LimitNailDamage { get; set;} = false;
    }
}