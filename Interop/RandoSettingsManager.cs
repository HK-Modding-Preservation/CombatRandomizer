using RandoSettingsManager;
using RandoSettingsManager.SettingsManagement;
using RandoSettingsManager.SettingsManagement.Versioning;
using CombatRandomizer.Manager;
using CombatRandomizer.Settings;

namespace CombatRandomizer.Interop
{
    internal static class RSM_Interop
    {
        public static void Hook()
        {
            RandoSettingsManagerMod.Instance.RegisterConnection(new CombatSettingsProxy());
        }
    }

    internal class CombatSettingsProxy : RandoSettingsProxy<CombatSettings, string>
    {
        public override string ModKey => CombatRandomizer.Instance.GetName();

        public override VersioningPolicy<string> VersioningPolicy { get; }
            = new EqualityVersioningPolicy<string>(CombatRandomizer.Instance.GetVersion());

        public override void ReceiveSettings(CombatSettings settings)
        {
            if (settings != null)
            {
                ConnectionMenu.Instance!.Apply(settings);
            }
            else
            {
                ConnectionMenu.Instance!.Disable();
            }
        }

        public override bool TryProvideSettings(out CombatSettings settings)
        {
            settings = CombatManager.Settings;
            return settings.Enabled;
        }
    }
}