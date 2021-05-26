using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Walls
{
	public class MapleMushWallTile : ModWall
	{
		public override void SetDefaults()
		{

			Main.wallHouse[Type] = true;
			dustType = 124;
			soundType = SoundID.Tink;
			soundStyle = 1;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Maple mushroom wall");
			AddMapEntry(new Color(255, 239, 213));
			drop = ItemType<Items.Placeable.MapleMush.MapleMushWall>();
		}

		public override void NumDust(int i, int j, bool fail, ref int num) 
		{
			num = fail ? 1 : 3;
		}
	}
}