using CombatRandomizer.Manager;
using MenuChanger;
using MenuChanger.Extensions;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using RandomizerMod.Menu;
using UnityEngine;

namespace CombatRandomizer.Settings
{
    public class ConnectionMenu 
    {
        // Top-level definitions
        internal static ConnectionMenu Instance { get; private set; }
        private readonly SmallButton pageRootButton;

        // Menu page and elements
        private readonly MenuPage combatPage;
        private MenuElementFactory<CombatSettings> topLevelElementFactory;

        // Individual setting pages
        private SmallButton nailPageButton;
        private MenuElementFactory<NailSettings> nailEF;
        private MenuElementFactory<NailDamageSettings> nailDamageEF;
        private SmallButton notchPageButton;
        private MenuElementFactory<NotchSettings> notchEF;
        private MenuElementFactory<NotchFragmentSettings> notchFragmentEF;
        private SmallButton gainPageButton;
        private MenuElementFactory<GainSettings> gainEF;
        private MenuElementFactory<SoulGainSettings> soulGainEF;
        private SmallButton drainPageButton;
        private MenuElementFactory<DrainSettings> drainEF;
        private MenuElementFactory<SoulDrainSettings> soulDrainEF;

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(ConstructMenu, HandleButton);
            MenuChangerMod.OnExitMainMenu += () => Instance = null;
        }

