using TerraStory.Tiles;
using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles
{
	public class YellowToyBlockTile : ModTile
	{
		public override void SetDefaults() {
			Main.tileStone[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			dustType = DustType<Sparkle>();
			drop = ItemType<Items.Placeable.YellowToyBlock>();
			AddMapEntry(new Color(255, 255, 0));
			SetModTree(new Trees.LudiTree());
			soundType = SoundID.Tink;
			soundStyle = 1;
		}

		public override void NumDust(int i, int j, bool fail, ref int num) {
			num = fail ? 1 : 3;
		}

		public override int SaplingGrowthType(ref int style) {
			style = 0;
			return TileType<LudiTreeSapling>();
		}
	}
}