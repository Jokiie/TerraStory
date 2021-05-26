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
	
	public class PapaPixieTreasureBag: ModItem
	{
		
		public override int BossBagNPC => mod.NPCType("PapaPixie");
		
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Papa Pixie's Treasure Bag");
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
            player.QuickSpawnItem(ItemID.SilverCoin, Main.rand.Next(60,75));
			player.QuickSpawnItem(ItemID.HealingPotion, Main.rand.Next(1,3));
			player.QuickSpawnItem(ItemID.ManaPotion, Main.rand.Next(1,3));
			player.QuickSpawnItem(ItemID.FallenStar, Main.rand.Next(3,5));
			int choice = Main.rand.Next(10);
			if (choice == 0)
				player.QuickSpawnItem(mod.ItemType("StarPixieStaff"));
		}
	}
}