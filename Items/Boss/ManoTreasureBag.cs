using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Weapons.Ranger;
using TerraStory.Items.Weapons.Cannoneer.BombsAmmo;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;
using TerraStory.NPCs.Bosses;

namespace TerraStory.Items.Boss
{
	
	public class ManoTreasureBag: ModItem
	{	
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mano Treasure Bag");
			Tooltip.SetDefault("Right Click To Open");
	    }

		public override void SetDefaults() 
		{
			item.width = 24;
			item.height = 24;
			item.consumable = true;
			item.maxStack = 999;
			item.rare = ItemRarityID.Cyan;
			item.expert = true;
		}

		public override int BossBagNPC => NPCType<Mano>();

		public override bool CanRightClick() 
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(10, 30));

			player.QuickSpawnItem(ItemID.LesserHealingPotion, Main.rand.Next(1, 5));

			player.QuickSpawnItem(ItemID.LesserManaPotion, Main.rand.Next(1, 3));

			player.QuickSpawnItem(ItemType<BundleOfMesos>(), Main.rand.Next(1, 3));

			int choice = Main.rand.Next(4);
			if (choice == 0)
			{
				player.QuickSpawnItem(ItemType<Subi>(), Main.rand.Next(50, 100));
			}
			if (choice == 1)
			{
				player.QuickSpawnItem(ItemType<NoviceBomb>(), Main.rand.Next(50, 100));
			}
			if (choice == 2)
			{
				player.QuickSpawnItem(ItemType<Bullet>(), Main.rand.Next(50, 100));
			}
			if (choice == 3)
			{
				player.QuickSpawnItem(ItemType<CopperArrow>(), Main.rand.Next(50, 100));
			}
		}
	}
}