using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Dusts
{
	public class ElectricDust : ModDust
	{
        public override void SetDefaults()
        {
			Dust.CloneDust(226);
        }
	}
}