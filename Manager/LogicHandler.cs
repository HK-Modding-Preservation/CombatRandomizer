using CombatRandomizer.Settings;
using RandomizerCore.Json;
using RandomizerCore.Logic;
using RandomizerCore.StringItems;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using System.IO;


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

            if (CombatManager.Settings.NailDamage > Difficulty.Disabled)
            {
                lmb.GetOrAddTerm("NAILDAMAGE", TermType.Int);
                lmb.AddItem(new StringItemTemplate("Nail_Damage", "NAILDAMAGE++"));
                lmb.DoMacroEdit(new("AERIALMINIBOSS", "ORIG + (NAILDAMAGE>4 | SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("BOSS", "ORIG + (NAILDAMAGE>4 | SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("MINIBOSS", "ORIG + (NAILDAMAGE>2 | SPICYCOMBATSKIPS)"));
            }
            if (CombatManager.Settings.SoulGain > Difficulty.Disabled)
            {
                lmb.GetOrAddTerm("SOULGAIN", TermType.Int);
                lmb.AddItem(new StringItemTemplate("Soul_Gain", "SOULGAIN++"));
                lmb.DoMacroEdit(new("AERIALMINIBOSS", "ORIG + (SOULGAIN>8 | SOULGAIN>6 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("BOSS", "ORIG + (SOULGAIN>8 | SOULGAIN>6 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("BOSSFLUKE", "ORIG + (SOULGAIN>8 | SOULGAIN>6 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("MINIBOSS", "ORIG + (SOULGAIN>6 | MILDCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("ALLBALDURS", "ORIG + (SOULGAIN>7 | MILDCOMBATSKIPS)"));
            }
            if (CombatManager.Settings.SoulPlugs > Difficulty.Disabled)
            {
                lmb.GetOrAddTerm("SOULPLUG", TermType.Int);
                lmb.AddItem(new StringItemTemplate("Soul_Plug", "SOULPLUG++"));
                lmb.DoMacroEdit(new("AERIALMINIBOSS", "ORIG + (SOULPLUG>5 | SOULPLUG>3 + MILDCOMBATSKIPS | SOULPLUG + SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("BOSS", "ORIG + (SOULPLUG>5 | SOULPLUG>3 + MILDCOMBATSKIPS | SOULPLUG + SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("BOSSFLUKE", "ORIG + (SOULPLUG>5 | SOULPLUG>3 + MILDCOMBATSKIPS | SOULPLUG + SPICYCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("MINIBOSS", "ORIG + (SOULPLUG>3 | MILDCOMBATSKIPS)"));
                lmb.DoMacroEdit(new("ALLBALDURS", "ORIG + (SOULPLUG>4 | MILDCOMBATSKIPS)"));
            }
            
            // To review best way to handle this. NOTCHES should equal total notches obtained, and not fragments.
            lmb.AddItem(new StringItemTemplate("Notch_Fragment", "NOTCHES++"));
        }
    }
}