using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace TerraStory.Tiles.Furnitures.MapleMush
{
	public class MapleMushChandelier : ModTile
	{
		public override void SetDefaults()
		{
			// Main.tileFlame[Type] = true; This breaks it.
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
			TileObjectData.newTile.Height = 3;
			TileObjectData.newTile.Width = 3;
			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.newTile.AnchorBottom = default(AnchorData); TileObjectData.newTile.CoordinateHeights = new[]
            {
				16,
				16,
				16
			};
			TileObjectData.addTile(Type);
			AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Maple mushroom chandelier");
			AddMapEntry(new Color(255, 165, 0), name);
			adjTiles = new int[] { TileID.Chandeliers };
			dustType = -1;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 0.75f;
			b = 0.40f;
		}

		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(10, 4));
			Item.NewItem(i * 16, j * 16, 48, 48, ModContent.ItemType<Items.Placeable.MapleMush.MapleMushChandelierItem>());
		}
	}
}