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
            { "/scrap", HandleScrapCommand }
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
            bool state = Plugin.Instance.ConfigManager.InfiniteSprint;
            Plugin.Instance.ConfigManager.InfiniteSprint = !state;
            Plugin.mls.LogInfo($"/sprint => {!state}");
        }

        internal static void HandleSpeedCommand(string[] commandInfo)
        {
            if (commandInfo.Length == 0 || commandInfo == null)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the speed");
                return;
            }

            if (float.TryParse(commandInfo[1], out float speed))
            {
                Plugin.Instance.ConfigManager.MovementSpeed = speed;
                Plugin.mls.LogInfo($"/speed => {speed}");
            } 
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleJumpCommand(string[] commandInfo)
        {
            if (commandInfo.Length == 0 || commandInfo == null)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the jump force");
                return;
            }

            if (float.TryParse(commandInfo[1], out float jump))
            {
                Plugin.Instance.ConfigManager.JumpForce = jump;
                Plugin.mls.LogInfo($"/jump => {jump}");
            }
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleClimbCommand(string[] commandInfo)
        {
            if (commandInfo.Length == 0 || commandInfo == null)
            {
                Plugin.mls.LogInfo("Invalid command. Please specify the climb speed");
                return;
            }

            if (float.TryParse(commandInfo[1], out float speed))
            {
                Plugin.Instance.ConfigManager.ClimbSpeed = speed;
                Plugin.mls.LogInfo($"/climb => {speed}");
            }
            else
            {
                Plugin.mls.LogInfo("Invalid number");
            }
        }

        internal static void HandleScrapCommand(string[] commandInfo)
        {
            if (commandInfo.Length == 0 || commandInfo == null)
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
    }
}
