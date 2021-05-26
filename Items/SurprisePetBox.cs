using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class SurprisePetBox : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Surprise Pet Box !");
			Tooltip.SetDefault("<Right click> for a chance to obtain a ramdon pet who loot for you !");
		}

		public override void SetDefaults() {
			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;
			item.rare = ItemRarityID.Cyan;
		}

		public override bool CanRightClick() {
			return true;
		}

		public override void RightClick(Player player) 
		{
			{
				int choice = Main.rand.Next(20);
				if (choice == 0)
				{
					player.QuickSpawnItem(ItemType<MapleLeaf>());
				}
				else if (choice == 1)
				{
					player.QuickSpawnItem(ItemType<FennecFoxSummon>());
				}
				if (choice == 2)
				{
					player.QuickSpawnItem(ItemType<BabyDragonSummon>());
				}
				if (choice == 3)
				{
					player.QuickSpawnItem(ItemType<KinoBadge>());
				}
				if (choice == 4)
				{
					player.QuickSpawnItem(ItemType<SnailSummon>());
				}
				if (choice > 18)
				{
					player.QuickSpawnItem(ItemType<RockOfEvolution>());
				}
			}
		}
	}
}