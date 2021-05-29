using TerraStory.Items.Tools;
using TerraStory.Items.Weapons.Warrior;
using TerraStory.Tiles;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using static Terraria.ModLoader.ModContent;
using Terraria.DataStructures;
using System;
using TerraStory.Walls;
using TerraStory.Tiles.Boss;
using TerraStory.Structures;
using static TerraStory.Structures.WorldGenHelper;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Tiles.Ambiance;
using System.Configuration;
using TerraStory.NPCs.TownNPCs;
using TerraStory.Items.Weapons.Thief.PinkRabbitPuppet;
using TerraStory.Tiles.Plants;

namespace TerraStory
{

	public class World : ModWorld
	{
		public static int LudibriumTiles = 0;
		public static int LudiChest;
		public static int LidiumOre;
		public static bool downedMano = false;
		public static bool downedOrangeMushmom = false;
		public static bool downedBlueMushmom = false;
		public static bool downedZombieMushmom = false;
		public static bool downedPapaPixie = false;
		public static bool downedTrueQueenBee = false;
		public static bool Lidium = false;
		public static bool rescuedCody2 = false;

		public override void Initialize()
		{
			downedMano = false;
			downedOrangeMushmom = false;
			downedBlueMushmom = false;
			downedZombieMushmom = false;
			downedPapaPixie = false;
			downedTrueQueenBee = false;
			rescuedCody2 = false;
			if (NPC.downedMechBoss1 || NPC.downedMechBoss2 || NPC.downedMechBoss3)
			{
				Lidium = true;
			}
			else
			{
				Lidium = false;
			}
		}

		public override TagCompound Save()
		{
			var downed = new List<string>();
			var rescued = new List<string>();
			if (downedMano) downed.Add("Mano");
			if (downedOrangeMushmom) downed.Add("OrangeMushmom");
			if (downedBlueMushmom) downed.Add("BlueMushmom");
			if (downedZombieMushmom) downed.Add("ZombieMushmom");
			if (downedPapaPixie) downed.Add("PapaPixie");
			if (downedTrueQueenBee) downed.Add("TrueQueenBee");
			if (rescuedCody2) rescued.Add("Cody2");

			return new TagCompound
			{
				{
					"Version", 0
				},
				{
					"downed" , downed
				}
			};
		}

		public override void Load(TagCompound tag)
		{
			var downed = tag.GetList<string>("downed");
			var rescued = tag.GetList<string>("rescued");
			downedMano = downed.Contains("Mano");
			downedOrangeMushmom = downed.Contains("OrangeMushmom");
			downedBlueMushmom = downed.Contains("BlueMushmom");
			downedZombieMushmom = downed.Contains("ZombieMushmom");
			downedPapaPixie = downed.Contains("PapaPixie");
			downedTrueQueenBee = downed.Contains("TrueQueenBee");
			rescuedCody2 = rescued.Contains("Cody2");
		}

		public override void LoadLegacy(BinaryReader reader)
		{
			int loadVersion = reader.ReadInt32();
			if (loadVersion == 0)
			{
				BitsByte flags = reader.ReadByte();

				downedOrangeMushmom = flags[0];
				downedBlueMushmom = flags[1];
				downedZombieMushmom = flags[2];
				downedPapaPixie = flags[3];
				downedTrueQueenBee = flags[4];
				downedMano = flags[5];
				rescuedCody2 = flags[6];
			}
		}

		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedOrangeMushmom;
			flags[1] = downedBlueMushmom;
			flags[2] = downedZombieMushmom;
			flags[3] = downedPapaPixie;
			flags[4] = downedTrueQueenBee;
			flags[5] = downedMano;
			flags[6] = rescuedCody2;

			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedOrangeMushmom = flags[0];
			downedBlueMushmom = flags[1];
			downedZombieMushmom = flags[2];
			downedPapaPixie = flags[3];
			downedTrueQueenBee = flags[4];
			downedMano = flags[5];
			rescuedCody2 = flags[6];
		}
		public virtual int OffsetX => 0;
		public virtual int OffsetY => 0;

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int shiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Shinies"));
			if (shiniesIndex == -1)
			{
				return;
			}

