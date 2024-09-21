using System;
using CombatRandomizer.IC;
using CombatRandomizer.Settings;
using ItemChanger;
using RandomizerCore.Logic;
using RandomizerMod.RC;
using RandomizerMod.Settings;
using Steamworks;

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
            if (CombatManager.Settings.NailSettings.Enabled)
            {
                int nailItems = CombatManager.Settings.NailSettings.NailDamageSettings.NailItems;
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
            }

            // Replace Charm Notches with Notch Fragments
            if (CombatManager.Settings.NotchSettings.Enabled)
            {
                rb.RemoveItemByName(ItemNames.Charm_Notch);
                int totalNotches = CombatManager.Settings.NotchSettings.NotchFragmentSettings.MaxNotches;
                totalNotches -= 3 - rb.gs.CursedSettings.CursedNotches;
                if (totalNotches > 0)
                {
                    int fragmentsPerNotch = CombatManager.Settings.NotchSettings.NotchFragmentSettings.FragmentsPerNotch;
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
            }

            if (CombatManager.Settings.GainSettings.Enabled)
            {
                int soulGainItems = CombatManager.Settings.GainSettings.SoulGainSettings.SoulGainItems;
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

            if (CombatManager.Settings.DrainSettings.Enabled)
            {
                int soulPlugItems = CombatManager.Settings.DrainSettings.SoulDrainSettings.PlugItems;
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
        }
            

        private static void SetPI(LogicManager lm, GenerationSettings gs, ProgressionInitializer pi)
        {
            if (!CombatManager.Settings.Enabled)
                return;
            
            int nailDamage = 21 - (CombatManager.Settings.NailSettings.Enabled ? CombatManager.Settings.NailSettings.NailDamageSettings.NailItems : 0);
            int soulGain = 11 - (CombatManager.Settings.GainSettings.Enabled ? CombatManager.Settings.GainSettings.SoulGainSettings.SoulGainItems : 0);
            int soulPlug = 5 - (CombatManager.Settings.DrainSettings.Enabled ? CombatManager.Settings.DrainSettings.SoulDrainSettings.PlugItems : 0);

            pi.Setters.Add(new(lm.GetTermStrict("NAILDAMAGE"), nailDamage));
            pi.Setters.Add(new(lm.GetTermStrict("SOULGAIN"), soulGain));
            pi.Setters.Add(new(lm.GetTermStrict("SOULPLUG"), soulPlug));

            // Force remove spell skip requirements as soul state is virtually impossible to track w/ soul draining active.
            if (CombatManager.Settings.DrainSettings.Enabled & CombatManager.Settings.DrainSettings.SoulDrainSettings.BaseDrain > 0)
            {
                pi.Setters.Add(new(lm.GetTerm("FIREBALLSKIPS"), 0));
                pi.Setters.Add(new(lm.GetTerm("SLOPEBALLSKIPS"), 0));
                pi.Setters.Add(new(lm.GetTerm("SHRIEKPOGOSKIPS"), 0));
            }
        }
    }
}