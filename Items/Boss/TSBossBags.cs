using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Accessories;
using TerraStory.Items.Armor.Mage;
using TerraStory.Items.Armor.Ranger;
using TerraStory.Items.Armor.Summoner;
using TerraStory.Items.Armor.Warrior;
using TerraStory.Items.Weapons.Ranger;
using TerraStory.Items.Weapons.Thief.NinjaClaw;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Boss
{
	public class TSBossBags : GlobalItem
	{
		public override bool PreOpenVanillaBag(string context, Player player, int arg)
		{
			if (context == "bossBag" && arg == ItemID.KingSlimeBossBag)
			{
				player.QuickSpawnItem(ItemID.GoldCoin, 1);
				player.QuickSpawnItem(ItemID.RoyalGel, 1);
				if (Main.rand.NextFloat() < .50f) // 50% chance
					player.QuickSpawnItem(ItemID.SlimySaddle, 1);
				if (Main.rand.NextFloat() < .50f) // 50% chance
					player.QuickSpawnItem(ItemID.SlimeGun, 1);
				if (Main.rand.NextFloat() < .50f) // 50% chance
					player.QuickSpawnItem(ItemID.SlimeHook, 1);
				if (Main.rand.NextFloat() < .50f) // 50% chance
					player.QuickSpawnItem(ItemID.Solidifier, 1);
				if (Main.rand.NextFloat() < .14f) // 14% chance
					player.QuickSpawnItem(ItemID.KingSlimeMask, 1);
				if (Main.rand.NextFloat() < .67f)
					switch (Main.rand.Next(2))
					{
						case 0:
							player.QuickSpawnItem(ItemID.NinjaShirt);
							break;
						case 1:
							player.QuickSpawnItem(ItemID.NinjaPants);
							break;
					}
					if (Main.rand.NextFloat() < .67f)
					    switch (Main.rand.Next(5))
					{
						case 0:
							player.QuickSpawnItem(ItemType<SummonerNinjaHood>());
							break;
						case 1:
							player.QuickSpawnItem(ItemType<MageNinjaHat>());
							break;
						case 2:
							player.QuickSpawnItem(ItemType<WarriorNinjaHelmet>());
							break;
						case 3:
							player.QuickSpawnItem(ItemType<NinjaRangerHelmet>());
							break;
						case 4:
							player.QuickSpawnItem(ItemID.NinjaHood, 1);
							break;
					}
				if (Main.rand.NextFloat() < .33f)
					switch (Main.rand.Next(4))
					{
						case 0:
							player.QuickSpawnItem(ItemType<Wolbi>(), Main.rand.Next(10, 100));
							break;
						case 1:
							player.QuickSpawnItem(ItemType<MightyBullet>(), Main.rand.Next(10, 100));
							break;
						case 2:
							player.QuickSpawnItem(ItemType<NinjaClaw>());
							break;
						case 3:
							player.QuickSpawnItem(ItemType<IronArrow>(), Main.rand.Next(10, 100));
							break;

					}

			}
			return false;
		}

	
		public override void OpenVanillaBag(string context, Player player, int arg)
		{
			// This method shows adding items to Fishrons boss bag. 
			// Typically you'll also want to also add an item to the non-expert boss drops, that code can be found in ExampleGlobalNPC.NPCLoot. Use this and that to add drops to bosses.
			if (context == "bossBag" && arg == ItemID.WallOfFleshBossBag)
			{
				if (Main.rand.NextFloat() < .14f)
					switch (Main.rand.Next(2))
				{
					case 0:
						player.QuickSpawnItem(ItemType<ThiefEmblem>());
						break;
					case 1:
						player.QuickSpawnItem(ItemType<CannoneerEmblem>());
						break;
				}
			}
		}
	}
}
