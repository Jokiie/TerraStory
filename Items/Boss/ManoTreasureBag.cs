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
	
	public class ManoTreasureBag: ModItem
	{
		
		public override int BossBagNPC => mod.NPCType("Mano");
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mano Treasure Bag");
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
			player.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Thief.Shurikens.Subi>(), Main.rand.Next(10, 100));
			player.QuickSpawnItem(ItemID.SilverCoin, 30);
			player.QuickSpawnItem(ItemID.LesserHealingPotion, Main.rand.Next(3, 10));
			player.QuickSpawnItem(ItemID.LesserManaPotion, Main.rand.Next(1, 3));
			player.QuickSpawnItem(ModContent.ItemType<BundleOfMesos>(), Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .50f) // 50% chance
				player.QuickSpawnItem(mod.ItemType("RainbowColoredSnailShell"), 1);
				player.QuickSpawnItem(ModContent.ItemType<Items.Weapons.Thief.Shurikens.Subi>(), Main.rand.Next(10, 100));

		}
	}
}