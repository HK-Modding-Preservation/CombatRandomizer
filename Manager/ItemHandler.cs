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
                nailItems = 14;
            if (nailDiff == Difficulty.Extreme)
                nailItems = 11;
            rb.AddItemByName("Nail_Damage", nailItems);
            rb.EditItemRequest("Nail_Damage", info => {
                info.getItemDef = () => new() 
                {
                    Name = "Nail_Damage",
                    Pool = "Nail Upgrades",
                    PriceCap = 500,
                    MajorItem = false
                };
            });

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
                rb.EditItemRequest("Notch_Fragment", info => {
                    info.getItemDef = () => new() 
                    {
                        Name = "Notch_Fragment",
                        Pool = "Charm Notches",
                        PriceCap = 500,
                        MajorItem = false
                    };
                });
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
                rb.EditItemRequest("Soul_Gain", info => {
                    info.getItemDef = () => new() 
                    {
                        Name = "Soul_Gain",
                        Pool = "Soul Upgrades",
                        PriceCap = 500,
                        MajorItem = false
                    };
                });
            };

            // Soul Plug: 1 + difficulty tier
            Difficulty drainDiff = CombatManager.Settings.SoulPlugs;
            int soulPlugItems = 1 + (int)drainDiff;
            rb.AddItemByName("Soul_Plug", soulPlugItems);
            rb.EditItemRequest("Soul_Plug", info => {
                info.getItemDef = () => new() 
                {
                    Name = "Soul_Plug",
                    Pool = "Soul Upgrades",
                    PriceCap = 2000,
                    MajorItem = false
                };
            });
        }
            

        private static void SetPI(LogicManager lm, GenerationSettings gs, ProgressionInitializer pi)
        {
            if (!CombatManager.Settings.Enabled)
                return;
            
            // Nail damage: (21 - available items) for starting PI to check for Nail Upgrade logic.
            int nailDamage = 5;
            if (CombatManager.Settings.NailDamage == Difficulty.Disabled)
                nailDamage = 21;
            if (CombatManager.Settings.NailDamage == Difficulty.Intermediate)
                nailDamage = 3;
            if (CombatManager.Settings.NailDamage == Difficulty.Hard)
                nailDamage = 7;
            if (CombatManager.Settings.NailDamage == Difficulty.Extreme)
                nailDamage = 10;

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
            
            // Maximum is 5 - which means all Soul Plug items have been obtained.
            int soulPlug = 4 - (int)CombatManager.Settings.SoulPlugs;

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