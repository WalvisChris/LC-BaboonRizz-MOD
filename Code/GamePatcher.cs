using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx.Logging;
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

        // SPRINT PATCH + PLAYER REFERENCE

        [HarmonyPatch(typeof(PlayerControllerB), "Update")]
        [HarmonyPrefix]
        static void UpdatePatch(ref PlayerControllerB __instance)
        {
            player = __instance;
            
            if (Plugin.Instance.ConfigManager.InfiniteSprint && __instance.sprintMeter < 1f)
            {
                __instance.sprintMeter = 1f;
            }
        }

        // LOADING MOVEMENT ON START

        [HarmonyPatch(typeof(PlayerControllerB), "Start")]
        [HarmonyPostfix]
        static void MovementPatch(ref PlayerControllerB __instance)
        {
            // get
            float CustomMovementSpeed = Plugin.Instance.ConfigManager.MovementSpeed;
            float CustomJumpForce = Plugin.Instance.ConfigManager.JumpForce;
            float CustomClimbSpeed = Plugin.Instance.ConfigManager.ClimbSpeed;

            // set
            __instance.movementSpeed = CustomMovementSpeed;
            __instance.jumpForce = CustomJumpForce;
            __instance.climbSpeed = CustomClimbSpeed;

            // log
            Plugin.mls.LogInfo($"Player movementSpeed loaded from config: {CustomMovementSpeed}");
            Plugin.mls.LogInfo($"Player jumpForce loaded from config: {CustomJumpForce}");
            Plugin.mls.LogInfo($"Player climbSpeed loaded from config: {CustomClimbSpeed}");
        }

        // DOOR PATCH - REMOVE DOORS ON GAME START

        [HarmonyPatch(typeof(StartOfRound), "Start")]
        [HarmonyPostfix]
        static void StartOfRoundPatch()
        {
            BigDoorsList.Clear();
            Plugin.mls.LogInfo("BigDoorsList cleared");
        }


        // DOOR PATCH - ADD DOORS

        [HarmonyPatch(typeof(TerminalAccessibleObject), "SetCodeTo")]
        [HarmonyPostfix]
        static void BigDoorPatch(ref TerminalAccessibleObject __instance)
        {
            if (__instance.isBigDoor)
            {
                BigDoorsList.Add(__instance);
            }
        }

        internal static PlayerControllerB player;

        internal static List<TerminalAccessibleObject> BigDoorsList = new List<TerminalAccessibleObject>();
    }
}