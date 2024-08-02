using CombatRandomizer.Interop;
using CombatRandomizer.Manager;
using CombatRandomizer.Settings;
using Modding;
using System;

namespace CombatRandomizer
{
    public class CombatRandomizer : Mod, IGlobalSettings<CombatSettings> 
    {
        new public string GetName() => "CombatRandomizer";
        public override string GetVersion() => "1.0.0.0";

        private static CombatRandomizer _instance;
        public CombatRandomizer() : base()
        {
            _instance = this;
        }
        internal static CombatRandomizer Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new InvalidOperationException($"{nameof(CombatRandomizer)} was never initialized");
                }
                return _instance;
            }
        }
        public CombatSettings GS { get; internal set; } = new();
        public override void Initialize()
        {
            // Ignore completely if Randomizer 4 is inactive
            if (ModHooks.GetMod("Randomizer 4") is Mod)
            {
                Instance.Log("Initializing...");
                CombatManager.Hook();
                
                if (ModHooks.GetMod("RandoSettingsManager") is Mod)
                    RSM_Interop.Hook();
                
                Instance.Log("Initialized.");


            }
        }
        public void OnLoadGlobal(CombatSettings s) => GS = s;
        public CombatSettings OnSaveGlobal() => GS;
    }   
}