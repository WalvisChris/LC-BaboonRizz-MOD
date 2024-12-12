using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaboonRizzMod
{
    [BepInPlugin("BaboonRizzMod.Chris", "Baboon Rizz Mod", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("BaboonRizzMod.Chris");
        internal static Plugin Instance;
        internal static ManualLogSource mls;
        internal ConfigurationController ConfigManager;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            mls = BepInEx.Logging.Logger.CreateLogSource("Baboon Rizz Mod");
            mls.LogInfo("Loaded Succesfully");

            ConfigManager = new ConfigurationController(Config);

            harmony.PatchAll(typeof(GamePatcher));
        }
    }
}
