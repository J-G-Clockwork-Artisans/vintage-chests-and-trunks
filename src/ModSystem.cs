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
        private Harmony? harmonyClient;
        private Harmony? harmonyServer;

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            
            // Register VintageLabeledTrunk block entity class to reuse the vanilla LabeledChest entity
            api.RegisterBlockEntityClass("VintageLabeledTrunk", typeof(BlockEntityLabeledChest));

            // Apply BlockMultiblock and BlockGenericTypedContainer patches on the server side to ensure placedPriorityInteract is respected for chalk/pigments
            if (api.Side == EnumAppSide.Server)
            {
                harmonyServer = new Harmony(Mod.Info.ModID + "_server");
                
                // Patch BlockMultiblock
                var originalMB = typeof(BlockMultiblock).GetMethod("OnLoaded", BindingFlags.Public | BindingFlags.Instance);
                var postfixMB = typeof(BlockMultiblockPatch).GetMethod("Postfix", BindingFlags.Public | BindingFlags.Static);
                if (originalMB != null && postfixMB != null)
                {
                    harmonyServer.Patch(originalMB, postfix: new HarmonyMethod(postfixMB));
                }

                // Patch BlockGenericTypedContainer
                var originalGTC = typeof(BlockGenericTypedContainer).GetMethod("OnLoaded", BindingFlags.Public | BindingFlags.Instance);
                var postfixGTC = typeof(BlockGenericTypedContainerPatch).GetMethod("Postfix", BindingFlags.Public | BindingFlags.Static);
                if (originalGTC != null && postfixGTC != null)
                {
                    harmonyServer.Patch(originalGTC, postfix: new HarmonyMethod(postfixGTC));
                }
            }
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            if (harmonyClient == null)
            {
                harmonyClient = new Harmony(Mod.Info.ModID + "_client");
                harmonyClient.PatchAll(Assembly.GetExecutingAssembly());
            }
        }

        public override void Dispose()
        {
            if (harmonyClient != null)
            {
                harmonyClient.UnpatchAll(harmonyClient.Id);
                harmonyClient = null;
            }
            if (harmonyServer != null)
            {
                harmonyServer.UnpatchAll(harmonyServer.Id);
                harmonyServer = null;
            }
            base.Dispose();
        }
    }
}
