namespace CombatRandomizer.Settings
{
    public class CombatSettings
    {
        public bool Enabled { get; set; } = false;
        public NailSettings NailSettings { get; set; } = new();
        public NotchSettings NotchSettings { get; set; } = new();
        public GainSettings GainSettings { get; set; } = new();
        public DrainSettings DrainSettings { get; set; } = new();
    }

    public class NailSettings
    {
        public bool Enabled;
        public NailDamageSettings NailDamageSettings = NailPresets.Standard;
        public bool LimitNailDamage { get; set;} = false;
    }

    public class NotchSettings
    {
        public bool Enabled;
        public NotchFragmentSettings NotchFragmentSettings = NotchPresets.Standard;
    }

    public class GainSettings
    {
        public bool Enabled;
        public SoulGainSettings SoulGainSettings = SoulGainPresets.Standard;
    }
    public class DrainSettings
    {
        public bool Enabled;
        public SoulDrainSettings SoulDrainSettings = SoulDrainPresets.Standard;
    }
}