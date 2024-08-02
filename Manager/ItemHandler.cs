using System;
using CombatRandomizer.IC;
using CombatRandomizer.Settings;
using ItemChanger;
using RandomizerCore.Logic;
using RandomizerMod.RC;
using RandomizerMod.Settings;

namespace CombatRandomizer.Manager {
    internal static class ItemHandler
    {
        internal static void Hook()
        {
            DefineObjects();
            ProgressionInitializer.OnCreateProgressionInitializer += SetPI;
            RequestBuilder.OnUpdate.Subscribe(-9999f, DisableSpellSkips);
            RequestBuilder.OnUpdate.Subscribe(100f, AddObjects);
        }

        public static void DefineObjects()
        {
            Finder.DefineCustomItem(new NailDamage());
            Finder.DefineCustomItem(new NotchFragment());
            Finder.DefineCustomItem(new SoulGain());
            Finder.DefineCustomItem(new SoulPlug());
        }

        private static void DisableSpellSkips(RequestBuilder rb)
        {
            if (!CombatManager.Settings.Enabled)
                return;
        }

        public static void AddObjects(RequestBuilder rb)
        {
            if (!CombatManager.Settings.Enabled)
                return;

            Difficulty difficulty = CombatManager.Settings.Difficulty;

            // Nail Damage: 16 if Normal, 20 if Easy/Hard, 9 if Extreme
            int nailItems = 0;
            if (difficulty == Difficulty.Easy)
                nailItems = 20;
            if (difficulty == Difficulty.Normal)
                nailItems = 16;
            if (difficulty == Difficulty.Hard)
                nailItems = 18;
            if (difficulty == Difficulty.Extreme)
                nailItems = 9;
            rb.AddItemByName("Nail_Damage", nailItems);

            // Replace Charm Notches with Notch Fragments
            rb.RemoveItemByName(ItemNames.Charm_Notch);
            int totalNotches = 8 + rb.gs.CursedSettings.CursedNotches;
            int fragmentsPerNotch = Math.Min((int)CombatManager.Settings.Difficulty + 2, 4);
            rb.AddItemByName("Notch_Fragment", totalNotches * fragmentsPerNotch);

            // Soul Gain: 8 if Hard, else 5.
            int soulGainItems = 0;
            if (difficulty == Difficulty.Hard)
                soulGainItems = 8;
            else
                soulGainItems = 5;
            rb.AddItemByName("Soul_Gain", soulGainItems);

            // Soul Plug: 1 if Easy, 3 if Normal, 5 if Hard/Extreme.
            int soulPlugItems = 0;
            if (difficulty == Difficulty.Easy)
                soulPlugItems = 1;
            if (difficulty == Difficulty.Normal)
                soulPlugItems = 3;
            if (difficulty >= Difficulty.Hard)
                soulPlugItems = 5;
            rb.AddItemByName("Soul_Plug", soulPlugItems);
        }

        private static void SetPI(LogicManager lm, GenerationSettings gs, ProgressionInitializer pi)
        {
            if (!CombatManager.Settings.Enabled)
                return;
            
            // Nail damage without charms
            int nailDamage = 5;
            if (CombatManager.Settings.Difficulty == Difficulty.Hard)
                nailDamage = 3;
            if (CombatManager.Settings.Difficulty == Difficulty.Extreme)
                nailDamage = 1;

            // How much soul you gain per swing without charms
            int soulGain = 0;
            if (CombatManager.Settings.Difficulty == Difficulty.Easy)
                soulGain = 8;
            if (CombatManager.Settings.Difficulty == Difficulty.Normal)
                soulGain = 6; 
            if (CombatManager.Settings.Difficulty == Difficulty.Hard)
                soulGain = 3;
            if (CombatManager.Settings.Difficulty == Difficulty.Extreme)
                soulGain = 1;
            
            // Maximum is 7 - which means no soul loss. Starts at 7 - setting drain.
            int soulPlug = 0;
            if (CombatManager.Settings.Difficulty == Difficulty.Easy)
                soulPlug = 6;
            if (CombatManager.Settings.Difficulty == Difficulty.Normal)
                soulPlug = 4; 
            if (CombatManager.Settings.Difficulty == Difficulty.Hard)
                soulPlug = 2;
            if (CombatManager.Settings.Difficulty == Difficulty.Extreme)
                soulPlug = 0;

            pi.Setters.Add(new(lm.GetTermStrict("NAILDAMAGE"), nailDamage));
            pi.Setters.Add(new(lm.GetTermStrict("SOULGAIN"), soulGain));
            pi.Setters.Add(new(lm.GetTermStrict("SOULPLUG"), soulPlug));

            // Force remove spell skips as soul state is virtually impossible to track w/ soul draining active.
            pi.Setters.Add(new(lm.GetTerm("FIREBALLSKIPS"), 0));
            pi.Setters.Add(new(lm.GetTerm("SLOPEBALLSKIPS"), 0));
            pi.Setters.Add(new(lm.GetTerm("SHRIEKPOGOSKIPS"), 0));
        }
    }
}