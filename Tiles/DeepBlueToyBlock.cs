using TerraStory.Tiles;
using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles
{
	public class DeepBlueToyBlock : ModTile
	{
		public override void SetDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			soundType = SoundID.Tink;
			soundStyle = 1;
			dustType = DustType<Sparkle1>();
			drop = ItemType<Items.Placeable.DeepBlueToyBlock>();
			AddMapEntry(new Color(0, 0, 139));
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b) {
			r = 0.5f;
			g = 0.5f;
			b = 0.5f;
		}
	}
}