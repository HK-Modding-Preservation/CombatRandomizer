namespace CombatRandomizer.Settings
{
    public class CombatSettings
    {
        public bool Enabled { get; set; } = false;
        public Difficulty Difficulty { get; set; } = Difficulty.Normal;
        public bool LimitNailDamage { get; set;} = false;
    }
}