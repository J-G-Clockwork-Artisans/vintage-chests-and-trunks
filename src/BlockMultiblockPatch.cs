using System;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace VintageChestsAndTrunks
{
    [HarmonyPatch(typeof(BlockMultiblock), "OnLoaded")]
    public static class BlockMultiblockPatch
    {
        public static void Postfix(BlockMultiblock __instance)
        {
            __instance.PlacedPriorityInteract = true;
        }
    }
}
