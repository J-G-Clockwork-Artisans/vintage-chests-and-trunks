using System;
using System.Reflection;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

namespace VintageChestsAndTrunks
{
    public class VintageChestsAndTrunksModSystem : ModSystem
    {
        private Harmony? harmony;

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            
            // Register VintageLabeledTrunk block entity class to reuse the vanilla LabeledChest entity
            api.RegisterBlockEntityClass("VintageLabeledTrunk", typeof(BlockEntityLabeledChest));
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            if (!Harmony.HasAnyPatches(Mod.Info.ModID))
            {
                harmony = new Harmony(Mod.Info.ModID);
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
        }

        public override void Dispose()
        {
            if (harmony != null)
            {
                harmony.UnpatchAll(Mod.Info.ModID);
                harmony = null;
            }
            base.Dispose();
        }
    }
}
