using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Mage;
using TerraStory.Items.Weapons.Minions;
using TerraStory.Items.Weapons.Ranger;
using TerraStory.Items.Tools;
using TerraStory.Items.Weapons.Thief.MapleClaw;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Items.Weapons.Warrior;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Boxes
{
	public class MapleBox : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Maple Box");
			Tooltip.SetDefault("Which maple weapon will you get?");
		}

		public override void SetDefaults() {
			item.width = 20;
			item.height = 20;
			item.rare = ItemRarityID.Red;
			item.value = Item.sellPrice(gold: 10);
		}

		public override bool CanRightClick()
		{
			return true;
		}

		public override void RightClick(Player player)
		{
			int choice = Main.rand.Next(17);
			if (choice == 0)
				player.QuickSpawnItem(ModContent.ItemType<MapleBow>());
			if (choice == 1)
				player.QuickSpawnItem(ModContent.ItemType<MapleGun>());
			if (choice == 2)
				player.QuickSpawnItem(ModContent.ItemType<MapleClaw>());
			if (choice == 3)
				player.QuickSpawnItem(ModContent.ItemType<MapleSword>());
			if (choice == 4)
				player.QuickSpawnItem(ModContent.ItemType<MapleSpear>());
			if (choice == 5)
				player.QuickSpawnItem(ModContent.ItemType<MapleStaff>());
			if (choice == 6)
				player.QuickSpawnItem(ModContent.ItemType<MapleScepter>());
			if (choice == 7)
				player.QuickSpawnItem(ModContent.ItemType<MapleBox>());
			if (choice == 8)
				player.QuickSpawnItem(ModContent.ItemType<MapleShuriken>(), 100);
			if (choice == 9)
				player.QuickSpawnItem(ModContent.ItemType<MapleArrow>(), 100);
			if (choice == 10)
				player.QuickSpawnItem(ModContent.ItemType<MapleBullet>(), 100);
			if (choice == 11)
				player.QuickSpawnItem(ModContent.ItemType<MaplePickaxe>());
			if (choice == 12)
				player.QuickSpawnItem(ModContent.ItemType<MapleHavorHammer>());
			if (choice == 13)
				player.QuickSpawnItem(ModContent.ItemType<MapleDragonAxe>());
			if (choice == 14)
				player.QuickSpawnItem(ModContent.ItemType<MapleSoulSinger>());
			if (choice == 15)
				player.QuickSpawnItem(ModContent.ItemType<MapleGlorySword>());
			if (choice == 16)
				player.QuickSpawnItem(ModContent.ItemType<GreenMapleSword>());
		}
	}
}