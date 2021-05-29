using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerraStory.Items.Placeable;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles.Plants
{
    public class CottonPlantTile : ModTile
    {
        public override void SetDefaults()
        {
      
            Main.tileFrameImportant[Type] = true;
            Main.tileCut[Type] = true;
            Main.tileNoFail[Type] = true;
            soundType = SoundID.Grass;
            TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
            TileObjectData.newTile.AnchorValidTiles = new int[] {
                TileID.Mud,
                TileID.Dirt,
                TileID.Grass,
                TileID.JungleGrass,
            };
            TileObjectData.newTile.AnchorAlternateTiles = new int[] {
                TileID.ClayPot,
                TileID.PlanterBox,
                ModContent.TileType<CottonPlantBoxTile>()
            };
            TileObjectData.addTile(Type);
        }
        
        public override bool CanPlace(int i, int j)
        {
            if (Main.tileAlch[Main.tile[i, j].type] || Main.tile[i, j].type == Type)
            {
                return false;
            }
            return true;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override bool Drop(int i, int j)
        {
            int growthStage = Main.tile[i, j].frameX / 15;
            if (growthStage > 0)
            {
                if (Main.player[Player.FindClosest(new Microsoft.Xna.Framework.Vector2(i * 16, j * 16), 0, 0)].HeldItem.netID == ItemID.StaffofRegrowth)
                {
                    Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<CottonPlantSeedItem>() , Main.rand.Next(1, 6));
                    Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<CottonPlantItem>(), Main.rand.Next(1, 3));
                }
                else if (growthStage == 2)
                {
                    Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<CottonPlantSeedItem>(), Main.rand.Next(1, 4));
                    Item.NewItem(i * 16, j * 16, 0, 0, ModContent.ItemType<CottonPlantItem>());
                }
            }
            return false;
        }
        public override void RandomUpdate(int i, int j)
        {
                if (Main.tile[i, j].frameX == 0)
            {
                Main.tile[i, j].frameX += 15;
            }
            else if (Main.tile[i, j].frameX == 15)
            {
                Main.tile[i, j].frameX += 15;



            }
        }
    }
}