        private static bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            button = Instance.pageRootButton;
            button.Text.color = CombatManager.Settings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            return true;
        }

        private static void ConstructMenu(MenuPage connectionPage)
        {
            Instance = new(connectionPage);
        }

        private ConnectionMenu(MenuPage connectionPage)
        {
            // Define connection page
            combatPage = new MenuPage("combatPage", connectionPage);
            topLevelElementFactory = new(combatPage, CombatManager.Settings);

             // Define buttons
            nailPageButton = new(combatPage, "Nail Damage");
            nailPageButton.AddHideAndShowEvent(NailPage());

             // Define buttons
            notchPageButton = new(combatPage, "Notch Fragments");
            notchPageButton.AddHideAndShowEvent(NotchPage());

             // Define buttons
            gainPageButton = new(combatPage, "Soul Gain");
            gainPageButton.AddHideAndShowEvent(SoulGainPage());

             // Define buttons
            drainPageButton = new(combatPage, "Soul Drain");
            drainPageButton.AddHideAndShowEvent(SoulDrainPage());

            VerticalItemPanel topLevelPanel = new(combatPage, new Vector2(0, 400), 60, true, [
                topLevelElementFactory.ElementLookup[nameof(CombatSettings.Enabled)],
                nailPageButton,
                notchPageButton,
                gainPageButton,
                drainPageButton
                ]);
            topLevelElementFactory.ElementLookup[nameof(CombatSettings.Enabled)].SelfChanged += EnableSwitch;
            topLevelPanel.ResetNavigation();
            topLevelPanel.SymSetNeighbor(Neighbor.Down, combatPage.backButton);
            topLevelPanel.SymSetNeighbor(Neighbor.Up, combatPage.backButton);

            pageRootButton = new SmallButton(connectionPage, "Combat Randomizer");
            pageRootButton.AddHideAndShowEvent(connectionPage, combatPage);

            
        }

        private MenuPage NailPage()
        {
            MenuPage nailPage = new("Nail Damage", combatPage);
            MenuLabel header = new(nailPage, "Nail Damage");
            nailEF = new(nailPage, CombatManager.Settings.NailSettings);
            nailDamageEF = new(nailPage, CombatManager.Settings.NailSettings.NailDamageSettings);
            MenuPreset<NailDamageSettings> nailPresets = new(nailPage, "Difficulty", NailPresets.Presets, CombatManager.Settings.NailSettings.NailDamageSettings, _ => "", nailDamageEF);

            VerticalItemPanel itemPanel = new(nailPage, new Vector2(0, 350), 75, false, [
                header,
                nailEF.ElementLookup[nameof(NailSettings.Enabled)],
                nailPresets,
                nailDamageEF.ElementLookup[nameof(NailDamageSettings.BaseDamage)],
                nailDamageEF.ElementLookup[nameof(NailDamageSettings.NailItems)],
                nailEF.ElementLookup[nameof(NailSettings.LimitNailDamage)]
            ]);
            nailEF.ElementLookup[nameof(NailSettings.Enabled)].SelfChanged += EnableSwitch;

            itemPanel.ResetNavigation();
            itemPanel.SymSetNeighbor(Neighbor.Up, nailPage.backButton);
            itemPanel.SymSetNeighbor(Neighbor.Down, nailPage.backButton);
            
            return nailPage;
        }

        private MenuPage NotchPage()
        {
            MenuPage notchPage = new("Notch Fragments", combatPage);
            MenuLabel header = new(notchPage, "Notch Fragments");
            notchEF = new(notchPage, CombatManager.Settings.NotchSettings);
            notchFragmentEF = new(notchPage, CombatManager.Settings.NotchSettings.NotchFragmentSettings);
            MenuPreset<NotchFragmentSettings> notchPresets = new(notchPage, "Difficulty", NotchPresets.Presets, CombatManager.Settings.NotchSettings.NotchFragmentSettings, _ => "", notchFragmentEF);

            VerticalItemPanel itemPanel = new(notchPage, new Vector2(0, 350), 75, false, [
                header,
                notchEF.ElementLookup[nameof(NotchSettings.Enabled)],
                notchPresets,
                notchFragmentEF.ElementLookup[nameof(NotchFragmentSettings.FragmentsPerNotch)],
                notchFragmentEF.ElementLookup[nameof(NotchFragmentSettings.MaxNotches)],
            ]);
            notchEF.ElementLookup[nameof(NotchSettings.Enabled)].SelfChanged += EnableSwitch;

            itemPanel.ResetNavigation();
            itemPanel.SymSetNeighbor(Neighbor.Up, notchPage.backButton);
            itemPanel.SymSetNeighbor(Neighbor.Down, notchPage.backButton);
            
            return notchPage;
        }

        private MenuPage SoulGainPage()
        {
            MenuPage gainPage = new("Soul Gain", combatPage);
            MenuLabel header = new(gainPage, "Soul Gain");
            gainEF = new(gainPage, CombatManager.Settings.GainSettings);
            soulGainEF = new(gainPage, CombatManager.Settings.GainSettings.SoulGainSettings);
            MenuPreset<SoulGainSettings> gainPresets = new(gainPage, "Difficulty", SoulGainPresets.Presets, CombatManager.Settings.GainSettings.SoulGainSettings, _ => "", soulGainEF);

            VerticalItemPanel itemPanel = new(gainPage, new Vector2(0, 350), 75, false, [
                header,
                gainEF.ElementLookup[nameof(GainSettings.Enabled)],
                gainPresets,
                soulGainEF.ElementLookup[nameof(SoulGainSettings.BaseGain)],
                soulGainEF.ElementLookup[nameof(SoulGainSettings.SoulGainItems)],
            ]);
            gainEF.ElementLookup[nameof(GainSettings.Enabled)].SelfChanged += EnableSwitch;

            itemPanel.ResetNavigation();
            itemPanel.SymSetNeighbor(Neighbor.Up, gainPage.backButton);
            itemPanel.SymSetNeighbor(Neighbor.Down, gainPage.backButton);
            
            return gainPage;
        }

        private MenuPage SoulDrainPage()
        {
            MenuPage drainPage = new("Soul Drain", combatPage);
            MenuLabel header = new(drainPage, "Soul Drain");
            drainEF = new(drainPage, CombatManager.Settings.DrainSettings);
            soulDrainEF = new(drainPage, CombatManager.Settings.DrainSettings.SoulDrainSettings);

            MenuPreset<SoulDrainSettings> drainPresets = new(drainPage, "Difficulty", SoulDrainPresets.Presets, CombatManager.Settings.DrainSettings.SoulDrainSettings, _ => "", soulDrainEF);

            VerticalItemPanel itemPanel = new(drainPage, new Vector2(0, 350), 75, false, [
                header,
                drainEF.ElementLookup[nameof(DrainSettings.Enabled)],
                drainPresets,
                soulDrainEF.ElementLookup[nameof(SoulDrainSettings.BaseDrain)],
                soulDrainEF.ElementLookup[nameof(SoulDrainSettings.PlugItems)],
            ]);
            drainEF.ElementLookup[nameof(DrainSettings.Enabled)].SelfChanged += EnableSwitch;

            itemPanel.ResetNavigation();
            itemPanel.SymSetNeighbor(Neighbor.Up, drainPage.backButton);
            itemPanel.SymSetNeighbor(Neighbor.Down, drainPage.backButton);
            
            return drainPage;
        }


        // Define parameter changes
        private void EnableSwitch(IValueElement obj)
        {
            pageRootButton.Text.color = CombatManager.Settings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            nailPageButton.Text.color = CombatManager.Settings.NailSettings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            notchPageButton.Text.color = CombatManager.Settings.NotchSettings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            gainPageButton.Text.color = CombatManager.Settings.GainSettings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
            drainPageButton.Text.color = CombatManager.Settings.DrainSettings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
        }

        // Apply proxy settings
        public void Disable()
        {
            IValueElement elem = topLevelElementFactory.ElementLookup[nameof(CombatSettings.Enabled)];
            elem.SetValue(false);
        }

        public void Apply(CombatSettings settings)
        {
            topLevelElementFactory.SetMenuValues(settings);
            nailEF.SetMenuValues(settings.NailSettings);
            nailDamageEF.SetMenuValues(settings.NailSettings.NailDamageSettings);
            notchEF.SetMenuValues(settings.NotchSettings);
            notchFragmentEF.SetMenuValues(settings.NotchSettings.NotchFragmentSettings);
            gainEF.SetMenuValues(settings.GainSettings);
            soulGainEF.SetMenuValues(settings.GainSettings.SoulGainSettings);
            drainEF.SetMenuValues(settings.DrainSettings);   
            soulDrainEF.SetMenuValues(settings.DrainSettings.SoulDrainSettings);   
        }
    }
}