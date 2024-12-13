using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaboonRizzMod
{
    internal class ConfigurationController
    {
        private ConfigEntry<float> MovementSpeedCfg;
        private ConfigEntry<bool> InfiniteSprintCfg;
        private ConfigEntry<float> JumpForceCfg;
        private ConfigEntry<float> ClimbSpeedCfg;
        private ConfigEntry<bool> AllowSprintCommandCfg;
        private ConfigEntry<bool> AllowSpeedCommandCfg;
        private ConfigEntry<bool> AllowJumpCommandCfg;
        private ConfigEntry<bool> AllowClimbCommandCfg;
        private ConfigEntry<bool> AllowScrapCommandCfg;
        private ConfigEntry<bool> AllowHealthCommandCfg;
        private ConfigEntry<bool> AllowDoorCommandCfg;

        internal float MovementSpeed { get => MovementSpeedCfg.Value; set => MovementSpeedCfg.Value = value;}
        internal bool InfiniteSprint { get => InfiniteSprintCfg.Value; set =>  InfiniteSprintCfg.Value = value;}
        internal float JumpForce { get => JumpForceCfg.Value; set => JumpForceCfg.Value = value;}
        internal float ClimbSpeed { get => ClimbSpeedCfg.Value; set => ClimbSpeedCfg.Value = value; }
        internal bool AllowSprintCommand { get => AllowSprintCommandCfg.Value; set => AllowSprintCommandCfg.Value = value; }
        internal bool AllowSpeedCommand { get => AllowSpeedCommandCfg.Value; set => AllowSpeedCommandCfg.Value = value; }
        internal bool AllowJumpCommand { get => AllowJumpCommandCfg.Value; set => AllowJumpCommandCfg.Value = value; }
        internal bool AllowClimbCommand { get => AllowClimbCommandCfg.Value; set => AllowClimbCommandCfg.Value = value; }
        internal bool AllowScrapCommand { get => AllowScrapCommandCfg.Value; set => AllowScrapCommandCfg.Value = value; }
        internal bool AllowHealthCommand { get => AllowHealthCommandCfg.Value; set => AllowHealthCommandCfg.Value = value; }
        internal bool AllowDoorCommand { get => AllowDoorCommandCfg.Value; set => AllowDoorCommandCfg.Value = value; }


        public ConfigurationController(ConfigFile config)
        {
            // Values
            MovementSpeedCfg = config.Bind("Options", "Set Custom Movement Speed", 4.6f);
            InfiniteSprintCfg = config.Bind("Options", "Enable Infinite Sprint", false);
            JumpForceCfg = config.Bind("Options", "Set Custom Jump Force", 13f);
            ClimbSpeedCfg = config.Bind("Options", "Set Custom Climb Speed", 3f);
            // Chat commands
            AllowSprintCommandCfg = config.Bind("Chat Commands", "Enable Infinite Sprint Command /sprint", true);
            AllowSpeedCommandCfg = config.Bind("Chat Commands", "Enable Movement Speed Command /speed <value>", true);
            AllowJumpCommandCfg = config.Bind("Chat Commands", "Enable Jump Force Command /jump <value>", true);
            AllowClimbCommandCfg = config.Bind("Chat Commands", "Enable Climb Speed Command /climb <value>", true);
            AllowScrapCommandCfg = config.Bind("Chat Commands", "Enable Scrap Value Command /scrap <value>", true);
            AllowHealthCommandCfg = config.Bind("Chat Commands", "Enable Player Health Command /health <value>", true);
            AllowDoorCommandCfg = config.Bind("Chat Commands", "Enable Open Door Command /door <code>", true);
        }
    }
}