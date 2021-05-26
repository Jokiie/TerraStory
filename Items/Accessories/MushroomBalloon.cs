using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Items.MountsSummon;
using TerraStory.Mounts;

namespace TerraStory.Items.Accessories
{
    [AutoloadEquip(EquipType.Balloon)]
    public class MushroomBalloon : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 34;
            item.accessory = true;
            item.value = Item.sellPrice(gold: 1);
            item.rare = ItemRarityID.Green;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {

                Player.jumpHeight += 1;
                Player.jumpSpeed += 5;
          
        }

        public override void DrawHands(ref bool drawHands, ref bool drawArms)
        {
            Player player = Main.player[Main.myPlayer];
            if(player.controlUseItem && item.type == ModContent.ItemType<MushroomSuit>())
            {
                drawHands = false;
                drawArms = false;
            }
        }
    }
}