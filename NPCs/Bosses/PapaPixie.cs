using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using System;

namespace TerraStory.NPCs.Bosses
{

	[AutoloadBossHead]

	public class PapaPixie : Hover
	{
		//AI
		private int ai;
		private int attackTimer;
		private bool fastSpeed = false;

		private bool stunned;
		private int stunnedTimer;

		// Animation
		private int frame = 0;
		private double counting;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Papa Pixie");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{

			npc.width = 120;
			npc.height = 140;

			npc.boss = true;
			npc.aiStyle = -1;
			aiType = NPCID.Wraith;
			npc.npcSlots = 5f;
			animationType = NPCID.Wraith;
			npc.scale = 0.99f;

			npc.lifeMax = 6000;
			npc.damage = 60;
			npc.defense = 8;
			npc.knockBackResist = 0f;

			npc.value = Item.buyPrice(gold: 10);
			npc.noGravity = true;
			npc.noTileCollide = true;
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/PapaPixieHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/PapaPixieDie");
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/PlotOfPixie");

			bossBag = mod.ItemType("PapaPixieTreasureBag");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

		public override void NPCLoot()
		{
			World.downedPapaPixie = true;
			if (Main.expertMode)
			{
				npc.DropBossBags();
			} else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, Main.rand.Next(1, 5));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("StarStaff"), 1);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.FallenStar, Main.rand.Next(1, 30));
				// Can add : if(Main.Hardmode) to add hardmode items
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0) /// this make so when npc die he will drop this
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/PapaPixieLostHat"), 1f);
			}
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100; i++)
				{
					Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, hitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
					dust.noGravity = true;
				}
				return;
			}
			for (int i = 0; i < 50; i++)
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, 2 * hitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
				dust.noGravity = true;
			}
		}

		// Only show health bar of the NPC when close to the player
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 200)
			{
				if (distance > 100)
				{
					// Make the health bar become smaller the farther away the NPC is.
					scale *= (100 - (distance - 100)) / 100;
				}
				return null;
			}
			return false;
		}

		// Make the NPC invisible when far away from the player.
		public override void CustomBehavior(ref float ai)
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 250)
			{
				npc.alpha = 100;
				if (distance > 100)
				{
					// Make the NPC fade out the farther away the NPC is.
					npc.alpha += (int)(155 * ((distance - 100) / 150));
				}
				return;
			}
			npc.alpha = 255;
		}

		public override void AI()
		{
			// Gets to the player and target Vector
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			Vector2 target = npc.HasPlayerTarget ? player.Center : Main.npc[npc.target].Center;
			// ensures that the npc is not rotated
			npc.rotation = 0.0f;
			npc.netAlways = true;
			npc.TargetClosest(true);
			// Ensures Npc life is not greater than its max life
			if (npc.life >= npc.lifeMax)
				npc.life = npc.lifeMax;
			// Handles despawning
			if (npc.target < 0 || npc.target == 255 || player.dead || !player.active)
			{
				npc.TargetClosest(false);
				npc.direction = 1;
				npc.velocity.Y = npc.velocity.Y - 0.1f;
				if (npc.timeLeft > 20)
				{
					npc.timeLeft = 20;
					return;
				}

			}
			//Stunned
			if (stunned)
			{
				npc.velocity.X = 0.0f;
				npc.velocity.Y = 0.0f;

				stunnedTimer++;

				if (stunnedTimer >= 100)
				{
					stunned = false;
					stunnedTimer = 0;
				}
			}
			// increment AI
			ai++;
			// Movement
			npc.ai[0] = (float)ai * 1f;
			int distance = (int)Vector2.Distance(target, npc.Center);
			if ((double)npc.ai[0] < 300)
			{
				frame = 0;
				MoveTowards(npc, target, (float)(distance > 300 ? 13f : 7f), 30f);
				npc.netUpdate = true;
			}
			else if ((double)npc.ai[0] >= 300 && (double)npc.ai[0] < 450.0)
			{
				stunned = true;
				frame = 1;
				npc.defense = 60;
				npc.damage = 30;
				MoveTowards(npc, target, (float)(distance > 300 ? 13f : 7f), 30f);
				npc.netUpdate = true;
			} else if ((double)npc.ai[0] >= 450.0)
			{
				frame = 0;
				stunned = false;
				npc.damage = 60;
				npc.defense = 30;
				if (!fastSpeed)
				{
					fastSpeed = true;
				} else
				{
					if ((double)npc.ai[0] % 50 == 0)
					{
						float speed = 12f;
						Vector2 vector = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
						float x = player.position.X + (float)(player.width / 2) - vector.X;
						float y = player.position.Y + (float)(player.height / 2) - vector.Y;
						float distance2 = (float)Math.Sqrt(x * x + y * y);
						float factor = speed / distance2;
						npc.velocity.X = x * factor;
						npc.velocity.Y = y * factor;
					}
					npc.netUpdate = true;
				}

				// Attack
				if ((double)npc.ai[0] % (Main.expertMode ? 100 : 150) == 0)
				{
					attackTimer++;
					if (attackTimer <= 2)
					{
						frame = 3;

						npc.velocity.X = 0f;
						Vector2 shootPos = npc.Center;
						float accuracy = 5f * (npc.life / npc.lifeMax);
						Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
						shootVel.Normalize();
						shootVel *= 14.5f;
						for (int i = 0; i < (Main.expertMode ? 5 : 3); i++)
						{
							Projectile.NewProjectile(shootPos.X + (float)(-100 * npc.direction) + (float)Main.rand.Next(-40, 41), shootPos.Y - (float)Main.rand.Next(-50, 40), shootVel.X, shootVel.Y, mod.ProjectileType("PapaPixieP"), npc.damage / 3, 5f);
						}
					}
					else
					{
						attackTimer = 0;
					}
				}

				if ((double)npc.ai[0] >= 650.0)
				{
					ai = 0;
					npc.alpha = 200;
					fastSpeed = false;
				}
			}
		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			if (frame == 0)
			{
				counting += 1.0;
				if (counting < 8.0)
				{
					npc.frame.Y = 0;
				} else if(counting < 16.0)
				{
					npc.frame.Y = frameHeight;
				} else if(counting < 24.0)
				{
					npc.frame.Y = frameHeight * 2;
				} else if (counting < 32.0)
				{
					npc.frame.Y = frameHeight * 3;
				} else
				{
					counting = 0.0;
				}
			}
			else if(frame == 1)
			{
				npc.frame.Y = frameHeight * 4;
			} else
			{
				npc.frame.Y = frameHeight * 5;
			}
		}

		public void MoveTowards(NPC npc, Vector2 playerTarget, float speed, float turnResistance)
		{
			var move = playerTarget - npc.Center;
			float length = move.Length();
			if (length > speed)
			{
				move *= speed / length;
			}
			move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
			length = move.Length();
			if (length > speed)
			{
				move *= speed / length;
			}
			npc.velocity = move;
        }
    }
}