using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Ranger;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class GunnerBag : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Gunner's Bag");
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
			player.QuickSpawnItem(ItemType<Gun>());
			player.QuickSpawnItem(ItemType<Bullet>(),300);
			player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
		}
	}
}