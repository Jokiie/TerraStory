using TerraStory.Tiles;
using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles
{
	public class LidiumBricksTile : ModTile
	{
		public override void SetDefaults() {
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = 62;
			drop = ItemType<Items.Placeable.LidiumBrick>();
			AddMapEntry(new Color(153, 50, 204));
			soundType = SoundID.Tink;
			soundStyle = 1;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}
	}
}