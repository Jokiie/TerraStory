using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TerraStory.Items.Weapons.Ranger;
using TerraStory.Items.Weapons.Thief.Shurikens;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerraStory.Dusts;

namespace TerraStory.Tiles.Ambiance
{
	public class Box : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileCut[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileLighted[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
			TileObjectData.newTile.Height = 2;
			TileObjectData.newTile.Width = 2;
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Box");
			dustType = ModContent.DustType<Sparkle0>();
			AddMapEntry(new Color(0, 255, 255), name);
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = .05f;
			g = .05f;
			b = .120f;
		}
		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}
			int height = tile.frameY == 36 ? 18 : 16;
			Main.spriteBatch.Draw(mod.GetTexture("Tiles/Ambiance/Box_Glow"), new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.frameX, tile.frameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			Tile t = Main.tile[i, j];
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(10, 0));
			/*for (int k = 0; k < 8; k++)
			{
				int d = Dust.NewDust(new Vector2(i * 16, j * 16 - 10), 54, 16, 0, 0.0f, -1, ModContent.DustType<Sparkle0>(), new Color(), 0.5f);//Leave this line how it is, it uses int division
				int d1 = Dust.NewDust(new Vector2(i * 16, j * 16 - 10), 75, 16, 0, 0.0f, 0, ModContent.DustType<Sparkle>(), new Color(), 0.5f);//Leave this line how it is, it uses int division		
				//Gore.NewGore(new Vector2((int)i * 16 + Main.rand.Next(-10, 10), (int)j * 16 + Main.rand.Next(-10, 10)), new Vector2(-1, 1), mod.GetGoreSlot("Gores/Pot1"), 1f);
			}*/
			int potionitem = Main.rand.Next(new int[] { 289, 290, 291, 292, 293, 294, 295, 296, 297, 298, 299, 300, 301, 302, 303, 304, 305 });
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem(i * 16, j * 16, 32, 32, potionitem, Main.rand.Next(1, 3));
			}
			int torchItem = Main.rand.Next(new int[] { 282, ItemID.Torch });
			int ammoItem = Main.rand.Next(new int[] { ModContent.ItemType<PaperFighterPlane>(), ModContent.ItemType<WoodenTop>() });
			int heals = ItemID.Heart;
			int item = 0;
			int coins = ItemID.SilverCoin;
			int Stack = 0;
			switch (Main.rand.Next(6))
			{
				case 0:
					item = torchItem;
					Stack = Main.rand.Next(2, 10);
					break;
				case 1:
					item = ammoItem;
					Stack = Main.rand.Next(25, 50);
					break;
				case 2:
					item = 28;
					Stack = Main.rand.Next(1, 3);
					break;
				case 3:
					item = coins;
					Stack = Main.rand.Next(1, 3);
					break;
				case 4:
					item = ammoItem;
					Stack = Main.rand.Next(15, 20);
					break;
				case 5:
					item = heals;
					break;
			}
			Item.NewItem(i * 16, j * 16, 32, 32, item, Stack);
		}
	}
}