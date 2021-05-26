using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Items.Boss
{
	
	public class BlueMushmomTreasureBag: ModItem
	{
		
		public override int BossBagNPC => mod.NPCType("BlueMushmom");
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Mushmom Treasure Bag");
			Tooltip.SetDefault("Right Click To Open");
	    }

		public override void SetDefaults() 
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 999;
			item.consumable = true;
			item.rare = ItemRarityID.Cyan;
			item.expert = true;
		}
		
		public override bool CanRightClick() 
		{
			return true;
		}
		
		public override void OpenBossBag(Player player)
		{
            player.QuickSpawnItem(ItemID.GoldCoin, 2);
			player.QuickSpawnItem(ItemID.LesserHealingPotion, Main.rand.Next(3, 10));
			player.QuickSpawnItem(ItemID.LesserManaPotion, Main.rand.Next(1, 3));
			player.QuickSpawnItem(ItemID.GlowingMushroom, Main.rand.Next(1, 10));
			player.QuickSpawnItem(ModContent.ItemType<BundleOfMesos>(), Main.rand.Next(1, 3));

			/*
			if (Main.rand.NextFloat() < .14f) // 14% chance
				player.QuickSpawnItem(mod.ItemType("MushmomCap"), 1);
			if (Main.rand.NextFloat() < .25f) // 25% chance
				player.QuickSpawnItem(mod.ItemType("MushroomMount"), 1);
			if (Main.rand.NextFloat() < .25f) // 25% chance
				player.QuickSpawnItem(mod.ItemType("KinoBadge"), 1);
			int choice = Main.rand.Next(3);
			if (choice == 0)
				player.QuickSpawnItem(mod.ItemType("MushroomSword"), 1);
			if (choice == 1)
				player.QuickSpawnItem(mod.ItemType("OrangeMushroomStaff"), 1);
			if (choice == 2)
				player.QuickSpawnItem(mod.ItemType("BigMushroom"), 1);
			*/
		}
	}
}