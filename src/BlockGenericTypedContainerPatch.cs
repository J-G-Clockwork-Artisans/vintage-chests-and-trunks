using System;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace VintageChestsAndTrunks
{
    [HarmonyPatch(typeof(BlockGenericTypedContainer), "OnLoaded")]
    public static class BlockGenericTypedContainerPatch
    {
        public static void Postfix(BlockGenericTypedContainer __instance)
        {
            if (__instance.Code != null && __instance.Code.Domain == "vintagechestsandtrunks" && __instance.Code.Path.Contains("labeledtrunk"))
            {
                __instance.PlacedPriorityInteract = true;
            }
        }
    }
}
