using System;

namespace CombatRandomizer.Settings
{
    [Flags]
    public enum Difficulty 
    {
        Extreme = 4,
        Hard = 3,
        Intermediate = 2,
        Standard = 1,
        Easy = 0,
        Disabled = -1
    }
}