using System;
using System.IO;
using RandomizerCore.Json;
using RandomizerCore.Logic;
using RandomizerCore.StringItems;
using RandomizerMod.RC;
using RandomizerMod.Settings;


namespace CombatRandomizer.Manager
{
    public class LogicHandler
    {
        public static void Hook()
        {
            RCData.RuntimeLogicOverride.Subscribe(5f, ApplyLogic);
        }

        private static void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!CombatManager.Settings.Enabled)
                return;

            lmb.GetOrAddTerm("NAILDAMAGE", TermType.Int);
            lmb.GetOrAddTerm("NOTCHFRAGMENTS", TermType.Int);
            lmb.GetOrAddTerm("SOULGAIN", TermType.Int);
            lmb.GetOrAddTerm("SOULPLUG", TermType.Int);

            lmb.AddItem(new StringItemTemplate("Nail_Damage", "NAILDAMAGE++"));
            lmb.AddItem(new StringItemTemplate("Notch_Fragment", "NOTCHFRAGMENTS++"));
            lmb.AddItem(new StringItemTemplate("Soul_Gain", "SOULGAIN++"));
            lmb.AddItem(new StringItemTemplate("Soul_Plug", "SOULPLUG++"));

            JsonLogicFormat fmt = new();
            using Stream m = typeof(LogicHandler).Assembly.GetManifestResourceStream("CombatRandomizer.Resources.Logic.macros.json");
            lmb.DeserializeFile(LogicFileType.Macros, fmt, m);
        }
    }
}