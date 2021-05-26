using System.Collections.Generic;
using TerraStory.Enums;
using Terraria;
using Terraria.ID;

namespace TerraStory.Content.Players
{
    public static class ContentSamples
	{
		public static Player player;
		public static class CreativeHelper
		{
			public enum ItemGroup
			{
				Coin = 10,
				Torches = 20,
				Glowsticks = 25,
				Wood = 30,
				Bombs = 40,
				LifePotions = 50,
				ManaPotions = 51,
				BuffPotion = 52,
				Flask = 53,
				Food = 54,
				Crates = 60,
				BossBags = 70,
				GoodieBags = 80,
				AlchemyPlants = 83,
				AlchemySeeds = 84,
				DyeMaterial = 87,
				BossItem = 90,
				EventItem = 91,
				ConsumableThatDoesNotDamage = 94,
				Solutions = 95,
				Ammo = 96,
				ConsumableThatDamages = 97,
				PlacableObjects = 100,
				Blocks = 120,
				Wands = 130,
				Rope = 140,
				Walls = 150,
				Wiring = 200,
				Pickaxe = 500,
				Axe = 510,
				Hammer = 520,
				MeleeWeapon = 530,
				RangedWeapon = 540,
				MagicWeapon = 550,
				SummonWeapon = 560,
				Headgear = 600,
				Torso = 610,
				Pants = 620,
				Accessories = 630,
				Hook = 700,
				Mount = 710,
				Minecart = 720,
				VanityPet = 800,
				LightPet = 810,
				Golf = 900,
				Dye = 910,
				HairDye = 920,
				Paint = 930,
				FishingRods = 1000,
				FishingQuestFish = 1010,
				Fish = 1015,
				FishingBait = 1020,
				Critters = 1030,
				Keys = 2000,
				RemainingUseItems = 5000,
				Material = 10000,
				EverythingElse = 11000
			}

			public struct ItemGroupAndOrderInGroup
			{
				public int ItemType;

				public ItemGroup Group;

				public int OrderInGroup;

				public int ReqLevel;


				public ItemGroupAndOrderInGroup(Item item, ItemGroup itemGroup, PlayerStats playerstats)
				{
					ItemType = item.type;
					ReqLevel = player.GetModPlayer<PlayerCharacter>().Level;
					Group = GetItemGroup(item, out OrderInGroup);

				}
			}

			private static List<int> _manualEventItemsOrder = new List<int>
			{
				361,
				1315,
				2767,
				602,
				1844,
				1958
			};

			private static List<int> _manualBossSpawnItemsOrder = new List<int>
			{
				43,
				560,
				70,
				1331,
				1133,
				1307,
				267,
				4988,
				544,
				557,
				556,
				1293,
				2673,
				4961,
				3601
			};

			private static List<int> _manualGolfItemsOrder = new List<int>
			{
				4095,
				4596,
				4597,
				4595,
				4598,
				4592,
				4593,
				4591,
				4594,
				4092,
				4093,
				4039,
				4094,
				4588,
				4589,
				4587,
				4590,
				3989,
				4242,
				4243,
				4244,
				4245,
				4246,
				4247,
				4248,
				4249,
				4250,
				4251,
				4252,
				4253,
				4254,
				4255,
				4040,
				4086,
				4085,
				4088,
				4084,
				4083,
				4087
			};

			public static int Compare(Item x, Item y)
			{
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup = ContentSamples.ItemCreativeSortingId[x.type];
				ContentSamples.CreativeHelper.ItemGroupAndOrderInGroup itemGroupAndOrderInGroup2 = ContentSamples.ItemCreativeSortingId[y.type];
				int num = itemGroupAndOrderInGroup.Group.CompareTo(itemGroupAndOrderInGroup2.Group);
				if (num == 0)
				{
					num = itemGroupAndOrderInGroup.OrderInGroup.CompareTo(itemGroupAndOrderInGroup2.OrderInGroup);
				}
				return num;
			}
		
		    public static int type;

			public static bool IsACoin
			{
				get
				{
					int num = type;
					if ((uint)(num - 71) <= 3u)
					{
						return true;
					}
					return false;
				}
			}

