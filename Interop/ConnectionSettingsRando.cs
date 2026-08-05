using ConnectionSettingsRando;
using CombatRandomizer.Manager;

namespace CombatRandomizer.Interop
{
    internal static class CSR_Interop
    {
        public static void Hook()
        {
            CSR.Register(
            CombatRandomizer.Instance.GetName(),
            () => CombatManager.Settings,
            s => SettingsRandomizer.CopyTo(s, CombatManager.Settings));
        }
    }
}