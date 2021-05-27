using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Ranger;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class ArcherBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Archer's Bag");
			Tooltip.SetDefault("<Right click> for goodies!");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(silver: 1);
		}

		public override bool CanRightClick() {
			return true;
		}

		public override void RightClick(Player player) 
		{
			player.QuickSpawnItem(ItemType<Bow>());
			player.QuickSpawnItem(ItemID.WoodenArrow, 300);
		}
	}
}