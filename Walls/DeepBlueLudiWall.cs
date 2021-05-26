using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Walls
{
	public class DeepBlueLudiWall : ModWall
	{
		public override void SetDefaults() {
			Main.wallHouse[Type] = false;
			dustType = DustType<Sparkle1>();
			soundType = SoundID.Tink;
			soundStyle = 1;
			AddMapEntry(new Color(0, 0, 205));
			drop = ItemType<Items.Placeable.DeepBlueLudiWall>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.4f;
			g = 0.4f;
			b = 0.4f;
		}
	}
}