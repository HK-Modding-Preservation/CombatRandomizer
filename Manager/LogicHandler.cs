using Modding;
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
            if (ModHooks.GetMod("GodhomeRandomizer") is Mod)
                RCData.RuntimeLogicOverride.Subscribe(100f, GodhomeRandoInterop);
        }

        private static void ApplyLogic(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!CombatManager.Settings.Enabled)
                return;

            lmb.GetOrAddTerm("NAILDAMAGE", TermType.Int);
            lmb.AddItem(new StringItemTemplate("Nail_Damage", "NAILDAMAGE++"));

            lmb.GetOrAddTerm("SOULGAIN", TermType.Int);
            lmb.AddItem(new StringItemTemplate("Soul_Gain", "SOULGAIN++"));

            lmb.GetOrAddTerm("SOULPLUG", TermType.Int);
            lmb.AddItem(new StringItemTemplate("Soul_Plug", "SOULPLUG++"));
            
            // To review best way to handle this. NOTCHES should equal total notches obtained, and not fragments.
            lmb.AddItem(new StringItemTemplate("Notch_Fragment", "NOTCHES++"));

            // Modify combat macros
            lmb.DoMacroEdit(new("ALLBALDURS", "((FIREBALL | QUAKE) + (SOULGAIN>10 | SOULPLUG>4)) | GLOWINGWOMB | (WEAVERSONG | SPORESHROOM + FOCUS) + OBSCURESKIPS | CYCLONE + DIFFICULTSKIPS"));
            lmb.DoMacroEdit(new("AERIALMINIBOSS", "ORIG + (NAILDAMAGE>4 + SOULGAIN>8 + SOULPLUG>3 | NAILDAMAGE>4 + SOULGAIN>6 + SOULPLUG>1 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
            lmb.DoMacroEdit(new("BOSS", "ORIG + (NAILDAMAGE>4 + SOULGAIN>8 + SOULPLUG>3 | NAILDAMAGE>4 + SOULGAIN>6 + SOULPLUG>1 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
            lmb.DoMacroEdit(new("BOSSFLUKE", "ORIG + (SOULGAIN>8 + SOULPLUG>3 | NAILDAMAGE>4 + SOULGAIN>6 + SOULPLUG>1 + MILDCOMBATSKIPS | SPICYCOMBATSKIPS) | NAILDAMAGE>12 + MILDCOMBATSKIPS | NAILDAMAGE>8 + SPICYCOMBATSKIPS"));
            lmb.DoMacroEdit(new("MINIBOSS", "ORIG + (NAILDAMAGE>4 + SOULGAIN>6 + SOULPLUG>1 | NAILDAMAGE>4 + SOULPLUG + MILDCOMBATSKIPS | SPICYCOMBATSKIPS)"));
        }

        private static void GodhomeRandoInterop(GenerationSettings gs, LogicManagerBuilder lmb)
        {
            if (!CombatManager.Settings.Enabled)
                return;

            lmb.DoLogicEdit(new("Nail_Upgradable_1", "NAILDAMAGE>8"));
            lmb.DoLogicEdit(new("Nail_Upgradable_2", "NAILDAMAGE>12"));
            lmb.DoLogicEdit(new("Nail_Upgradable_3", "NAILDAMAGE>16"));
            lmb.DoLogicEdit(new("Nail_Upgradable_4", "NAILDAMAGE>20"));
        }
    }
}