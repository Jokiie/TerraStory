using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Backgrounds.LudiMapBackground
{
    public class LudibriumSurfBG : ModSurfaceBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu
            && Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium;
        }
        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }
        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiFarBg"); // Surface far barckground
        }

        public override int ChooseMiddleTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiMidBg"); // Surface middle background
        }

        public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
        {
            return mod.GetBackgroundSlot("Backgrounds/LudiMapBackground/LudiCloseBg"); // surface close background
        }
    }
}