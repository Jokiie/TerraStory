using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerraStory.Items.Placeable.MapleMush;
using Terraria.ID;

namespace TerraStory.Tiles.Furnitures.MapleMush
{
	public class MapleMushRoof : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = mod.ItemType("Maple mushroom roof");
			soundType = SoundID.Item;
			soundStyle = 2;
			dustType = 124;
			AddMapEntry(new Color(255, 165, 0));
			drop = ModContent.ItemType<MapleMushRoofItem>();
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
		public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 3;
		}

	}
}