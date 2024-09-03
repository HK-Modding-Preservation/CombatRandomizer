using System.Collections.Generic;
using CombatRandomizer.Manager;

namespace CombatRandomizer.Settings
{
    public static class NailPresets
    {
        public static NailDamageSettings Easy => new()
        {
            BaseDamage = 5,
            NailItems = 20
        };
        public static NailDamageSettings Standard => new()
        {
            BaseDamage = 4,
            NailItems = 19
        };
        public static NailDamageSettings Intermediate => new()
        {
            BaseDamage = 3,
            NailItems = 18
        };
        public static NailDamageSettings Hard => new()
        {
            BaseDamage = 3,
            NailItems = 10
        };
        public static NailDamageSettings Extreme => new()
        {
            BaseDamage = 2,
            NailItems = 7
        };

        public static readonly Dictionary<string, NailDamageSettings> Presets = new()
        {
            [nameof(Easy)] = Easy,
            [nameof(Standard)] = Standard,
            [nameof(Intermediate)] = Intermediate,
            [nameof(Hard)] = Hard,
            [nameof(Extreme)] = Extreme
        };
    }

    public static class NotchPresets
    {
        public static NotchFragmentSettings Easy => new()
        {
            FragmentsPerNotch = 2,
            MaxNotches = 11
        };
        public static NotchFragmentSettings Standard => new()
        {
            FragmentsPerNotch = 3,
            MaxNotches = 11
        };
        public static NotchFragmentSettings Intermediate => new()
        {
            FragmentsPerNotch = 4,
            MaxNotches = 11
        };
        public static NotchFragmentSettings Hard => new()
        {
            FragmentsPerNotch = 4,
            MaxNotches = 9
        };
        public static NotchFragmentSettings Extreme => new()
        {
            FragmentsPerNotch = 4,
            MaxNotches = 6
        };

        public static readonly Dictionary<string, NotchFragmentSettings> Presets = new()
        {
            [nameof(Easy)] = Easy,
            [nameof(Standard)] = Standard,
            [nameof(Intermediate)] = Intermediate,
            [nameof(Hard)] = Hard,
            [nameof(Extreme)] = Extreme
        };
    }

    public static class SoulGainPresets
    {
        public static SoulGainSettings Easy => new()
        {
            BaseGain = 8,
            SoulGainItems = 8
        };
        public static SoulGainSettings Standard => new()
        {
            BaseGain = 7,
            SoulGainItems = 7
        };

        public static SoulGainSettings Intermediate => new()
        {
            BaseGain = 4,
            SoulGainItems = 7
        };

        public static SoulGainSettings Hard => new()
        {
            BaseGain = 3,
            SoulGainItems = 5
        };

        public static SoulGainSettings Extreme => new()
        {
            BaseGain = 1,
            SoulGainItems = 4
        };

        public static readonly Dictionary<string, SoulGainSettings> Presets = new()
        {
            [nameof(Easy)] = Easy,
            [nameof(Standard)] = Standard,
            [nameof(Intermediate)] = Intermediate,
            [nameof(Hard)] = Hard,
            [nameof(Extreme)] = Extreme
        };
    }

    public static class SoulDrainPresets
    {
        public static SoulDrainSettings Easy => new()
        {
            BaseDrain = 1,
            PlugItems = 2
        };
        public static SoulDrainSettings Standard => new()
        {
            BaseDrain = 2,
            PlugItems = 3
        };

        public static SoulDrainSettings Intermediate => new()
        {
            BaseDrain = 3,
            PlugItems = 3
        };

        public static SoulDrainSettings Hard => new()
        {
            BaseDrain = 5,
            PlugItems = 4
        };

        public static SoulDrainSettings Extreme => new()
        {
            BaseDrain = 7,
            PlugItems = 5
        };

        public static readonly Dictionary<string, SoulDrainSettings> Presets = new()
        {
            [nameof(Easy)] = Easy,
            [nameof(Standard)] = Standard,
            [nameof(Intermediate)] = Intermediate,
            [nameof(Hard)] = Hard,
            [nameof(Extreme)] = Extreme
        };
    }
}