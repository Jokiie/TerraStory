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
			DisplayName.SetDefault("Beginner's Bag");
			Tooltip.SetDefault("<Right click> for a beginner weapon and a random choice of :" +
				"\n Warrior, Archer, Magician, Summoner, cannoneer or thief bags !");
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
			player.QuickSpawnItem(ItemType<BeginnerSword>());
			player.QuickSpawnItem(ItemType<GreenSnailShell>(), 300);
			player.QuickSpawnItem(ItemType<FryingPan>());

			
			int choice = Main.rand.Next(6);
			if (choice == 0)
				player.QuickSpawnItem(ItemType<WarriorBag>());
			if (choice == 1)
				player.QuickSpawnItem(ItemType<MagicianBag>());
			if (choice == 2)
				player.QuickSpawnItem(ItemType<ThiefBag>());
			if (choice == 3)
				player.QuickSpawnItem(ItemType<ArcherBag>());
			if (choice == 4)
				player.QuickSpawnItem(ItemType<SummonerBag>());
			if (choice == 5)
				player.QuickSpawnItem(ItemType<CannoneerBag>());
		}
	}
}