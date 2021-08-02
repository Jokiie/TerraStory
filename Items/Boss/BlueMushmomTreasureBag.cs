using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Boss;
using static Terraria.ModLoader.ModContent;

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
		}
	}
}