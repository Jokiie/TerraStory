using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Walls
{
	public class SnowLudiWall : ModWall
	{
		public override void SetDefaults() {
			Main.wallHouse[Type] = false;
			dustType = DustType<Sparkle>();
			soundType = SoundID.Tink;
			soundStyle = 1;
			AddMapEntry(new Color(50, 123, 179));
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