			tasks.Insert(shiniesIndex + 12, new PassLegacy("Ludibrium", GenLudibrium));
			tasks.Insert(shiniesIndex + 11, new PassLegacy("Ludibrium Tower", GenerateStructure));
			tasks.Insert(shiniesIndex + 13, new PassLegacy("Generating Toys", GenChests));
			tasks.Insert(shiniesIndex + 52, new PassLegacy("Generating Cottons",
				delegate (GenerationProgress progress)
				{
					progress.Message = "Generating Cottons..";

					int amountToGen = 100; //In case of custom world size??
					if (Main.maxTilesX == 4200)
					{
						amountToGen = WorldGen.genRand.Next(50, 70);
					}
					else if (Main.maxTilesX == 6400)
					{
						amountToGen = WorldGen.genRand.Next(60, 80);
					}
					else if (Main.maxTilesX == 8400)
					{
						amountToGen = WorldGen.genRand.Next(70, 100);
					}

					for (int i = 0; i < amountToGen; i++)
					{
						bool placeSuccessful = false;
						while (!placeSuccessful)
						{
							int ShellposX = WorldGen.genRand.Next(0, Main.maxTilesX);
							int ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 150);
							if (Main.maxTilesY == 1200)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 420);
							}
							else if (Main.maxTilesY == 1800)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 620);
							}
							else if (Main.maxTilesY == 2400)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 820);
							}
							Tile tile = Main.tile[ShellposX, ShellposY];
							if (!(tile.type == TileID.Dirt
								|| tile.type == TileID.Grass
								|| tile.type == TileID.Mud))
							{
								continue;
							}
							if (ShellposY >= Main.worldSurface)
							{
								continue;
							}
							WorldGen.PlaceTile(ShellposX, ShellposY, TileType<CottonPlantTile>());
							placeSuccessful = tile.active() && tile.type == TileType<CottonPlantTile>();
						}
					}
				}));

				tasks.Insert(shiniesIndex + 15, new PassLegacy("ManoShell",
				delegate (GenerationProgress progress)
				{
					progress.Message = "Generating things..";

					int amountToGen = 5; //In case of custom world size??
					if (Main.maxTilesX == 4200)
					{
						amountToGen = WorldGen.genRand.Next(5, 8);
					}
					else if (Main.maxTilesX == 6400)
					{
						amountToGen = WorldGen.genRand.Next(8, 11);
					}
					else if (Main.maxTilesX == 8400)
					{
						amountToGen = WorldGen.genRand.Next(11, 15);
					}

					for (int i = 0; i < amountToGen; i++)
					{
						bool placeSuccessful = false;
						while (!placeSuccessful)
						{
							int ShellposX = WorldGen.genRand.Next(0, Main.maxTilesX);
							int ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 150);
							if (Main.maxTilesY == 1200)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY - 70 , Main.spawnTileY + 110);
							}
							else if (Main.maxTilesY == 1800)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 520);
							}
							else if (Main.maxTilesY == 2400)
							{
								ShellposY = WorldGen.genRand.Next(Main.spawnTileY, Main.spawnTileY + 520);
							}
							Tile tile = Main.tile[ShellposX, ShellposY];
							if (!(tile.type == TileID.Dirt
								|| tile.type == TileID.Grass
								|| tile.type == TileID.Stone
								|| tile.type == TileID.Mud
								|| tile.type == TileID.FleshGrass
								|| tile.type == TileID.CorruptGrass
								|| tile.type == TileID.JungleGrass
								|| tile.type == TileID.Sand
								|| tile.type == TileID.Crimsand
								|| tile.type == TileID.Ebonsand))
							{
								continue;
							}
							if (ShellposY >= Main.worldSurface)
							{
								continue;
							}
							WorldGen.PlaceTile(ShellposX, ShellposY, TileType<ManoShell>());
							placeSuccessful = tile.active() && tile.type == TileType<ManoShell>();
						}
					}
				}));
		}
		private bool IsOneOf(Tile tile, List<ushort> types, List<ushort> walls = null)
		{
			if (walls != null)
			{
				for (int i = 0; i < walls.Count; i++)
				{
					if (walls[i] == tile.wall) return true;
				}
			}
			if (tile.active())
			{
				for (int i = 0; i < types.Count; i++)
				{
					if (types[i] == tile.type) return true;
				}
			}
			return false;
		}

		private int Generate = 0;
		private int Length;

		private void GenLudibrium(GenerationProgress progress)
		{

			progress.Message = "Generating toys...";

			List<ushort> ignoreTiles = new List<ushort>()
			{
				TileID.BlueDungeonBrick,
				TileID.GreenDungeonBrick,
				TileID.PinkDungeonBrick,
				TileID.SandstoneBrick,
				TileID.Sandstone,
				TileID.Hive,
				TileID.LihzahrdBrick,
				TileID.HardenedSand,
				TileID.Ash,
				TileID.Hellstone,
				TileID.HellstoneBrick,
				TileID.Obsidian,
				TileID.ObsidianBrick,
				TileID.Hellforge,
				TileID.Platforms,
				TileID.Containers,
				TileID.Chairs,
				TileID.Tables,
				TileID.DemonAltar,
				TileID.WoodBlock,
				TileID.Candles,
				TileID.Chandeliers,
				TileID.Jackolanterns,
				TileID.HangingLanterns,
				TileID.WaterCandle,
				TileID.Books,
				TileID.ClayPot,
				TileID.Beds,
				TileID.Loom,
				TileID.Pianos,
				TileID.Dressers,
				TileID.Benches,
				TileID.Bathtubs,
				TileID.Banners,
				TileID.Lamps,
				TileID.Lampposts,
				TileID.Lamps,
				TileID.SkullLanterns,
				TileID.Candelabras,
				TileID.Bookcases,
				TileID.Thrones,
				TileID.Bowls,
				TileID.GrandfatherClocks,
				TileID.Statues,
				TileID.Sawmill,
				TileID.Lever,
				TileID.PressurePlates,
				TileID.Traps,
				TileID.Boulder,
				TileID.Explosives,
				TileID.Timers,
				TileID.Sinks,
				TileID.Campfire,
				TileID.ImbuingStation,
				TileID.AlchemyTable,
				TileID.FakeContainers,
				TileID.FakeContainers2,
				TileID.GeyserTrap,
				TileID.ProjectilePressurePad,
				TileID.Containers2,
				TileID.Tables2,
				TileID.LivingWood,
				TileID.LivingMahogany,
				TileID.LivingMahoganyLeaves,
				TileID.ClosedDoor,
				TileID.OpenDoor,
				TileID.GraniteBlock,
				TileID.Granite,
				TileID.Marble,
				TileID.MarbleBlock
			};
			List<ushort> ignoreWalls = new List<ushort>()
			{
				WallID.SandstoneBrick,
				WallID.BlueDungeonSlabUnsafe,
				WallID.BlueDungeonTileUnsafe,
				WallID.BlueDungeonUnsafe,
				WallID.GreenDungeonSlabUnsafe,
				WallID.GreenDungeonTileUnsafe,
				WallID.GreenDungeonUnsafe,
				WallID.PinkDungeonSlabUnsafe,
				WallID.PinkDungeonTileUnsafe,
				WallID.PinkDungeonUnsafe,
				WallID.LihzahrdBrickUnsafe,
				WallID.HiveUnsafe,
				WallID.HardenedSand,
				WallID.ObsidianBrickUnsafe,
				WallID.ObsidianBackUnsafe,
				WallID.HellstoneBrickUnsafe,
				WallID.HellstoneBrick,
				WallID.Wood,
				WallID.LivingWood,
				WallID.LivingLeaf,
				WallID.Planked,
				WallID.Sandstone
			};
			Random rand = new Random();
			int DistanceFromDung;

			if (Main.dungeonX > Main.maxTilesX / 2) //rightside dungeon 
			{
				// defaults :  WorldGen.genRand.Next((Main.maxTilesX / 2) + 300, Main.maxTilesX - 500);
				DistanceFromDung = ((Main.maxTilesX / 2) + (Main.maxTilesX / 2) / 2 - 200);
			}
			else //leftside dungeon
			{
				// defaults : WorldGen.genRand.Next(75, (Main.maxTilesX / 2) - 600);
				DistanceFromDung = ((Main.maxTilesX / 2) - (Main.maxTilesX / 2) / 2 - 125);
			}
			int StartX = DistanceFromDung;
			int StartXMid = StartX + 70;
			int StartXEdge = StartX + 380;
			int StartY = 0;
			for (int y = 0; y < Main.maxTilesY; y++)
			{
				StartY++;
				StartX = DistanceFromDung;
				int size = 300;
				if (Main.maxTilesY == 1200)
				{
					size = 300;
				}
				else if (Main.maxTilesY == 1800)
				{
					size = 350;
				}
				else if (Main.maxTilesY == 2400)
				{
					size = 450;
				}
				//nombre de tile de gauche a droite
				for (int i = 0; i < size; i++)
				{
					StartX++;

					Tile tile = Framing.GetTileSafely(StartX, StartY);
					if (IsOneOf(tile, ignoreTiles, ignoreWalls))
						continue;
					// si il ny a pas le main tile mentionner ci-dessous
					if (Main.tile[StartX, StartY] != null)
					{
						// si il y a le main tile active proche
						if (Main.tile[StartX, StartY].active())
						{
							// si cest du ex: Dirt , clay
							if (Main.tile[StartX, StartY].type == 0 || Main.tile[StartX, StartY].type == 40 || Main.tile[StartX, StartY].type == ModContent.TileType<YellowToyBlockTile>())
							{
								// loop pour avoir un degrader sur les cotes
								if (Main.tile[StartX, StartY + 1] == null)
								{
									// entre 0 et 50 blocs, on remplace quelques blocs par des blocs qui etaient deja la sur les bords
									if (Main.rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 10)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 10)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
									}
								}
							}
							// Stone & silk /// Ebonstone : Main.tile[StartX, StartY].type == 25 || Crimstone : Main.tile[StartX, StartY].type == 203
							if (Main.tile[StartX, StartY].type == 204 || Main.tile[StartX, StartY].type == 25 || Main.tile[StartX, StartY].type == 203 || Main.tile[StartX, StartY].type == 1 || Main.tile[StartX, StartY].type == 123 || Main.tile[StartX, StartY].type == ModContent.TileType<OrangeLudiBlock>())
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
									}
								}
							}

							// Grass, Corruptgrass,Crimsongrass

							if (Main.tile[StartX, StartY].type == 2 || Main.tile[StartX, StartY].type == 23 || Main.tile[StartX, StartY].type == 199)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
									}
								}
							}

							// JungleGrass,Mushroomgrass

							if (Main.tile[StartX, StartY].type == 60 || Main.tile[StartX, StartY].type == 70)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
									}
								}
							}
							// Mud, Mudstone moss ,gem sparkk
							if (Main.tile[StartX, StartY].type == 381 || Main.tile[StartX, StartY].type == 268 || Main.tile[StartX, StartY].type == 267 || Main.tile[StartX, StartY].type == 266 || Main.tile[StartX, StartY].type == 265 || Main.tile[StartX, StartY].type == 264 || Main.tile[StartX, StartY].type == 263 || Main.tile[StartX, StartY].type == 262 || Main.tile[StartX, StartY].type == 261 || Main.tile[StartX, StartY].type == 260 || Main.tile[StartX, StartY].type == 259 || Main.tile[StartX, StartY].type == 258 || Main.tile[StartX, StartY].type == 257 || Main.tile[StartX, StartY].type == 256 || Main.tile[StartX, StartY].type == 255 || Main.tile[StartX, StartY].type == 184 || Main.tile[StartX, StartY].type == 183 || Main.tile[StartX, StartY].type == 182 || Main.tile[StartX, StartY].type == 181 || Main.tile[StartX, StartY].type == 180 || Main.tile[StartX, StartY].type == 179 || Main.tile[StartX, StartY].type == 59 || Main.tile[StartX, StartY].type == 120)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
									}
								}
							}
							// snow and slush,FleshIce
							if (Main.tile[StartX, StartY].type == 147 || Main.tile[StartX, StartY].type == 224 || Main.tile[StartX, StartY].type == 200)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<DeepBlueToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
									}
								}
							}
							// Iceblock, CorruptIce, fleshice
							if (Main.tile[StartX, StartY].type == 161 || Main.tile[StartX, StartY].type == 163 || Main.tile[StartX, StartY].type == 200)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<OrangeLudiBlock>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<RedToyBlock>();
									}
								}
							}
							// plants
							int[] TileArray89 = { 3, 24, 110, 113, 115, 201, 205, 52, 62, 32, 165 };
							if (TileArray89.Contains(Main.tile[StartX, StartY].type))
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].active(false);
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].active(false);
									}
								}
							}
							// sands
							if (Main.tile[StartX, StartY].type == 112 || Main.tile[StartX, StartY].type == 234 || Main.tile[StartX, StartY].type == 53 || Main.tile[StartX, StartY].type == 112 || Main.tile[StartX, StartY].type == 234)
							{
								if (Main.tile[StartX, StartY + 1] == null)
								{
									if (rand.Next(0, 50) == 1)
									{
										Generate = 0;
										if (StartX < StartXMid - 1)
										{
											Length = StartXMid - StartX;
											Generate = Main.rand.Next(Length);
										}
										if (StartX > StartXEdge + 1)
										{
											Length = StartX - StartXEdge;
											Generate = Main.rand.Next(Length);
										}
										if (Generate < 18)
										{
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
											if (StartY > Main.rockLayer + 36)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
											if (StartY > Main.maxTilesY - 304)
												Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
										}
									}
								}
								else
								{
									Generate = 0;
									if (StartX < StartXMid - 1)
									{
										Length = StartXMid - StartX;
										Generate = Main.rand.Next(Length);
									}
									if (StartX > StartXEdge + 1)
									{
										Length = StartX - StartXEdge;
										Generate = Main.rand.Next(Length);
									}
									if (Generate < 18)
									{
										Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<YellowToyBlockTile>();
										if (StartY > Main.rockLayer + 36)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<CyanToyBlock>();
										if (StartY > Main.maxTilesY - 304)
											Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<PinkToyBlock>();
									}
								}
							}
							/*
							// Remplace toute les ores present dans le biomes par LidiumOre
							if (Main.tile[StartX, StartY].type == 6 || Main.tile[StartX, StartY].type == 7 || Main.tile[StartX, StartY].type == 8 || Main.tile[StartX, StartY].type == 9 || Main.tile[StartX, StartY].type == 221 || Main.tile[StartX, StartY].type == 222 || Main.tile[StartX, StartY].type == 223 || Main.tile[StartX, StartY].type == 204 || Main.tile[StartX, StartY].type == 166 || Main.tile[StartX, StartY].type == 167 || Main.tile[StartX, StartY].type == 168 || Main.tile[StartX, StartY].type == 169 || Main.tile[StartX, StartY].type == 221 || Main.tile[StartX, StartY].type == 107 || Main.tile[StartX, StartY].type == 108 || Main.tile[StartX, StartY].type == 22 || Main.tile[StartX, StartY].type == 111 || Main.tile[StartX, StartY].type == 123)
							{
								Main.tile[StartX, StartY].type = (ushort)ModContent.TileType<LidiumOre>();

							}*/
						}
					}
					//---------------------------------------------------------------------------------------W A L L S-------------------------------------------------------------------------------------------------------//

					//  walls
					if (Main.tile[StartX, StartY].wall == 53 || Main.tile[StartX, StartY].wall == 52 || Main.tile[StartX, StartY].wall == 51 || Main.tile[StartX, StartY].wall == 50 || Main.tile[StartX, StartY].wall == 49 || Main.tile[StartX, StartY].wall == 48 || Main.tile[StartX, StartY].wall == 211 || Main.tile[StartX, StartY].wall == 210 || Main.tile[StartX, StartY].wall == 209 || Main.tile[StartX, StartY].wall == 208 || Main.tile[StartX, StartY].wall == 79 || Main.tile[StartX, StartY].wall == 54 || Main.tile[StartX, StartY].wall == 55 || Main.tile[StartX, StartY].wall == 56 || Main.tile[StartX, StartY].wall == 57 || Main.tile[StartX, StartY].wall == 58 || Main.tile[StartX, StartY].wall == 59 || Main.tile[StartX, StartY].wall == 61 || Main.tile[StartX, StartY].wall == 170 || Main.tile[StartX, StartY].wall == 171 || Main.tile[StartX, StartY].wall == 185 || Main.tile[StartX, StartY].wall == 1 || Main.tile[StartX, StartY].wall == 212 || Main.tile[StartX, StartY].wall == 213 || Main.tile[StartX, StartY].wall == 214 || Main.tile[StartX, StartY].wall == 215 || Main.tile[StartX, StartY].wall == 2 || Main.tile[StartX, StartY].wall == 16 || Main.tile[StartX, StartY].wall == 196 || Main.tile[StartX, StartY].wall == 197 || Main.tile[StartX, StartY].wall == 198 || Main.tile[StartX, StartY].wall == 199 || Main.tile[StartX, StartY].wall == 63 || Main.tile[StartX, StartY].wall == 64 || Main.tile[StartX, StartY].wall == 65 || Main.tile[StartX, StartY].wall == 66 || Main.tile[StartX, StartY].wall == 64 || Main.tile[StartX, StartY].wall == 67 || Main.tile[StartX, StartY].wall == 69 || Main.tile[StartX, StartY].wall == 70 || Main.tile[StartX, StartY].wall == 80 || Main.tile[StartX, StartY].wall == 81 || Main.tile[StartX, StartY].wall == 204 || Main.tile[StartX, StartY].wall == 205 || Main.tile[StartX, StartY].wall == 206 || Main.tile[StartX, StartY].wall == 207 || Main.tile[StartX, StartY].wall == 71 || Main.tile[StartX, StartY].wall == 40 || Main.tile[StartX, StartY].wall == ModContent.WallType<OrangeLudiWall>())
					{
						if (Main.tile[StartX, StartY + 1] == null)
						{
							if (rand.Next(0, 50) == 1)
							{
								Generate = 0;
								if (StartX < StartXMid - 1)
								{
									Length = StartXMid - StartX;
									Generate = Main.rand.Next(Length);
								}
								if (StartX > StartXEdge + 1)
								{
									Length = StartX - StartXEdge;
									Generate = Main.rand.Next(Length);
								}
								if (Generate < 18)
								{
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<OrangeLudiWall>();
									if (StartY > Main.rockLayer + 36)
										Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<DeepBlueLudiWall>();
									if (StartY > Main.maxTilesY - 304)
										Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<RedLudiWall>();
								}
							}
						}
						else
						{
							Generate = 0;
							if (StartX < StartXMid - 1)
							{
								Length = StartXMid - StartX;
								Generate = Main.rand.Next(Length);
							}
							if (StartX > StartXEdge + 1)
							{
								Length = StartX - StartXEdge;
								Generate = Main.rand.Next(Length);
							}
							if (Generate < 18)
							{
								Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<OrangeLudiWall>();
								if (StartY > Main.rockLayer + 36)
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<DeepBlueLudiWall>();
								if (StartY > Main.maxTilesY - 304)
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<RedLudiWall>();
							}
						}
					}
					//  walls
					if (Main.tile[StartX, StartY].wall == ModContent.WallType<ToyWall>())
					{
						if (Main.tile[StartX, StartY + 1] == null)
						{
							if (rand.Next(0, 50) == 1)
							{
								Generate = 0;
								if (StartX < StartXMid - 1)
								{
									Length = StartXMid - StartX;
									Generate = Main.rand.Next(Length);
								}
								if (StartX > StartXEdge + 1)
								{
									Length = StartX - StartXEdge;
									Generate = Main.rand.Next(Length);
								}
								if (Generate < 18)
								{
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<ToyWall>();
									if (StartY > Main.rockLayer + 36)
										Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<CyanLudiWall>();
									if (StartY > Main.maxTilesY - 304)
										Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<RedLudiWall>();
								}
							}
						}
						else
						{
							Generate = 0;
							if (StartX < StartXMid - 1)
							{
								Length = StartXMid - StartX;
								Generate = Main.rand.Next(Length);
							}
							if (StartX > StartXEdge + 1)
							{
								Length = StartX - StartXEdge;
								Generate = Main.rand.Next(Length);
							}
							if (Generate < 18)
							{
								Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<ToyWall>();
								if (StartY > Main.rockLayer + 36)
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<CyanLudiWall>();
								if (StartY > Main.maxTilesY - 304)
									Main.tile[StartX, StartY].wall = (ushort)ModContent.WallType<RedLudiWall>();
							}
						}

						bool success = false;
						int CmaxTries = 5000;
						int Ctries = 0;
						int Csuccesses = 0;
						while (!success)
						{
							int ChestToPlace = 10;
							if (Main.maxTilesY == 1200)
							{
								ChestToPlace = 10;

							}
							else if (Main.maxTilesY == 1800)
							{
								ChestToPlace = 15;

							}
							else if (Main.maxTilesY == 2400)
							{
								ChestToPlace = 20;

							}
							while (Ctries < CmaxTries && Csuccesses < ChestToPlace)
							{
								int cx = StartX;
								int cy = StartY;
								if (WorldGen.PlaceChest(cx, cy, (ushort)ModContent.TileType<LudiChestTile>(), false, 0) != -1)
								{
									Csuccesses++;
								}
								Ctries++;
							}
							success = true;
						}
					}
					else if (WorldGen.genRand.Next(30) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Box>());
					}
					else if (WorldGen.genRand.Next(100) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube1x1>());
					}
					else if (WorldGen.genRand.Next(100) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube1x2>());
					}
					else if (WorldGen.genRand.Next(100) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube1x3>());
					}
					else if (WorldGen.genRand.Next(70) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube2x1>());
					}
					else if (WorldGen.genRand.Next(70) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube2x2>());
					}
					else if (WorldGen.genRand.Next(70) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube2x3>());
					}
					else if (WorldGen.genRand.Next(50) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube3x1>());
					}
					else if (WorldGen.genRand.Next(50) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube3x2>());
					}
					else if (WorldGen.genRand.Next(50) == 0)
					{
						WorldGen.PlaceTile(StartX, StartY, (ushort)ModContent.TileType<Cube3x3>());
					}
				}
			}
		}
	    private void GenChests(GenerationProgress progress)
		{
			progress.Message = "Placing more toys...";

			int[] itemsToPlaceInLudiChests = { ItemType<PaperFighterPlane>(), Main.rand.Next(0, 50), ItemType<Orange>(), Main.rand.Next(0, 30), ItemType<SoldierHongSword>(), Main.rand.Next(0, 1), ItemType<PinkRabbitPuppet>(), Main.rand.Next(0, 1) };
			int itemsToPlaceInLudiChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 0 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == ItemID.None)
						{
							chest.item[inventoryIndex].SetDefaults(itemsToPlaceInLudiChests[itemsToPlaceInLudiChestsChoice]);
							itemsToPlaceInLudiChestsChoice = (itemsToPlaceInLudiChestsChoice + 1) % itemsToPlaceInLudiChests.Length;
							// Alternate approach: Random instead of cyclical: chest.item[inventoryIndex].SetDefaults(Main.rand.Next(itemsToPlaceInLudiChests));
							break;
						}
					}
				}

			}
		}

		private void GenerateStructure(GenerationProgress progress)
		{
			progress.Message = " Generating Ludibrium Tower";
			bool placed = false;
			while (!placed)
			{

				Random rand = new Random();
				int DistanceFromDung;

				if (Main.dungeonX > Main.maxTilesX / 2) //rightside dungeon 
				{
					// defaults :  WorldGen.genRand.Next((Main.maxTilesX / 2) + 300, Main.maxTilesX - 500);
					DistanceFromDung = ((Main.maxTilesX / 2) + (Main.maxTilesX / 2) / 2 - 200);
					int StartX = (Main.rand.Next(DistanceFromDung + 110, DistanceFromDung + 120));
					{
						int StartY = (int)Main.rockLayer - 380;

						if (Main.maxTilesY == 1200)
						{
							StartY = (int)Main.worldSurface - 150;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower3", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
						else if (Main.maxTilesY == 1800)
						{
							StartY = (int)Main.rockLayer - 380;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower3", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
						else if (Main.maxTilesY == 2400)
						{
							// -600
							// si je descend le nombre, ca descend
							StartY = (int)Main.rockLayer - 433;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
					}
				}
				else //leftside dungeon
				{
					// defaults : WorldGen.genRand.Next(75, (Main.maxTilesX / 2) - 600);
					DistanceFromDung = ((Main.maxTilesX / 2) - (Main.maxTilesX / 2) / 2 - 125);
					int StartX = (Main.rand.Next(DistanceFromDung + 120, DistanceFromDung + 130));

					{
						int StartY = (int)Main.maxTilesY;

						if (Main.maxTilesY == 1200)
						{
							StartY = (int)Main.worldSurface - 150;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower3", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
						else if (Main.maxTilesY == 1800)
						{
							StartY = (int)Main.rockLayer - 380;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower3", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
						else if (Main.maxTilesY == 2400)
						{
							// -600
							// si je descend le nombre, ca descend
							StartY = (int)Main.rockLayer - 433;
							StructureHelper.StructureHelper.GenerateStructure("Structures/LudiTower", new Point16(StartX, StartY), TerraStory.Mod);
							placed = true;
						}
						int Startx = StartX + 52;
						int Starty = StartY + 50;
						WorldGen.PlaceChest(Startx + 1, Starty - 1, (ushort)ModContent.TileType<LudiChestTile>(), false, 2);
					}
				}

			}
		}
		public override void PostWorldGen()
		{

			// FIXED YAY
			foreach (Chest chest in Main.chest.Where(c => c != null))
			{
				// Get a chest
				var tile = Main.tile[chest.x, chest.y]; // the chest tile
				{
					if (tile.type == TileType<LudiChestTile>())
					{
						//chest.AddItem(Utils.SelectRandom(WorldGen.genRand, ItemType<Spoon>(), ItemType<Lollipop>(),
						//ItemType<FryingPan>(), ItemType<RedValentineRose>()));
						
						chest.AddItem(ItemType<WoodenTop>(), WorldGen.genRand.Next(0, 50));
						chest.AddItem(ItemType<PaperFighterPlane>(), WorldGen.genRand.Next(0, 50));
						chest.AddItem(ItemType<Orange>(), WorldGen.genRand.Next(0, 50));
						chest.AddItem(ItemType<Items.BundleOfMesos>(), WorldGen.genRand.Next(1, 5));
						chest.AddItem(ItemID.GoldCoin, WorldGen.genRand.Next(1, 3));
						chest.AddItem(ItemID.Rope, WorldGen.genRand.Next(10, 50));
					}
				}
			}
			
		}
		public override void PostUpdate()
		{
			Player player = Main.LocalPlayer;
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				if (!Lidium)
				{
					for (int k = 0; k < (int)((Main.maxTilesX * Main.maxTilesY * 1.13f) * 15E-05); k++)
					{
						int x = WorldGen.genRand.Next(100, Main.maxTilesX - 100);
						int y = WorldGen.genRand.Next((int)Main.rockLayer, Main.maxTilesY - 200);
						if (Main.tile[x, y] != null)
						{
							if (Main.tile[x, y].active())
							{
								if (Main.tile[x, y].type == 1)
								{
									WorldGen.OreRunner(x, y, WorldGen.genRand.Next(4, 9), WorldGen.genRand.Next(4, 9), (ushort)TileType<Tiles.LidiumOre>());
								}
							}
						}
					}
					Main.NewText("The world has been blessed with lidium!", 61, 255, 142);
					Lidium = true;
				}
			}
			
		}
		public static bool CheckFlat(int startX, int startY, int width, float threshold, int goingDownWeight = 0, int goingUpWeight = 0)
		{
			// Fail if the tile at the other end of the check plane isn't also solid
			if (!WorldGen.SolidTile(startX + width, startY)) return false;

			float totalVariance = 0;
			for (int i = 0; i < width; i++)
			{
				if (startX + i >= Main.maxTilesX) return false;

				// Fail if there is a tile very closely above the check area
				for (int k = startY - 1; k > startY - 100; k--)
				{
					if (WorldGen.SolidTile(startX + i, k)) return false;
				}

				// If the tile is solid, go up until we find air
				// If the tile is not, go down until we find a floor
				int offset = 0;
				bool goingUp = WorldGen.SolidTile(startX + i, startY);
				offset += goingUp ? goingUpWeight : goingDownWeight;
				while ((goingUp && WorldGen.SolidTile(startX + i, startY - offset))
					|| (!goingUp && !WorldGen.SolidTile(startX + i, startY + offset)))
				{
					offset++;
				}
				if (goingUp) offset--; // account for going up counting the first tile
				totalVariance += offset;
			}
			return totalVariance / width <= threshold;
		}

		public override void TileCountsAvailable(int[] tileCounts)
		{
			LudibriumTiles 
				= tileCounts[ModContent.TileType<OrangeLudiBlock>()]
				+ tileCounts[ModContent.TileType<CyanToyBlock>()]
				+ tileCounts[ModContent.TileType<DeepBlueToyBlock>()]
				+ tileCounts[ModContent.TileType<RedToyBlock>()]
				+ tileCounts[ModContent.TileType<YellowToyBlockTile>()]
				+ tileCounts[ModContent.WallType<SnowLudiWall>()]
				+ tileCounts[ModContent.WallType<OrangeLudiWall>()]
				+ tileCounts[ModContent.TileType<LudiTreeTile>()]
				+ tileCounts[ModContent.WallType<RedLudiWall>()]
				+ tileCounts[ModContent.WallType<DeepBlueLudiWall>()]
				+ tileCounts[ModContent.WallType<LudiTreeWall>()]
				+ tileCounts[ModContent.WallType<ToyWall>()]
				+ tileCounts[ModContent.TileType<PinkToyBlock>()]
				+ tileCounts[ModContent.TileType<GalaxyToyBlock>()];
		}
	}
}