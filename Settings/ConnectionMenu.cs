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
            VerticalItemPanel topLevelPanel = new(combatPage, new Vector2(0, 400), 60, true, topLevelElementFactory.Elements); 
            topLevelElementFactory.ElementLookup[nameof(CombatSettings.Enabled)].SelfChanged += EnableSwitch;
            topLevelPanel.ResetNavigation();
            topLevelPanel.SymSetNeighbor(Neighbor.Down, combatPage.backButton);
            topLevelPanel.SymSetNeighbor(Neighbor.Up, combatPage.backButton);
            pageRootButton = new SmallButton(connectionPage, "Combat Randomizer");
            pageRootButton.AddHideAndShowEvent(connectionPage, combatPage);
        }
        // Define parameter changes
        private void EnableSwitch(IValueElement obj)
        {
            pageRootButton.Text.color = CombatManager.Settings.Enabled ? Colors.TRUE_COLOR : Colors.DEFAULT_COLOR;
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
        }
    }
}