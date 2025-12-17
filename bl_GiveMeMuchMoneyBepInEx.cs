using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.Mono;
using HarmonyLib;
using SemanticVersioning;
using System;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine;

namespace BitchlandGiveMuchMoneyBepInWx
{
    [BepInPlugin("com.wolfitdm.GiveMeMuchMoneyBitchlandBepInEx", "GiveMeMuchMoneyBitchlandBepInEx Plugin", "1.0.0.0")]
    public class GiveMeMuchMoneyBitchlandBepInEx : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger;

        private ConfigEntry<bool> configEnableMe;

        public GiveMeMuchMoneyBitchlandBepInEx()
        {
        }

        public static Type MyGetType(string originalClassName)
        {
            return Type.GetType(originalClassName + ",Assembly-CSharp");
        }

        private static string pluginKey = "General.Toggles";

        public static bool enableThisMod = false;

        private void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            configEnableMe = Config.Bind(pluginKey,
                                              "EnableThisMod",
                                              true,
                                             "Whether or not you want enable this mod (default true also yes, you want it, and false = no)");


            enableThisMod = configEnableMe.Value;

            Harmony.CreateAndPatchAll(typeof(GiveMeMuchMoneyBitchlandBepInEx));

            Logger.LogInfo($"Plugin GiveMeMuchMoneyForBitchLand BepInEx is loaded!");
        }

        [HarmonyPatch(typeof(job_ArmyBuildingWork), "Chat_ReceptionGuard")]
        [HarmonyPostfix] // call after the original method is called
        public static void job_ArmyBuildingWork_Chat_ReceptionGuard(object __instance)
        {
            Logger.LogInfo("This method is called after the original method is called!");
            if (!enableThisMod)
            {
                return;
            }

            job_ArmyBuildingWork _this = (job_ArmyBuildingWork)__instance;
            UI_Gameplay _gameplay = Main.Instance.GameplayMenu;
            Person person = _gameplay.PersonChattingTo;
            _gameplay.AddChatOption("Give Me 90 Mio Cash", (Action)(() =>
            {
                Main.Instance.GameplayMenu.EnableMove();
                Main.Instance.Player.Money += 90000000;
            }));
            return;
        }


        [HarmonyPatch(typeof(job_StripClub), "StripChat")]
        [HarmonyPostfix] // call after the original method is called
        public static void job_StripClub_StripChat(object __instance)
        {
            Logger.LogInfo("This method is called after the original method is called!");
            if (!enableThisMod)
            {
                return;
            }

            job_StripClub _this = (job_StripClub)__instance;
            UI_Gameplay _gameplay = Main.Instance.GameplayMenu;
            Person person = _gameplay.PersonChattingTo;
            _gameplay.AddChatOption("Give Me 90 Mio Cash", (Action)(() =>
            {
                Main.Instance.GameplayMenu.EnableMove();
                Main.Instance.Player.Money += 90000000;
            }));
            return;
        }

        [HarmonyPatch(typeof(job_StripClub), "StripChat")]
        [HarmonyPrefix] // Call before the original method
        public static bool blalalalalal(object __instance)
        {
            Logger.LogInfo("This method is called before the original StripChat method is called from the class job_StripClub!");
            return true; // true = call the original method, false = do not call the original method
        }
    }
}
