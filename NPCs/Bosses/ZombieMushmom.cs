 using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Buffs;
using TerraStory.Dusts;
using Terraria.Localization;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.NPCs.Bosses
{

	[AutoloadBossHead]

	public class ZombieMushmom : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Zombie Mushmom");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{

			npc.width = 120;
			npc.height = 140;

			npc.boss = true;
			npc.aiStyle = -1; // Slime AI =1 , 15 = king slime, -1 = Custom AI
			aiType = NPCID.KingSlime;
			npc.npcSlots = 3f;
			animationType = NPCID.KingSlime;
			npc.noTileCollide = false;
			npc.scale = 0.99f;

			npc.lifeMax = 1700;
			npc.damage = 50;
			npc.defense = 13;
			npc.knockBackResist = 0f;

			npc.value = Item.buyPrice(gold: 1);
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MushmomHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MushmomDie");
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RedWitch");

			bossBag = mod.ItemType("ZombieMushmomTreasureBag");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

		public override void NPCLoot()
		{
			World.downedZombieMushmom = true;
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, Main.rand.Next(1, 2));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.ViciousMushroom, Main.rand.Next(1, 10));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.VileMushroom, Main.rand.Next(1, 10));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LesserHealingPotion, Main.rand.Next(3, 10));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LesserManaPotion, Main.rand.Next(1, 3));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Tobi>(), Main.rand.Next(10, 30));
				if (Main.rand.NextFloat() < .10f) // 10% chance
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ZombieMushmomTrophy"), 1);

				/*
				if (Main.rand.NextFloat() < .14f) // 14% chance
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MushmomCap"), 1);
				if (Main.rand.NextFloat() < .10f) // 10% chance
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("KinoBadge"), 1);
				if (Main.rand.NextFloat() < .10f) // 10% chance
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MushroomMount"), 1);
				int choice = Main.rand.Next(3);
				if (choice == 0)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MushroomSword"), 1);
				if (choice == 1)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OrangeMushroomStaff"), 1);
				if (choice == 2)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BigMushroom"), 1);
				// Can add : if(Main.Hardmode) to add hardmode items*/
			}
		}
	
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.LesserHealingPotion;
		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
		}
		/*
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0) /// this make so when npc die he will drop this
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrangeMushmomCorpse"), 1f); // 1f = Sprite size
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OrangeMushmomCap"), 1f);
			}
		}*/
		public override void PostAI()
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 400 && npc.velocity.Y == 0 && Main.player[npc.target].velocity.Y == 0)
			{
				Main.player[npc.target].AddBuff(BuffType<ZombieMushmomA>(), 2, true);
			}
			if (npc.velocity.Y == 0)
			{
				for (int dust = 0; dust < 10; dust++)
				{
					Dust dust37;
					Dust dust81;
					int dust1 = Dust.NewDust(npc.Bottom + Vector2.UnitX * -40f + Vector2.UnitY * -30f, npc.width, npc.height / 4, 4, npc.velocity.X, npc.velocity.Y);
					Main.dust[dust1].noGravity = false;
					dust37 = Main.dust[dust1];
					dust81 = dust37;
					dust81.velocity *= 0.5f;
				}
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Main.NewText("Zombie mushmom : I really wanted to play some more...", Color.Red);

			}
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return 
				World.downedZombieMushmom && 
				!Main.dayTime ? 0.001f : 0f;
		}
		public override void AI()
		{
			float num726 = 1f;
			bool SePrepareAseTeleport = false;
			bool DropGore = false;
			npc.aiAction = 0;
			if (this.npc.ai[3] == 0f && npc.life > 0)
			{
				this.npc.ai[3] = npc.lifeMax;
			}
			if (npc.localAI[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
			{
				this.npc.ai[0] = -100f;
				npc.localAI[3] = 1f;
				npc.TargetClosest();
				npc.netUpdate = true;
			}
			if (Main.player[npc.target].dead)
			{
				npc.TargetClosest();
				if (Main.dayTime)
                {
					npc.timeLeft = 0;
                }
				if (Main.player[npc.target].dead)
				{
					npc.timeLeft = 0;
					if (Main.player[npc.target].Center.X < npc.Center.X)
					{
						npc.direction = 1;
					}
					else
					{
						npc.direction = -1;
					}
				}
			}
			if (!Main.player[npc.target].dead && this.npc.ai[2] >= 300f && this.npc.ai[1] < 5f && npc.velocity.Y == 0f)
			{
				this.npc.ai[2] = 0f;
				this.npc.ai[0] = 0f;
				this.npc.ai[1] = 5f;
				if (Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.TargetClosest(faceTarget: true);
					Point NpcTile = npc.Center.ToTileCoordinates();
					Point TargetTile = Main.player[npc.target].Center.ToTileCoordinates();
					Vector2 Distance = Main.player[npc.target].Center - npc.Center;
					int TileLine = 10;
					int TileDistX = 0;
					int TileDistY = 7;
					int Timer = 0;
					bool Despawn = false;
					if (Distance.Length() > 2000f)
					{
						Despawn = true;
						Timer = 100;
					}
					while (!Despawn && Timer < 100)
					{
						Timer++;
						int TargetTilePosX = Main.rand.Next(TargetTile.X - TileLine, TargetTile.X + TileLine + 1);
						int TargetTilePosY = Main.rand.Next(TargetTile.Y - TileLine, TargetTile.Y + 1);
						if ((TargetTilePosY >= TargetTile.Y - TileDistY && TargetTilePosY <= TargetTile.Y + TileDistY && TargetTilePosX >= TargetTile.X - TileDistY && TargetTilePosX <= TargetTile.X + TileDistY) || (TargetTilePosY >= NpcTile.Y - TileDistX && TargetTilePosY <= NpcTile.Y + TileDistX && TargetTilePosX >= NpcTile.X - TileDistX && TargetTilePosX <= NpcTile.X + TileDistX) || Main.tile[TargetTilePosX, TargetTilePosY].nactive())
						{
							continue;
						}
						int TileY = TargetTilePosY;
						int TileX = 0;
						if (Main.tile[TargetTilePosX, TileY].nactive() && Main.tileSolid[Main.tile[TargetTilePosX, TileY].type] && !Main.tileSolidTop[Main.tile[TargetTilePosX, TileY].type])
						{
							TileX = 1;
						}
						else
						{
							for (; TileX < 150 && TileY + TileX < Main.maxTilesY; TileX++)
							{
								int TileXY = TileY + TileX;
								if (Main.tile[TargetTilePosX, TileXY].nactive() && Main.tileSolid[Main.tile[TargetTilePosX, TileXY].type] && !Main.tileSolidTop[Main.tile[TargetTilePosX, TileXY].type])
								{
									TileX--;
									break;
								}
							}
						}
						TargetTilePosY += TileX;
						bool Teleport = true;
						if (Teleport && Main.tile[TargetTilePosX, TargetTilePosY].lava())
						{
							Teleport = false;
						}
						if (Teleport && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
						{
							Teleport = false;
						}
						if (Teleport)
						{
							npc.localAI[1] = TargetTilePosX * 16 + 8;
							npc.localAI[2] = TargetTilePosY * 16 + 16;
							Despawn = true;
							break;
						}
					}
					if (Timer >= 100)
					{
						Vector2 bottom = Main.player[Player.FindClosest(npc.position, npc.width, npc.height)].Bottom;
						npc.localAI[1] = bottom.X;
						npc.localAI[2] = bottom.Y;
					}
				}
			}
			if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
			{
				this.npc.ai[2]++;
			}
			if (Math.Abs(npc.Top.Y - Main.player[npc.target].Bottom.Y) > 320f)
			{
				this.npc.ai[2]++;
			}
			Dust dust37;
			Dust dust81;
			if (this.npc.ai[1] == 5f)
			{
				SePrepareAseTeleport = true;
				npc.aiAction = 1;
				this.npc.ai[0]++;
				num726 = MathHelper.Clamp((60f - this.npc.ai[0]) / 60f, 0f, 1f);
				num726 = 0.5f + num726 * 0.5f;
				if (this.npc.ai[0] >= 60f)
				{
					DropGore = true;
				}
				/*if (this.npc.ai[0] == 60f)
				{
					Gore.NewGore(npc.Center + new Vector2(-40f, -npc.height / 2), npc.velocity, 734);
				}*/
				if (this.npc.ai[0] >= 60f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					npc.Bottom = new Vector2(npc.localAI[1], npc.localAI[2]);
					this.npc.ai[1] = 6f;
					this.npc.ai[0] = 0f;
					npc.netUpdate = true;
				}
				if (Main.netMode == NetmodeID.MultiplayerClient && this.npc.ai[0] >= 120f)
				{
					this.npc.ai[1] = 6f;
					this.npc.ai[0] = 0f;
				}
				if (!DropGore)
				{
					for (int dust = 0; dust < 10; dust++)
					{
						int dust1 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, ModContent.DustType<TransparentDust>(), new Color(253, 245, 230, 80), 2f);
						Main.dust[dust1].noGravity = true;
						dust37 = Main.dust[dust1];
						dust81 = dust37;
						dust81.velocity *= 0.5f;
					}
				}
			}
			else if (this.npc.ai[1] == 6f)
			{
				SePrepareAseTeleport = true;
				npc.aiAction = 0;
				this.npc.ai[0]++;
				num726 = MathHelper.Clamp(this.npc.ai[0] / 30f, 0f, 1f);
				num726 = 0.5f + num726 * 0.5f;
				if (this.npc.ai[0] >= 30f && Main.netMode != NetmodeID.MultiplayerClient)
				{
					this.npc.ai[1] = 0f;
					this.npc.ai[0] = 0f;
					npc.netUpdate = true;
					npc.TargetClosest();
				}
				if (Main.netMode == NetmodeID.MultiplayerClient && this.npc.ai[0] >= 60f)
				{
					this.npc.ai[1] = 0f;
					this.npc.ai[0] = 0f;
					npc.TargetClosest();
				}
				for (int HitDust = 0; HitDust < 10; HitDust++)
				{
					int dust0 = Dust.NewDust(npc.position + Vector2.UnitX * -20f, npc.width + 40, npc.height, 4, npc.velocity.X, npc.velocity.Y, ModContent.DustType<TransparentDust>(), new Color(253, 245, 230, 80), 2f);
					Main.dust[dust0].noGravity = true;
					dust37 = Main.dust[dust0];
					dust81 = dust37;
					dust81.velocity *= 2f;
				}
			}
			npc.dontTakeDamage = (npc.hide = DropGore);
			if (npc.velocity.Y == 0f)
			{
				npc.velocity.X *= 0.8f;
				if ((double)npc.velocity.X > -0.1 && (double)npc.velocity.X < 0.1)
				{
					npc.velocity.X = 0f;
				}
				if (!SePrepareAseTeleport)
				{
					this.npc.ai[0] += 2f;
					if ((double)npc.life < (double)npc.lifeMax * 0.8)
					{
						this.npc.ai[0] += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.6)
					{
						this.npc.ai[0] += 1f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.4)
					{
						this.npc.ai[0] += 2f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.2)
					{
						this.npc.ai[0] += 3f;
					}
					if ((double)npc.life < (double)npc.lifeMax * 0.1)
					{
						this.npc.ai[0] += 4f;
					}
					if (this.npc.ai[0] >= 0f)
					{
						npc.netUpdate = true;
						npc.TargetClosest();
						if (this.npc.ai[1] == 3f)
						{
							npc.velocity.Y = -13f;
							npc.velocity.X += 3.5f * (float)npc.direction;
							this.npc.ai[0] = -200f;
							this.npc.ai[1] = 0f;
						}
						else if (this.npc.ai[1] == 2f)
						{
							npc.velocity.Y = -6f;
							npc.velocity.X += 4.5f * (float)npc.direction;
							this.npc.ai[0] = -120f;
							this.npc.ai[1] += 1f;
						}
						else
						{
							npc.velocity.Y = -8f;
							npc.velocity.X += 4f * (float)npc.direction;
							this.npc.ai[0] = -120f;
							this.npc.ai[1] += 1f;
						}
					}
					else if (this.npc.ai[0] >= -30f)
					{
						npc.aiAction = 1;
					}
				}
			}
			else if (npc.target < 255 && ((npc.direction == 1 && npc.velocity.X < 3f) || (npc.direction == -1 && npc.velocity.X > -3f)))
			{
				if ((npc.direction == -1 && (double)npc.velocity.X < 0.1) || (npc.direction == 1 && (double)npc.velocity.X > -0.1))
				{
					npc.velocity.X += 0.2f * (float)npc.direction;
				}
				else
				{
					npc.velocity.X *= 0.93f;
				}
			}
			int num741 = Dust.NewDust(npc.position, npc.width, npc.height, 4, npc.velocity.X, npc.velocity.Y, ModContent.DustType<TransparentDust>(), new Color(253, 245, 230, 80), npc.scale * 1.2f);
			Main.dust[num741].noGravity = true;
			dust37 = Main.dust[num741];
			dust81 = dust37;
			dust81.velocity *= 0.5f;
			if (npc.life <= 0)
			{
				return;
			}
			/*float MushScale = (float)npc.life / (float)npc.lifeMax;
			MushScale = MushScale * 0.5f + 0.75f;
			MushScale *= num726;
			if (MushScale != npc.scale)
			{
				npc.position.X += npc.width / 2;
				npc.position.Y += npc.height;
				npc.scale = MushScale;
				npc.width = (int)(98f * npc.scale);
				npc.height = (int)(92f * npc.scale);
				npc.position.X -= npc.width / 2;
				npc.position.Y -= npc.height;
			}*/
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				return;
			}
			int num743 = (int)((double)npc.lifeMax * 0.05);
			if (!((float)(npc.life + num743) < this.npc.ai[3]))
			{
				return;
			}
			this.npc.ai[3] = npc.life;
			int num745 = Main.rand.Next(1, 4);
			for (int num746 = 0; num746 < num745; num746++)
			{
				int x = (int)(npc.position.X + (float)Main.rand.Next(npc.width - 32));
				int y = (int)(npc.position.Y + (float)Main.rand.Next(npc.height - 32));
				int MushHelpers = NPCType<ZombieMushroom>();
				if (Main.expertMode && Main.rand.Next(4) == 0)
				{
					MushHelpers = NPCType<ZombieMushroom>();
				}
				int num748 = NPC.NewNPC(x, y, MushHelpers);
				Main.npc[num748].SetDefaults(MushHelpers);
				Main.npc[num748].velocity.X = (float)Main.rand.Next(-15, 16) * 0.1f;
				Main.npc[num748].velocity.Y = (float)Main.rand.Next(-30, 1) * 0.1f;
				Main.npc[num748].ai[0] = -1000 * Main.rand.Next(3);
				Main.npc[num748].ai[1] = 0f;
				if (Main.netMode == NetmodeID.Server && num748 < 200)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num748);
				}
			}
		}
	}
}