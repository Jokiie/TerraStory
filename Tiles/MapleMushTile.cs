using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles
{
	public class MapleMushTile : ModTile
	{
		public override void SetDefaults() 
		{
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Maple mushroom");
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = false;
			soundType = SoundID.Tink;
			soundStyle = 1;
			dustType = 124;
			drop = ItemType<Items.Placeable.MapleMush.MapleMushroom>();
			AddMapEntry(new Color(255, 165, 0));
		}

		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}
	}
}