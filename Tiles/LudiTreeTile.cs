using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles
{
	public class LudiTreeTile : ModTile
	{
		public override void SetDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			soundType = SoundID.Tink;
			soundStyle = 1;
			dustType = DustType<Sparkle4>();
			drop = mod.ItemType("LudiTreeBlock");
			AddMapEntry(new Color(0, 100, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
		}

		public override int SaplingGrowthType(ref int style) {
			style = 0;
			return TileType<LudiTreeSapling>();
		}
	}
}