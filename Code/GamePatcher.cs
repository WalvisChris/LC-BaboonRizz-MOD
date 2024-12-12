using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameNetcodeStuff;
using HarmonyLib;

namespace BaboonRizzMod
{
    [HarmonyPatch]
    internal class GamePatcher
    {
        // CHAT PATCH

        [HarmonyPatch(typeof(HUDManager), "SubmitChat_performed")]
        [HarmonyPrefix]
        static void HUDPatch(ref HUDManager __instance)
        {
            try
            {
                if (Utilities.HandleCommand(__instance.chatTextField.text))
                {
                    __instance.chatTextField.text = "";
                }
            }
            catch (Exception e)
            {
                Plugin.mls.LogInfo($"Unable to handle command {e.Message}");
            }
        }

        // MOVEMENT PATCH + SCRAP PATCH

        [HarmonyPatch(typeof(PlayerControllerB), "Update")]
        [HarmonyPrefix]
        static void MovementPatch(ref PlayerControllerB __instance)
        {
            player = __instance;
            
            if (Plugin.Instance.ConfigManager.InfiniteSprint && __instance.sprintMeter < 1f)
            {
                __instance.sprintMeter = 1f;
            }
            __instance.movementSpeed = Plugin.Instance.ConfigManager.MovementSpeed;
            __instance.jumpForce = Plugin.Instance.ConfigManager.JumpForce;
            __instance.climbSpeed = Plugin.Instance.ConfigManager.ClimbSpeed;
        }

        internal static PlayerControllerB player;
    }
}
