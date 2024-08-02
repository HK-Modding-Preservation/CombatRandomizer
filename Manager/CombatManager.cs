using Newtonsoft.Json;
using RandomizerMod.Logging;
using CombatRandomizer.Settings;
using CombatRandomizer.Modules;
using ItemChanger;

namespace CombatRandomizer.Manager
{
    internal static class CombatManager
    {
        public static CombatSettings Settings => CombatRandomizer.Instance.GS;
        public static void Hook()
        {
            ItemHandler.Hook();
            ConnectionMenu.Hook();
            LogicHandler.Hook();
            SettingsLog.AfterLogSettings += AddFileSettings;
        }

        private static void AddFileSettings(LogArguments args, System.IO.TextWriter tw)
        {
            // Log settings into the settings file
            tw.WriteLine("Combat Randomizer Settings:");
            using JsonTextWriter jtw = new(tw) { CloseOutput = false };
            RandomizerMod.RandomizerData.JsonUtil._js.Serialize(jtw, Settings);
            tw.WriteLine();

            if (Settings.Enabled)
            {
                CombatModule module = ItemChangerMod.Modules.GetOrAdd<CombatModule>();
                // Enforce damage limit if Enemy Pogo setting is on
                module.Settings.LimitNailDamage = module.Settings.LimitNailDamage || args.gs.SkipSettings.EnemyPogos;
            }
        }        
    }
}