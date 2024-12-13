using BepInEx.Logging;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaboonRizzMod
{
    internal class Utilities
    {
        private static readonly Dictionary<string, Action<string[]>> CommandHandlers = new Dictionary<string, Action<string[]>>()
        {
            { "/sprint", HandleSprintCommand },
            { "/speed", HandleSpeedCommand },
            { "/jump", HandleJumpCommand },
            { "/climb", HandleClimbCommand },
            { "/scrap", HandleScrapCommand },
            { "/health", HandleHealthCommand },
            { "/door", HandleDoorCommand },
        };

        internal static bool HandleCommand(string command)
        {
            const string Prefix = "/";

            if (!command.ToLower().StartsWith(Prefix.ToLower())) { return false; }

            string[] commandArguments = command.ToLower().Split(' ');
            command = commandArguments[0];

            if (CommandHandlers.TryGetValue(command, out var handler))
            {
                handler(commandArguments);
                return true;
            }

            Plugin.mls.LogInfo($"Unknown command: {command}");
            return true;
        }

        internal static void HandleSprintCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowSprintCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            bool state = Plugin.Instance.ConfigManager.InfiniteSprint;
            Plugin.Instance.ConfigManager.InfiniteSprint = !state;
            Plugin.mls.LogInfo($"/sprint => {!state}");
        }

        internal static void HandleSpeedCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowSpeedCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the speed");
                return;
            }

            if (float.TryParse(commandInfo[1], out float speed))
            {
                Plugin.Instance.ConfigManager.MovementSpeed = speed;
                GamePatcher.player.movementSpeed = speed;
                Plugin.mls.LogInfo($"/speed => {speed}");
            } 
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleJumpCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowJumpCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the jump force");
                return;
            }

            if (float.TryParse(commandInfo[1], out float jump))
            {
                Plugin.Instance.ConfigManager.JumpForce = jump;
                GamePatcher.player.jumpForce = jump;
                Plugin.mls.LogInfo($"/jump => {jump}");
            }
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleClimbCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowClimbCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the climb speed");
                return;
            }

            if (float.TryParse(commandInfo[1], out float speed))
            {
                Plugin.Instance.ConfigManager.ClimbSpeed = speed;
                GamePatcher.player.climbSpeed = speed;
                Plugin.mls.LogInfo($"/climb => {speed}");
            }
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleScrapCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowScrapCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the scrap amount");
                return;
            }

            if (int.TryParse(commandInfo[1], out int value))
            {
                var playerController = GamePatcher.player;

                if (playerController != null && playerController.currentlyHeldObjectServer != null)
                {
                    playerController.currentlyHeldObjectServer.SetScrapValue(value);
                    Plugin.mls.LogInfo($"/scrap => {value} (server)");
                }
                else if (playerController != null && playerController.currentlyHeldObject != null)
                {
                    playerController.currentlyHeldObject.SetScrapValue(value);
                    Plugin.mls.LogInfo($"/scrap => {value}");
                } 
                else
                {
                    Plugin.mls.LogInfo("No object currently held (PlayerControllerB.currentlyHeldObject = null)");
                }
            }
            else
            {
                Plugin.mls.LogInfo($"Invalid scrap value: '{value}'. Please provide a valid number.");
            }
        }
    
        internal static void HandleHealthCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowHealthCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the health amount");
                return;
            }

            if (int.TryParse(commandInfo[1], out int health))
            {
                GamePatcher.player.health = health;
                Plugin.mls.LogInfo($"/health => {health}");
            }
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }
    
        internal static void HandleDoorCommand(string[] commandInfo)
        {
            if (!Plugin.Instance.ConfigManager.AllowDoorCommand)
            {
                Plugin.mls.LogInfo($"The use of the '{commandInfo[0]}' command is disabled in the config file");
                return;
            }

            if (commandInfo.Length == 0 || commandInfo == null || commandInfo.Length < 2)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the door code");
                return;
            }

            if (GamePatcher.BigDoorsList.Count == 0)
            {
                Plugin.mls.LogInfo("doorCodesList is empty.");
            }

            var matchingObject = GamePatcher.BigDoorsList.FirstOrDefault(obj => obj.objectCode == commandInfo[1]);
            if (matchingObject != null)
            {
                matchingObject.SetDoorOpenServerRpc(true);
                Plugin.mls.LogInfo($"Door {commandInfo[1]} opened");
            }
            else
            {
                Plugin.mls.LogInfo($"Door {commandInfo[1]} is not present");
            }
        }
    }
}
