using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Backgrounds.LudiMapBackground
{
    public class LudibriumUnderBG : ModUgBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return Main.LocalPlayer.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium;
        }

        public override void FillTextureArray(int[] textureSlots)
        {
            textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiUdBgTop");
            textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiUdBg");
            textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiCavBgTop");
            textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiCavBg");
        }
    }
}