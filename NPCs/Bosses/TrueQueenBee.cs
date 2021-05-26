using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using TerraStory.NPCs;
using static Terraria.ModLoader.ModContent;
using TerraStory.Dusts;
using Microsoft.Xna.Framework.Graphics;

namespace TerraStory.NPCs.Bosses
{
	[AutoloadBossHead]

	public class TrueQueenBee : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("TrueQueenBee");
			Main.npcFrameCount[npc.type] = 12;

		}
		public override void SetDefaults()
		{
			npc.width = 80;  // hitbox
			npc.height = 140;
			npc.boss = true;
			npc.aiStyle = -1;
			npc.npcSlots = 7f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/QueenBeeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/QueenBeeDie");
			npc.scale = 1f;
			npc.defense = 10;
			npc.lifeMax = 3800;
			npc.damage = 40;
			npc.knockBackResist = 0f;
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.value = Item.buyPrice(gold: 15);
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RedWitch");
			bossBag = mod.ItemType("TrueQueenBeeTreasureBag");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

		public override void NPCLoot()
		{
			World.downedTrueQueenBee = true;
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, Main.rand.Next(0, 1));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 20));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Mesos"), 1);
				// Can add : if(Main.Hardmode) to add hardmode items
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

		//NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModContent.NPCType<AngryBee>(), 0, npc.whoAmI, targetX, targetY, 2);
		//Projectile.NewProjectile(shootPos.X + (float) (-25 * npc.direction) + (float) Main.rand.Next(-10, 11), shootPos.Y - (float) Main.rand.Next(-10, 10), shootVel.X, shootVel.Y, mod.ProjectileType("RollingBee"), npc.damage / 4, 5f);

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			// Determines the animation speed . positive value ex: 0.5f = higher speed
			npc.frameCounter -= -11.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void AI()
		{
			{
				{
					int TrueQueenBee = 0;
					for (int Cible = 0; Cible < 255; Cible++)
					{
						if (Main.player[Cible].active && !Main.player[Cible].dead && (npc.Center - Main.player[Cible].Center).Length() < 1000f)
						{
							TrueQueenBee++;
						}
					}
					if (Main.expertMode)
					{
						int ScaleTheQueen = (int)(20f * (1f - (float)npc.life / (float)npc.lifeMax));
						npc.defense = npc.defDefense + ScaleTheQueen;

					}
					if (npc.target < 0 || npc.target == 225 || Main.player[npc.target].dead || !Main.player[npc.target].active)
					{
						npc.TargetClosest();
					}
					if (Main.player[npc.target].dead && Main.expertMode)
					{
						if ((double)npc.position.Y < Main.worldSurface * 16.0 + 2000.0)
						{
							npc.velocity.Y += 0.04f;
						}
						if (npc.position.X < (float)(Main.maxTilesX * 8))
						{
							npc.velocity.X -= 0.04f;
						}
						else
						{
							npc.velocity.X += 0.04f;
						}
						if (npc.timeLeft > 10)
						{
							npc.timeLeft = 10;
						}
					}
					else if (this.npc.ai[0] == -1f)
					{
						if (Main.netMode == NetmodeID.MultiplayerClient)
						{
							return;

						}
						float PhaseDeBase = this.npc.ai[1];
						int Phase;
						do
						{
							Phase = Main.rand.Next(3);
							switch (Phase)
							{
								case 1:
									Phase = 2;
									CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), new Color(255, 155, 0, 100), "Go, my children! GO!");
									break;
								case 2:
									Phase = 3;
									CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), new Color(255, 155, 0, 100), "Even if you run away they will catch up with you!");
									break;
							}
						}
						while ((float)Phase == PhaseDeBase);
						this.npc.ai[0] = Phase;
						this.npc.ai[1] = 0f;
						this.npc.ai[2] = 0f;
					}
					else if (this.npc.ai[0] == 0f)
					{
						int ChoosenPhase = 2;
						if (Main.expertMode)
						{
							if (npc.life < npc.lifeMax / 2)
							{
								ChoosenPhase++;
							}
							if (npc.life < npc.lifeMax / 3)
							{
								ChoosenPhase++;
							}
							if (npc.life < npc.lifeMax / 5)
							{
								ChoosenPhase++;
							}
						}
						if (this.npc.ai[1] > (float)(2 * ChoosenPhase) && this.npc.ai[1] % 2f == 0f)
						{
							this.npc.ai[0] = -1f;
							this.npc.ai[1] = 0f;
							this.npc.ai[2] = 0f;
							npc.netUpdate = true;
							return;
						}
                        #region Dash1
                        if (this.npc.ai[1] % 2f == 0f)
						{
							npc.TargetClosest();
							// Si la position Y du npc est plus petite de 20f du joueur
							if (Math.Abs(npc.position.Y + (float)(npc.height / 2) - (Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))) < 20f)
							{
								npc.localAI[0] = 1f;
								this.npc.ai[1] += 1f;
								this.npc.ai[2] = 0f;
								float QueenVelocity = 20f; //12f
								if (Main.expertMode)
								{
									QueenVelocity = 40f; // 16f
									// la vitesse du boss augmente a mesure que ca vie descend
									if ((double)npc.life < (double)npc.lifeMax * 0.75)
									{
										QueenVelocity += 4f;
									}
									if ((double)npc.life < (double)npc.lifeMax * 0.5)
									{
										QueenVelocity += 5f;
									}
									if ((double)npc.life < (double)npc.lifeMax * 0.25)
									{
										QueenVelocity += 6f;
									}
									if ((double)npc.life < (double)npc.lifeMax * 0.1)
									{
										QueenVelocity += 7f;
									}
								}
								// dash sur le joueur
								Vector2 QueenBeePos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
								float TargetPosX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - QueenBeePos.X;
								float TargetPosY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - QueenBeePos.Y;
								float TargetPos = (float)Math.Sqrt(TargetPosX * TargetPosX + TargetPosY * TargetPosY);
								TargetPos = QueenVelocity / TargetPos;
								npc.velocity.X = TargetPosX * TargetPos;
								npc.velocity.Y = TargetPosY * TargetPos;
								npc.spriteDirection = npc.direction;
								//Main.PlaySound(SoundLoader.customSoundType, (int)npc.position.X, (int)npc.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/QueenBeeSound"));
								return;
							}
                            #endregion
                            #region DashPas
                            // Lorsque fini de dasher sur le player
                            npc.localAI[0] = 0f;
							float QueenMaxVelocity = 12f;
							float QueenMinVelocity = 0.15f;
							if (Main.expertMode)
							{
								if ((double)npc.life < (double)npc.lifeMax * 0.75)
								{
									QueenMaxVelocity += 1f;
									QueenMinVelocity += 0.05f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.5)
								{
									QueenMaxVelocity += 1f;
									QueenMinVelocity += 0.05f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.25)
								{
									QueenMaxVelocity += 2f;
									QueenMinVelocity += 0.05f;
								}
								if ((double)npc.life < (double)npc.lifeMax * 0.1)
								{
									QueenMaxVelocity += 2f;
									QueenMinVelocity += 0.1f;
								}
							}
							// si le npc est plus bas que le joueur
							if (npc.position.Y + (float)(npc.height / 2) < Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2))
							{
								// essaie toujours detre plus haut
								npc.velocity.Y += QueenMinVelocity;
							}
							else
							{
								//sinon ralenti 
								npc.velocity.Y -= QueenMinVelocity;
							}
							// si il est a 12f en haut du joueur
							if (npc.velocity.Y < -12f)
							{
								// ne bouge plus
								npc.velocity.Y = 0f - QueenMaxVelocity;
							}
							// si le npc est plus bas de 12f du  joueur
							if (npc.velocity.Y > 12f)
							{
								// remonte rapidement en haut du joueur
								npc.velocity.Y = QueenMaxVelocity;
							}
							//si le joueur est plus loin horizontalement de 600f
							if (Math.Abs(npc.position.X + (float)(Main.player[npc.target].width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) > 600f)
							{
								// essai de se rapprocher tranquillement
								npc.velocity.X += 0.15f * (float)npc.direction;
							}
							// mais si le joueur est proche dau moin 300f
							else if (Math.Abs(npc.position.X + (float)(Main.player[npc.target].width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) < 300f)
							{
								//ralenti
								npc.velocity.X -= 0.15f * (float)npc.direction;
							}

							// maintient les vitesses constantes
							else
							{
								npc.velocity.X *= 0.8f;
							}
							if (npc.velocity.X < -16f)
							{
								npc.velocity.X = -16f;
							}
							if (npc.velocity.X > 16f)
							{
								npc.velocity.X = 16f;
							}
							npc.spriteDirection = npc.direction;
							return;
						}
						// si le npc est immobile
						if (npc.velocity.X < 0f)
						{
							// change de direction
							npc.direction = -1;
						}
						else
						{
							// et change de direction encore
							npc.direction = 1;
						}
						npc.spriteDirection = npc.direction;
						int DistanceDuDash = 600;
						if (Main.expertMode)
						{
							if ((double)npc.life < (double)npc.lifeMax * 0.1)
							{
								DistanceDuDash = 300;
							}
							else if ((double)npc.life < (double)npc.lifeMax * 0.25)
							{
								DistanceDuDash = 450;
							}
							else if ((double)npc.life < (double)npc.lifeMax * 0.5)
							{
								DistanceDuDash = 500;
							}
							else if ((double)npc.life < (double)npc.lifeMax * 0.75)
							{
								DistanceDuDash = 550;
							}
						}
						int DirectionBoss = 1;

						// si le npc est directement sur le joueur
						if (npc.position.X + (float)(npc.width / 2) < Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))
						{
							//stabilise la direction
							DirectionBoss = -1;
						}
						// si le boss etait immobile et que la distance entre le joeur et lui est plus grande que son range de dash
						if (npc.direction == DirectionBoss && Math.Abs(npc.position.X + (float)(npc.width / 2) - (Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2))) > (float)DistanceDuDash)
						{
							// change de phase
							this.npc.ai[2] = 1f;
						}
						// si il a changer de phase
						if (this.npc.ai[2] == 1f)
						{
							//commence a le chasser
							npc.TargetClosest();
							npc.spriteDirection = npc.direction;
							npc.localAI[0] = 0f;
							npc.velocity *= 0.9f;
							float minVelocity = 0.1f;
							if (Main.expertMode)
							{
								if (npc.life < npc.lifeMax / 2)
								{
									npc.velocity *= 0.9f;
									minVelocity += 0.05f;
								}
								if (npc.life < npc.lifeMax / 3)
								{
									npc.velocity *= 0.9f;
									minVelocity += 0.05f;
								}
								if (npc.life < npc.lifeMax / 5)
								{
									npc.velocity *= 0.9f;
									minVelocity += 0.05f;
								}
							}
							if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < minVelocity)
							{
								this.npc.ai[2] = 0f;
								this.npc.ai[1] += 1f;
							}
						}
						else
						{
							npc.localAI[0] = 1f;
						}
					}
                    #endregion
                    #region Dash2
                    // si a terminer le dash, redash
                    else if (this.npc.ai[0] == 2f)
					{
						npc.TargetClosest();
						npc.spriteDirection = npc.direction;
						float num1147 = 12f;
						float num1148 = 0.07f;
						if (Main.expertMode)
						{
							num1148 = 0.1f;
						}
						Vector2 BossPos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float PlayerPosX2 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - BossPos.X;
						float PlayerPosY2 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 200f - BossPos.Y;
						float PlayerPos2 = (float)Math.Sqrt(PlayerPosX2 * PlayerPosX2 + PlayerPosY2 * PlayerPosY2);

						// si est plus loing de 200f
						if (PlayerPos2 < 200f)
						{
							this.npc.ai[0] = 1f;
							this.npc.ai[1] = 0f;
							npc.netUpdate = true;
							return;
						}
						PlayerPos2 = num1147 / PlayerPos2;
						if (npc.velocity.X < PlayerPosX2)
						{
							npc.velocity.X += num1148;
							if (npc.velocity.X < 0f && PlayerPosX2 > 0f)
							{
								npc.velocity.X += num1148;
							}
						}
						else if (npc.velocity.X > PlayerPosX2)
						{
							npc.velocity.X -= num1148;
							if (npc.velocity.X > 0f && PlayerPosX2 < 0f)
							{
								npc.velocity.X -= num1148;
							}
						}
						if (npc.velocity.Y < PlayerPosY2)
						{
							npc.velocity.Y += num1148;
							if (npc.velocity.Y < 0f && PlayerPosY2 > 0f)
							{
								npc.velocity.Y += num1148;
							}
						}
						else if (npc.velocity.Y > PlayerPosY2)
						{
							npc.velocity.Y -= num1148;
							if (npc.velocity.Y > 0f && PlayerPosY2 < 0f)
							{
								npc.velocity.Y -= num1148;
							}
						}
					}
                    #endregion
                    #region shoot
                    else if (this.npc.ai[0] == 1f)
					{
						npc.localAI[0] = 0f;
						npc.TargetClosest();
						Vector2 QueenPos = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
						Vector2 QueenPos2 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float targetPosX2 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - QueenPos2.X;
						float targetPosY2 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - QueenPos2.Y;
						float targetPos = (float)Math.Sqrt(targetPosX2 * targetPosX2 + targetPosY2 * targetPosY2);
						this.npc.ai[1] += 1f;
						if (Main.expertMode)
						{
							this.npc.ai[1] += TrueQueenBee / 2;
							if ((double)npc.life < (double)npc.lifeMax * 0.75)
							{
								this.npc.ai[1] += 0.25f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.5)
							{
								this.npc.ai[1] += 0.25f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.25)
							{
								this.npc.ai[1] += 0.25f;
							}
							if ((double)npc.life < (double)npc.lifeMax * 0.1)
							{
								this.npc.ai[1] += 0.25f;
							}
						}
						bool flag42 = false;
						if (this.npc.ai[1] > 40f)
						{
							this.npc.ai[1] = 0f;
							this.npc.ai[2]++;
							flag42 = true;
						}
						if (Collision.CanHit(QueenPos, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && flag42)
						{
							Main.PlaySound(SoundID.NPCHit, (int)npc.position.X, (int)npc.position.Y);
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								int cryBee = ModContent.NPCType<CryBee>();
								int angryBee = ModContent.NPCType<AngryBee>();
								//int ChooseChild = ModContent.NPCType<AngryBee>();
								int AngryBee = NPC.NewNPC((int)QueenPos.X, (int)QueenPos.Y, angryBee);
								int CryBee = NPC.NewNPC((int)QueenPos.X, (int)QueenPos.Y, cryBee);
								int choosenChild = AngryBee + CryBee;
								Main.npc[choosenChild].velocity.X = (float)Main.rand.Next(-200, 201) * 0.010f;
								Main.npc[choosenChild].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.010f;
								Main.npc[choosenChild].localAI[0] = 60f;
								Main.npc[choosenChild].netUpdate = true;
							}
						}
                        #endregion
                        #region Si le joueur est out of range
                        if (targetPos > 400f || !Collision.CanHit(new Vector2(QueenPos.X, QueenPos.Y - 30f), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							float QueenMaxVelocity = 14f;
							float QueenMinVelocity = 0.1f;
							QueenPos2 = QueenPos;
							targetPosX2 = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - QueenPos2.X;
							targetPosY2 = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - QueenPos2.Y;
							targetPos = (float)Math.Sqrt(targetPosX2 * targetPosX2 + targetPosY2 * targetPosY2);
							targetPos = QueenMaxVelocity / targetPos;
							if (npc.velocity.X < targetPosX2)
							{
								npc.velocity.X += QueenMinVelocity;
								if (npc.velocity.X < 0f && targetPosX2 > 0f)
								{
									npc.velocity.X += QueenMinVelocity;
								}
							}
							else if (npc.velocity.X > targetPosX2)
							{
								npc.velocity.X -= QueenMinVelocity;
								if (npc.velocity.X > 0f && targetPosX2 < 0f)
								{
									npc.velocity.X -= QueenMinVelocity;
								}
							}
							if (npc.velocity.Y < targetPosY2)
							{
								npc.velocity.Y += QueenMinVelocity;
								if (npc.velocity.Y < 0f && targetPosY2 > 0f)
								{
									npc.velocity.Y += QueenMinVelocity;
								}
							}
							else if (npc.velocity.Y > targetPosY2)
							{
								npc.velocity.Y -= QueenMinVelocity;
								if (npc.velocity.Y > 0f && targetPosY2 < 0f)
								{
									npc.velocity.Y -= QueenMinVelocity;
								}
							}
						}
						else
						{
							npc.velocity *= 0.9f;
						}
						npc.spriteDirection = npc.direction;
						if (this.npc.ai[2] > 5f)
						{
							this.npc.ai[0] = -1f;
							this.npc.ai[1] = 1f;
							npc.netUpdate = true;
						}
					}
					#endregion
					#region spawns des bees
					else
                    {
						if (this.npc.ai[0] != 3f)
						{
							return;
						}
						float BossMaxVelocity = 4f;
						float BossMinVelocity = 0.05f;
						if (Main.expertMode)
						{
							BossMinVelocity = 0.075f;
							BossMaxVelocity = 6f;
						}
						Vector2 QueenPos = new Vector2(npc.position.X + (float)(npc.width / 2) + (float)(Main.rand.Next(20) * npc.direction), npc.position.Y + (float)npc.height * 0.8f);
						Vector2 newQueenPos = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float targetposX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - newQueenPos.X;
						float targetposY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - 300f - newQueenPos.Y;
						float targetpos = (float)Math.Sqrt(targetposX * targetposX + targetposY * targetposY);
						this.npc.ai[1] += 1f;
						bool DoitSpawnerDesBees = false;
						if (Main.expertMode)
						{
							if ((double)npc.life < (double)npc.lifeMax * 0.1)
							{
								if (this.npc.ai[1] % 15f == 14f)
								{
									DoitSpawnerDesBees = true;
								}
							}
							else if (npc.life < npc.lifeMax / 3)
							{
								if (this.npc.ai[1] % 25f == 24f)
								{
									DoitSpawnerDesBees = true;
								}
							}
							else if (npc.life < npc.lifeMax / 2)
							{
								if (this.npc.ai[1] % 30f == 29f)
								{
									DoitSpawnerDesBees = true;
								}
							}
							else if (this.npc.ai[1] % 35f == 34f)
							{
								DoitSpawnerDesBees = true;
							}
						}
						else if (this.npc.ai[1] % 40f == 39f)
						{
							DoitSpawnerDesBees = true;
						}
						if (DoitSpawnerDesBees && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(QueenPos, 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							Main.PlaySound(SoundID.Item17, npc.position);
							if (Main.netMode != NetmodeID.MultiplayerClient)
							{
								float num1165 = 8f;
								if (Main.expertMode)
								{
									num1165 += 2f;
								}
								if (Main.expertMode && (double)npc.life < (double)npc.lifeMax * 0.1)
								{
									num1165 += 3f;
								}
								float TargetPosX = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - QueenPos.X + (float)Main.rand.Next(-80, 81);
								float TargetPosY = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - QueenPos.Y + (float)Main.rand.Next(-40, 41);
								float TargetPos = (float)Math.Sqrt(TargetPosX * TargetPosX + TargetPosY * TargetPosY);
								TargetPos = num1165 / TargetPos;
								TargetPosX *= TargetPos;
								TargetPosY *= TargetPos;
								int RollingBeesDmg = 40;  //defaults = 11 
								int ShootRollingBees = mod.ProjectileType("RollingBee");
								int RollingBees = Projectile.NewProjectile(QueenPos.X, QueenPos.Y, TargetPosX, TargetPosY, ShootRollingBees, RollingBeesDmg, 0f, Main.myPlayer, 5);
								Main.projectile[RollingBees].timeLeft = 300;
							}
						}
						if (!Collision.CanHit(new Vector2(QueenPos.X, QueenPos.Y - 30f), 1, 1, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height))
						{
							BossMaxVelocity = 14f;
							BossMinVelocity = 0.1f;
							newQueenPos = QueenPos;
							targetposX = Main.player[npc.target].position.X + (float)(Main.player[npc.target].width / 2) - newQueenPos.X;
							targetposY = Main.player[npc.target].position.Y + (float)(Main.player[npc.target].height / 2) - newQueenPos.Y;
							targetpos = (float)Math.Sqrt(targetposX * targetposX + targetposY * targetposY);
							targetpos = BossMaxVelocity / targetpos;
							if (npc.velocity.X < targetposX)
							{
								npc.velocity.X += BossMinVelocity;
								if (npc.velocity.X < 0f && targetposX > 0f)
								{
									npc.velocity.X += BossMinVelocity;
								}
							}
							else if (npc.velocity.X > targetposX)
							{
								npc.velocity.X -= BossMinVelocity;
								if (npc.velocity.X > 0f && targetposX < 0f)
								{
									npc.velocity.X -= BossMinVelocity;
								}
							}
							if (npc.velocity.Y < targetposY)
							{
								npc.velocity.Y += BossMinVelocity;
								if (npc.velocity.Y < 0f && targetposY > 0f)
								{
									npc.velocity.Y += BossMinVelocity;
								}
							}
							else if (npc.velocity.Y > targetposY)
							{
								npc.velocity.Y -= BossMinVelocity;
								if (npc.velocity.Y > 0f && targetposY < 0f)
								{
									npc.velocity.Y -= BossMinVelocity;
								}
							}
						}
						else if (targetpos > 100f)
						{
							npc.TargetClosest();
							npc.spriteDirection = npc.direction;
							targetpos = BossMaxVelocity / targetpos;
							if (npc.velocity.X < targetposX)
							{
								npc.velocity.X += BossMinVelocity;
								if (npc.velocity.X < 0f && targetposX > 0f)
								{
									npc.velocity.X += BossMinVelocity * 2f;
								}
							}
							else if (npc.velocity.X > targetposX)
							{
								npc.velocity.X -= BossMinVelocity;
								if (npc.velocity.X > 0f && targetposX < 0f)
								{
									npc.velocity.X -= BossMinVelocity * 2f;
								}
							}
							if (npc.velocity.Y < targetposY)
							{
								npc.velocity.Y += BossMinVelocity;
								if (npc.velocity.Y < 0f && targetposY > 0f)
								{
									npc.velocity.Y += BossMinVelocity * 2f;
								}
							}
							else if (npc.velocity.Y > targetposY)
							{
								npc.velocity.Y -= BossMinVelocity;
								if (npc.velocity.Y > 0f && targetposY < 0f)
								{
									npc.velocity.Y -= BossMinVelocity * 2f;
								}
							}
						}
						if (this.npc.ai[1] > 800f)
						{
							this.npc.ai[0] = -1f;
							this.npc.ai[1] = 3f;
							npc.netUpdate = true;
						}
					}
                    #endregion
                    Color color;
					color = Color.White;
					Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<TrueQueenBeeDust>(), npc.velocity.X, npc.velocity.Y, 0, color, 1f);
					if (npc.velocity.X <= 0.15f || npc.velocity.Y <= 0.15f)
					{
						return;
					}
				}
			}
		}
	}
}