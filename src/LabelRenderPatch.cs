using System;
using HarmonyLib;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

namespace VintageChestsAndTrunks
{
    [HarmonyPatch(typeof(ChestLabelRenderer), "OnRenderFrame")]
    public static class LabelRenderPatch
    {
        private static bool Prefix(ChestLabelRenderer __instance, float deltaTime, EnumRenderStage stage)
        {
            // Use Traverse to safely access the private fields of ChestLabelRenderer
            Traverse traverse = Traverse.Create(__instance);
            
            ICoreClientAPI? api = traverse.Field("api").GetValue() as ICoreClientAPI;
            BlockPos? pos = traverse.Field("pos").GetValue() as BlockPos;
            Matrixf? modelMat = traverse.Field("ModelMat").GetValue() as Matrixf;

            if (api == null || pos == null || modelMat == null)
            {
                return true; // Fall back to original method if critical fields are null
            }

            Block block = api.World.BlockAccessor.GetBlock(pos);
            if (block == null || block.Code.Domain != "vintagechestsandtrunks" || !block.Code.Path.Contains("labeledtrunk"))
            {
                return true; // Not our labeled trunk, fall back to vanilla rendering
            }

            LoadedTexture? loadedTexture = traverse.Field("loadedTexture").GetValue() as LoadedTexture;
            if (loadedTexture == null)
            {
                // Generate the text texture immediately if it is null (e.g. during typing updates)
                // This prevents falling back to the vanilla renderer for 1 frame which causes left-shifted blinking
                loadedTexture = traverse.Method("RenderText").GetValue() as LoadedTexture;
                if (loadedTexture != null)
                {
                    traverse.Field("loadedTexture").SetValue(loadedTexture);
                }
                else
                {
                    return false; // Skip drawing if texture couldn't be generated
                }
            }

            MeshRef? quadModelRef = traverse.Field("quadModelRef").GetValue() as MeshRef;
            if (quadModelRef == null)
            {
                return true; // Fall back to original method if quadModelRef is still null
            }

            if (loadedTexture.TextureId == 0 || stage != EnumRenderStage.Opaque)
            {
                return false; // Skip drawing if texture isn't ready or we are in wrong render stage
            }

            IRenderAPI render = api.Render;
            IClientPlayer player = api.World.Player;
            Vec3d cameraPos = player?.Entity?.CameraPos ?? Vec3d.Zero;

            // Don't render text if the player is too far away to prevent performance waste
            if (cameraPos.SquareDistanceTo(pos.X, pos.Y, pos.Z) > 400.0)
            {
                return false;
            }

            IShaderProgram currentActiveShader = render.CurrentActiveShader;
            try
            {
                render.GlDisableCullFace();
                render.GlToggleBlend(true, EnumBlendMode.PremultipliedAlpha);

                float rotY = (float)traverse.Field("rotY").GetValue();
                float quadWidth = (float)traverse.Field("QuadWidth").GetValue();
                float quadHeight = (float)traverse.Field("QuadHeight").GetValue();

                // Prepare standard shader for block coordinates
                IStandardShaderProgram shader = render.PreparedStandardShader(pos.X, pos.Y, pos.Z, null);
                shader.Tex2D = loadedTexture.TextureId;
                
                // Translate matrix specifically for double chest (trunk) dimensions
                // Changed Z translation from 0.0925f to 0.0915f to prevent text culling at horizontal angles
                shader.ModelMatrix = modelMat.Identity()
                    .Translate(pos.X - cameraPos.X, pos.Y - cameraPos.Y, pos.Z - cameraPos.Z)
                    .Translate(0.5f, 0.5f, 0.5f)
                    .RotateY(rotY + (float)Math.PI)
                    .Translate(-0.5f, -0.5f, -0.5f)
                    .Translate(0f, 0.35f, 0.0915f) // Correct offset for label on trunk
                    .Scale(0.45f * quadWidth, 0.45f * quadHeight, 0.45f * quadWidth)
                    .Values;

                shader.ViewMatrix = render.CameraMatrixOriginf;
                shader.ProjectionMatrix = render.CurrentProjectionMatrix;
                shader.NormalShaded = 0;
                shader.ExtraGodray = 0f;
                shader.SsaoAttn = 0f;
                shader.AlphaTest = 0.05f;
                shader.OverlayOpacity = 0f;

                // Maximize light values between block position and the block in front of the trunk face
                Vec4f lightVal = api.World.BlockAccessor.GetLightRGBs(pos.X, pos.Y, pos.Z);
                BlockFacing facing = BlockFacing.FromCode(block.Variant["side"]);
                if (facing != null)
                {
                    Vec4f faceLight = api.World.BlockAccessor.GetLightRGBs(pos.X + facing.Normali.X, pos.Y + facing.Normali.Y, pos.Z + facing.Normali.Z);
                    lightVal = new Vec4f(
                        Math.Max(lightVal.X, faceLight.X),
                        Math.Max(lightVal.Y, faceLight.Y),
                        Math.Max(lightVal.Z, faceLight.Z),
                        Math.Max(lightVal.W, faceLight.W)
                    );
                }
                shader.RgbaLightIn = lightVal;
                shader.AddRenderFlags = 0;

                render.RenderMesh(quadModelRef);
            }
            catch (Exception ex)
            {
                api.Logger.Warning("[VintageChestsAndTrunks] Labeled trunk label render failed: {0}", ex);
            }
            finally
            {
                render.GlToggleBlend(true, EnumBlendMode.Standard);
                render.GlEnableCullFace();
                if (currentActiveShader != null && currentActiveShader != render.CurrentActiveShader)
                {
                    currentActiveShader.Use();
                }
            }

            return false; // Skip the vanilla chest-label renderer
        }
    }
}
