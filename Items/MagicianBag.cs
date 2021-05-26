using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Mage;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class MagicianBag : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Magician's Bag");
			Tooltip.SetDefault("<Right click> for goodies!");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Green;
		}

		public override bool CanRightClick() {
			return true;
		}

		public override void RightClick(Player player) {
			player.QuickSpawnItem(ItemType<Staff>());
			player.QuickSpawnItem(ItemID.LesserManaPotion, 5);
			player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
		}
	}
}