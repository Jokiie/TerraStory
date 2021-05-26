using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Accessories;
using TerraStory.Items.Weapons.Warrior;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class WarriorBag : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Warrior's Bag");
			Tooltip.SetDefault("<Right click> for goodies!");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Green;
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			player.QuickSpawnItem(ItemType<Sword>());
			player.QuickSpawnItem(ItemType<WoodenShield>());
			player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
		}
	}
}