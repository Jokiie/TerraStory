using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using TerraStory.Items.Tools;
using TerraStory.Items.Weapons;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class BeginnerBag : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starter Bag");
			Tooltip.SetDefault("<Right click> for a Hammer-Pan , 5 lesser healing potions and a bag for each class!");
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
			player.QuickSpawnItem(ItemID.LesserHealingPotion, 5);
			player.QuickSpawnItem(ItemType<FryingPan>());
			player.QuickSpawnItem(ItemType<WarriorBag>());
			player.QuickSpawnItem(ItemType<MagicianBag>());
			player.QuickSpawnItem(ItemType<ThiefBag>());
			player.QuickSpawnItem(ItemType<ArcherBag>());
			player.QuickSpawnItem(ItemType<SummonerBag>());
			player.QuickSpawnItem(ItemType<CannoneerBag>());
		}
	}
}