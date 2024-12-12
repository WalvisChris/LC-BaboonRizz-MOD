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

        internal float MovementSpeed { get => MovementSpeedCfg.Value; set => MovementSpeedCfg.Value = value;}
        internal bool InfiniteSprint { get => InfiniteSprintCfg.Value; set =>  InfiniteSprintCfg.Value = value;}
        internal float JumpForce { get => JumpForceCfg.Value; set => JumpForceCfg.Value = value;}
        internal float ClimbSpeed { get => ClimbSpeedCfg.Value; set => ClimbSpeedCfg.Value = value; }

        public ConfigurationController(ConfigFile config)
        {
            MovementSpeedCfg = config.Bind("Options", "Set Custom Movement Speed", 5f);
            InfiniteSprintCfg = config.Bind("Options", "Enable Infinite Sprint", false);
            JumpForceCfg = config.Bind("Options", "Set Custom Jump Force", 10f);
            ClimbSpeedCfg = config.Bind("Options", "Set Custom Climb Speed", 3f);
        }
    }
}