			public static ItemGroup GetItemGroup(Item item, out int orderInGroup)
			{
				orderInGroup = 0;
				int num = _manualGolfItemsOrder.IndexOf(item.type);
				if (num != -1)
				{
					orderInGroup = num;
					return ItemGroup.Golf;
				}
				int num2 = ItemID.Sets.SortingPriorityWiring[item.type];
				if (num2 != -1)
				{
					orderInGroup = -num2;
					return ItemGroup.Wiring;
				}
				if (item.type == ItemID.ActuationRod)
				{
					return ItemGroup.Wiring;
				}
				if (item.type == ItemID.GoldenKey || item.type == ItemID.ShadowKey || item.type == ItemID.TempleKey || item.type == ItemID.JungleKey || item.type == ItemID.FrozenKey || item.type == ItemID.HallowedKey || item.type == ItemID.CorruptionKey || item.type == ItemID.CrimsonKey || item.type == ItemID.LightKey || item.type == ItemID.NightKey)
				{
					orderInGroup = -item.rare;
					return ItemGroup.Keys;
				}
				if (item.type == ItemID.RopeCoil || item.type == ItemID.SilkRopeCoil || item.type == ItemID.VineRopeCoil || item.type == ItemID.WebRopeCoil)
				{
					return ItemGroup.Rope;
				}
				if (item.type == ItemID.BlueSolution || item.type == ItemID.DarkBlueSolution || item.type == ItemID.GreenSolution || item.type == ItemID.PurpleSolution || item.type == ItemID.RedSolution)
				{
					return ItemGroup.Solutions;
				}
				if (item.type == ItemID.Glowstick || item.type == ItemID.BouncyGlowstick || item.type == ItemID.SpelunkerGlowstick || item.type == ItemID.StickyGlowstick)
				{
					if (item.type == ItemID.Glowstick)
					{
						orderInGroup = -1;
					}
					return ItemGroup.Glowsticks;
				}
				if (item.type == ItemID.Bomb || item.type == ItemID.BouncyBomb || item.type == ItemID.StickyBomb || item.type == ItemID.Dynamite || item.type == ItemID.BouncyDynamite || item.type == ItemID.StickyDynamite || item.type == ItemID.BombFish )
				{
					return ItemGroup.Bombs;
				}
				if (item.createTile == TileID.FishingCrate)
				{
					return ItemGroup.Crates;
				}
				if (item.type == ItemID.GoodieBag || item.type == ItemID.Present || item.type == ItemID.HerbBag )
				{
					return ItemGroup.GoodieBags;
				}
				if ((item.type >= ItemID.KingSlimeBossBag && item.type <= ItemID.MoonLordBossBag) || (item.type >= ItemID.BossBagBetsy && item.type <= ItemID.BossBagDarkMage))
				{
					return ItemGroup.BossBags;
				}
				if (item.type == ItemID.RedHusk || item.type == ItemID.OrangeBloodroot || item.type == ItemID.YellowMarigold || item.type == ItemID.LimeKelp || item.type == ItemID.GreenMushroom || item.type == ItemID.TealMushroom || item.type == ItemID.CyanHusk || item.type == ItemID.SkyBlueFlower || item.type == ItemID.BlueBerries || item.type == ItemID.PurpleMucos || item.type == ItemID.VioletHusk || item.type == ItemID.PinkPricklyPear || item.type == ItemID.BlackInk)
				{
					return ItemGroup.DyeMaterial;
				}
				if (item.type == ItemID.StrangePlant1 || item.type == ItemID.StrangePlant2 || item.type == ItemID.StrangePlant3 || item.type == ItemID.StrangePlant4)
				{
					orderInGroup = -1;
					return ItemGroup.DyeMaterial;
				}
				if (item.dye != 0)
				{
					return ItemGroup.Dye;
				}
				if (item.hairDye != -1)
				{
					return ItemGroup.HairDye;
				}
				if (item.createWall > 0)
				{
					return ItemGroup.Walls;
				}
				if (item.createTile == TileID.ImmatureHerbs)
				{
					return ItemGroup.AlchemySeeds;
				}
				if (item.type == ItemID.Blinkroot || item.type == ItemID.Daybloom || item.type == ItemID.Deathweed || item.type == ItemID.Fireblossom || item.type == ItemID.Moonglow || item.type == ItemID.Shiverthorn || item.type == ItemID.Waterleaf)
				{
					return ItemGroup.AlchemyPlants;
				}
				if (item.createTile == TileID.WoodBlock || item.createTile == TileID.BorealWood || item.createTile == TileID.PalmWood || item.createTile == TileID.Ebonwood || item.createTile == TileID.RichMahogany || item.createTile == TileID.Shadewood || item.createTile == TileID.Pearlwood || item.createTile == TileID.SpookyWood || item.createTile == TileID.DynastyWood)
				{
					if (item.createTile == TileID.WoodBlock)
					{
						orderInGroup = 0;
					}
					else if (item.createTile == TileID.DynastyWood)
					{
						orderInGroup = 100;
					}
					else
					{
						orderInGroup = 50;
					}
					return ItemGroup.Wood;
				}
				if (item.createTile >= TileID.Dirt)
				{
					if (item.type == ItemID.StaffofRegrowth)
					{
						orderInGroup = -1;
						return ItemGroup.Pickaxe;
					}
					if (item.tileWand >= 0)
					{
						return ItemGroup.Wands;
					}
					if (item.createTile == TileID.Rope || item.createTile == TileID.VineRope || item.createTile == TileID.SilkRope || item.createTile == TileID.WebRope || item.createTile == TileID.Chain)
					{
						return ItemGroup.Rope;
					}
					if (!Main.tileSolid[item.createTile] || Main.tileSolidTop[item.createTile] || item.createTile == TileID.ClosedDoor)
					{
						if (item.createTile == TileID.Torches)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 5;
							}
							else
							{
								orderInGroup = 10;
							}
							return ItemGroup.Torches;
						}
						if (item.createTile == TileID.ExposedGems)
						{
							orderInGroup = 5;
						}
						else if (item.createTile == TileID.MetalBars)
						{
							orderInGroup = 7;
						}
						else if (item.type == ItemID.Acorn)
						{
							orderInGroup = 8;
						}
						else if (TileID.Sets.Platforms[item.createTile])
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 19;
							}
							else
							{
								orderInGroup = 20;
							}
						}
						else if (item.createTile == TileID.WorkBenches)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 29;
							}
							else
							{
								orderInGroup = 30;
							}
						}
						else if (item.createTile == TileID.Anvils || item.createTile == TileID.MythrilAnvil)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 39;
							}
							else
							{
								orderInGroup = 40;
							}
						}
						else if (item.createTile == TileID.AdamantiteForge || item.createTile == TileID.Furnaces)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 49;
							}
							else
							{
								orderInGroup = 50;
							}
						}
						else if (item.createTile == TileID.ClosedDoor)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 59;
							}
							else
							{
								orderInGroup = 60;
							}
						}
						else if (item.createTile == TileID.Chairs)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 69;
							}
							else
							{
								orderInGroup = 70;
							}
						}
						else if (item.createTile == TileID.Beds)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 74;
							}
							else
							{
								orderInGroup = 75;
							}
						}
						else if (item.createTile == TileID.Tables)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 79;
							}
							else
							{
								orderInGroup = 80;
							}
						}
						else if (item.createTile == TileID.Tables2)
						{
							orderInGroup = 90;
						}
						else if (item.createTile == TileID.Containers)
						{
							if (item.placeStyle == 0)
							{
								orderInGroup = 99;
							}
							else
							{
								orderInGroup = 100;
							}
						}
						else if (item.createTile == TileID.Containers2)
						{
							orderInGroup = 110;
						}
						else if (item.createTile == TileID.FakeContainers)
						{
							orderInGroup = 120;
						}
						else if (item.createTile == TileID.FakeContainers2)
						{
							orderInGroup = 130;
						}
						else
						{
							orderInGroup = item.createTile + 1000;
						}
						return ItemGroup.PlacableObjects;
					}
					if (TileID.Sets.Conversion.Grass[item.createTile] || item.type == ItemID.MushroomGrassSeeds)
					{
						orderInGroup = 5;
					}
					else
					{
						orderInGroup = 10000;
					}
					if (item.type == ItemID.DirtBlock)
					{
						orderInGroup = 10;
					}
					else if (item.type == ItemID.StoneBlock)
					{
						orderInGroup = 20;
					}
					else if (item.type == ItemID.ClayBlock)
					{
						orderInGroup = 30;
					}
					else if (item.type == ItemID.SiltBlock)
					{
						orderInGroup = 40;
					}
					else if (item.type == ItemID.SlushBlock)
					{
						orderInGroup = 50;
					}
					else if (item.type == ItemID.SandBlock)
					{
						orderInGroup = 60;
					}
					else if (item.type == ItemID.Glass)
					{
						orderInGroup = 70;
					}
					else if (item.type == ItemID.MudBlock)
					{
						orderInGroup = 80;
					}
					else if (item.type == ItemID.Cactus)
					{
						orderInGroup = 80;
					}
					return ItemGroup.Blocks;
				}
				if (item.mountType != -1)
				{
					if (MountID.Sets.Cart[item.mountType])
					{
						return ItemGroup.Minecart;
					}
					return ItemGroup.Mount;
				}
				if (item.bait > 0)
				{
					orderInGroup = -item.bait;
					return ItemGroup.FishingBait;
				}
				if (item.makeNPC > 0)
				{
					return ItemGroup.Critters;
				}
				if (item.fishingPole > 1)
				{
					orderInGroup = -item.fishingPole;
					return ItemGroup.FishingRods;
				}
				if (item.questItem)
				{
					return ItemGroup.FishingQuestFish;
				}
				if ((item.type >= ItemID.Trout && item.type <= ItemID.Stinkfish) || item.type == ItemID.Bass)
				{
					orderInGroup = -item.rare;
					return ItemGroup.FishingQuestFish;
				}
				int num3 = ItemID.Sets.SortingPriorityPainting[item.type];
				if (num3 != -1 || item.paint != 0)
				{
					orderInGroup = -num3;
					return ItemGroup.Paint;
				}
				int num4 = _manualBossSpawnItemsOrder.IndexOf(item.type);
				if (num4 != -1)
				{
					orderInGroup = num4;
					return ItemGroup.BossItem;
				}
				int num5 = _manualEventItemsOrder.IndexOf(item.type);
				if (num5 != -1)
				{
					orderInGroup = num5;
					return ItemGroup.EventItem;
				}
				if (item.shoot != ProjectileID.None && Main.projHook[item.shoot])
				{
					return ItemGroup.Hook;
				}
				if (item.type == ItemID.GenderChangePotion || item.type == ItemID.TeleportationPotion || item.type == ItemID.RecallPotion || item.type == ItemID.WormholePotion || item.type == ItemID.LovePotion || item.type == ItemID.StinkPotion)
				{
					return ItemGroup.BuffPotion;
				}
				if (item.headSlot >= 0)
				{
					orderInGroup = -item.defense;
					orderInGroup -= item.rare * 1000;
					if (item.vanity)
					{
						orderInGroup += 100000;
					}
					return ItemGroup.Headgear;
				}
				if (item.bodySlot >= 0)
				{
					orderInGroup = -item.defense;
					orderInGroup -= item.rare * 1000;
					if (item.vanity)
					{
						orderInGroup += 100000;
					}
					return ItemGroup.Torso;
				}
				if (item.legSlot >= 0)
				{
					orderInGroup = -item.defense;
					orderInGroup -= item.rare * 1000;
					if (item.vanity)
					{
						orderInGroup += 100000;
					}
					return ItemGroup.Pants;
				}
				if (item.accessory)
				{
					orderInGroup = item.vanity.ToInt() - item.expert.ToInt();
					if (item.type >= ItemID.RedString && item.type <= ItemID.BlackString)
					{
						orderInGroup -= 200000;
					}
					else if (item.type >= ItemID.BlackCounterweight && item.type <= ItemID.YellowCounterweight)
					{
						orderInGroup -= 100000;
					}
					orderInGroup -= item.rare * 10000;
					if (item.vanity)
					{
						orderInGroup += 100000;
					}
					return ItemGroup.Accessories;
				}
				if (item.pick > 0)
				{
					orderInGroup = -item.pick;
					return ItemGroup.Pickaxe;
				}
				if (item.axe > 0)
				{
					orderInGroup = -item.axe;
					return ItemGroup.Axe;
				}
				if (item.hammer > 0)
				{
					orderInGroup = -item.hammer;
					return ItemGroup.Hammer;
				}
				if (item.healLife > 0)
				{
					if (item.type == ItemID.SuperHealingPotion)
					{
						orderInGroup = 0;
					}
					else if (item.type == ItemID.GreaterHealingPotion)
					{
						orderInGroup = 1;
					}
					else if (item.type == ItemID.HealingPotion)
					{
						orderInGroup = 2;
					}
					else if (item.type == ItemID.LesserHealingPotion)
					{
						orderInGroup = 3;
					}
					else
					{
						orderInGroup = -item.healLife + 1000;
					}
					return ItemGroup.LifePotions;
				}
				if (item.healMana > 0)
				{
					orderInGroup = -item.healMana;
					return ItemGroup.ManaPotions;
				}
				if (item.ammo != AmmoID.None && !item.notAmmo && item.type != ItemID.Gel && item.type != ItemID.FallenStar)
				{
					orderInGroup = -item.ammo * 10000;
					orderInGroup += -item.damage;
					return ItemGroup.Ammo;
				}
				if (item.consumable)
				{
					if (item.damage > 0)
					{
						if (item.type == ItemID.HolyWater || item.type == ItemID.UnholyWater || item.type == ItemID.BloodWater)
						{
							orderInGroup = -100000;
						}
						else
						{
							orderInGroup = -item.damage;
						}
						return ItemGroup.ConsumableThatDamages;
					}
					if (item.type == 4910 || item.type == 4829 || item.type == 4830)
					{
						orderInGroup = 10;
					}
					else if (item.type == ItemID.PurificationPowder || item.type == ItemID.ViciousPowder || item.type == ItemID.VilePowder)
					{
						orderInGroup = -10;
					}
					else if (item.type >= ItemID.StarTopper1 && item.type <= ItemID.BlueAndYellowLights)
					{
						orderInGroup = 5;
					}
					return ItemGroup.ConsumableThatDoesNotDamage;
				}
				if (item.damage > 0)
				{
					orderInGroup = -item.damage;
					if (item.melee)
					{
						return ItemGroup.MeleeWeapon;
					}
					if (item.ranged)
					{
						return ItemGroup.RangedWeapon;
					}
					if (item.magic)
					{
						return ItemGroup.MagicWeapon;
					}
					if (item.summon)
					{
						return ItemGroup.SummonWeapon;
					}
				}
				orderInGroup = -item.rare;
				if (item.useStyle > 0)
				{
					return ItemGroup.RemainingUseItems;
				}
				if (item.material)
				{
					return ItemGroup.Material;
				}
				return ItemGroup.EverythingElse;
			}

			public static void SetCreativeMenuOrder()
			{
				List<Item> list = new List<Item>();
				for (int i = 1; i < 5045; i++)
				{
					Item item = new Item();
					item.SetDefaults(i);
					list.Add(item);
				}
			}

			public static bool ShouldRemoveFromList(Item item)
			{
				return ItemID.Sets.Deprecated[item.type];
			}
		}

		public static Dictionary<int, NPC> NpcsByNetId = new Dictionary<int, NPC>();

		public static Dictionary<int, Projectile> ProjectilesByType = new Dictionary<int, Projectile>();

		public static Dictionary<int, Item> ItemsByType = new Dictionary<int, Item>();

		public static Dictionary<string, int> ItemNetIdsByPersistentIds = new Dictionary<string, int>();

		public static Dictionary<int, string> ItemPersistentIdsByNetIds = new Dictionary<int, string>();

		public static Dictionary<string, int> NpcNetIdsByPersistentIds = new Dictionary<string, int>();

		public static Dictionary<int, string> NpcPersistentIdsByNetIds = new Dictionary<int, string>();

		public static Dictionary<int, CreativeHelper.ItemGroupAndOrderInGroup> ItemCreativeSortingId = new Dictionary<int, CreativeHelper.ItemGroupAndOrderInGroup>();

		public static int ReqLevel;
	}
}
