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

            // Nail Damage items.
            Difficulty nailDiff = CombatManager.Settings.NailDamage;
            int nailItems = 0;
            if (nailDiff == Difficulty.Easy)
                nailItems = 20;
            if (nailDiff == Difficulty.Standard)
                nailItems = 16;
            if (nailDiff == Difficulty.Intermediate)
                nailItems = 18;
            if (nailDiff == Difficulty.Hard)
                nailItems = 15;
            if (nailDiff == Difficulty.Extreme)
                nailItems = 12;
            rb.AddItemByName("Nail_Damage", nailItems);

            // Replace Charm Notches with Notch Fragments
            if (CombatManager.Settings.NotchFragments > Difficulty.Disabled)
            {
                rb.RemoveItemByName(ItemNames.Charm_Notch);
                int totalNotches = 8 + rb.gs.CursedSettings.CursedNotches;
                int fragmentsPerNotch = Math.Min((int)CombatManager.Settings.NotchFragments + 2, 4);
                // Max 9 notches
                if (CombatManager.Settings.NotchFragments == Difficulty.Hard)
                    totalNotches -= 2;
                // Max 6 notches
                if (CombatManager.Settings.NotchFragments == Difficulty.Extreme)
                    totalNotches -= 5;
                int notchFragments = totalNotches * fragmentsPerNotch;
                rb.AddItemByName("Notch_Fragment", notchFragments);
            }

            // Soul Gain: 7 if Intermediate else 5.
            Difficulty gainDiff = CombatManager.Settings.SoulGain;
            if (gainDiff > Difficulty.Disabled)
            {
                int soulGainItems;
                if (gainDiff == Difficulty.Intermediate)
                    soulGainItems = 7;
                else
                    soulGainItems = 5;
                rb.AddItemByName("Soul_Gain", soulGainItems);
            }

            // Soul Plug: 1 if Easy, 3 if Normal, 5 if Hard/Extreme.
            Difficulty drainDiff = CombatManager.Settings.SoulPlugs;
            int soulPlugItems = 0;
            if (drainDiff == Difficulty.Easy)
                soulPlugItems = 1;
            if (drainDiff == Difficulty.Standard)
                soulPlugItems = 3;
            if (drainDiff >= Difficulty.Hard)
                soulPlugItems = 5;
            rb.AddItemByName("Soul_Plug", soulPlugItems);
        }

        private static void SetPI(LogicManager lm, GenerationSettings gs, ProgressionInitializer pi)
        {
            if (!CombatManager.Settings.Enabled)
                return;
            
            // Nail damage: (21 - max damage) + base damage for starting PI to check for Nail Upgrade logic.
            int nailDamage = 5;
            if (CombatManager.Settings.NailDamage == Difficulty.Disabled)
                nailDamage = 21;
            if (CombatManager.Settings.NailDamage == Difficulty.Intermediate)
                nailDamage = 3;
            if (CombatManager.Settings.NailDamage == Difficulty.Hard)
                nailDamage = 6;
            if (CombatManager.Settings.NailDamage == Difficulty.Extreme)
                nailDamage = 9;

            // (11 - max gain) + base gain for starting PI for Soul Gain.
            int soulGain = 11;
            if (CombatManager.Settings.SoulGain == Difficulty.Easy)
                soulGain = 8;
            if (CombatManager.Settings.SoulGain == Difficulty.Standard)
                soulGain = 6;
            if (CombatManager.Settings.SoulGain == Difficulty.Intermediate)
                soulGain = 4; 
            if (CombatManager.Settings.SoulGain == Difficulty.Hard)
                soulGain = 6;
            if (CombatManager.Settings.SoulGain == Difficulty.Extreme)
                soulGain = 6;
            
            // Maximum is 7 - which means no soul loss. Starts at 7 - setting drain.
            int soulPlug = 7;
            if (CombatManager.Settings.SoulPlugs == Difficulty.Easy)
                soulPlug = 6;
            if (CombatManager.Settings.SoulPlugs == Difficulty.Standard)
                soulPlug = 5;
            if (CombatManager.Settings.SoulPlugs == Difficulty.Intermediate)
                soulPlug = 4;
            if (CombatManager.Settings.SoulPlugs == Difficulty.Hard)
                soulPlug = 3;
            if (CombatManager.Settings.SoulPlugs == Difficulty.Extreme)
                soulPlug = 2;

            pi.Setters.Add(new(lm.GetTermStrict("NAILDAMAGE"), nailDamage));
            pi.Setters.Add(new(lm.GetTermStrict("SOULGAIN"), soulGain));
            pi.Setters.Add(new(lm.GetTermStrict("SOULPLUG"), soulPlug));

            // Force remove spell skips as soul state is virtually impossible to track w/ soul draining active.
            if (CombatManager.Settings.SoulPlugs > Difficulty.Disabled)
            {
                pi.Setters.Add(new(lm.GetTerm("FIREBALLSKIPS"), 0));
                pi.Setters.Add(new(lm.GetTerm("SLOPEBALLSKIPS"), 0));
                pi.Setters.Add(new(lm.GetTerm("SHRIEKPOGOSKIPS"), 0));
            }
        }
    }
}