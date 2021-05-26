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
using TerraStory.Projectiles.Bosses;

namespace TerraStory.NPCs.Bosses
{

	[AutoloadBossHead]

	public class Timer : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Timer");
			Main.npcFrameCount[npc.type] = 26;
		}

		public override void SetDefaults()
		{

			npc.width = 100;
			npc.height = 150;
			npc.aiStyle = -1;
			npc.npcSlots = 3f;
			npc.scale = 0.99f;
			npc.lifeMax = 1500;
			npc.damage = 40;
			npc.defense = 10;
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(gold: 1);
			npc.boss = true;
			npc.lavaImmune = true;
			npc.noTileCollide = false;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/TimerHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/TimerDie");
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RedWitch");
			npc.gfxOffY = + 40f;
			bossBag = mod.ItemType("TimerTreasureBag");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}
		/*
		public override void NPCLoot()
		{
			World.downedBlueMushmom = true;
			if (Main.expertMode)
			{
				npc.DropBossBags();
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, Main.rand.Next(0, 1));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GlowingMushroom, Main.rand.Next(1, 20));
				if (Main.rand.NextFloat() < .10f) // 10% chance
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueMushmomTrophy"), 1);
				// Can add : if(Main.Hardmode) to add hardmode items
			}
		}*/

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Main.NewText("Timer : X X XXXXXX XXX X X XX", Color.Red);
			}
		}

		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.HealingPotion;
		}
		/*
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -25.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}*/
		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			if (frame == 0)
			{
				counting += 1.0;
				if (counting < 8.0)
				{
					npc.frame.Y = 0;  // frame 0
				}
				else if (counting < 12.0)
				{
					npc.frame.Y = frameHeight;  // frame 1
				}
				else if (counting < 24.0)
				{
					npc.frame.Y = frameHeight * 2; //frame 2
				}
				else if (counting < 32.0)
				{
					npc.frame.Y = frameHeight * 3; //frame 3..
				}
				else if (counting < 44.0)
				{
					npc.frame.Y = frameHeight * 4; //frame 4..
				}
				else if (counting < 56.0)
				{
					npc.frame.Y = frameHeight * 5; //frame 5..
				}
				else if (counting < 68.0)
				{
					npc.frame.Y = frameHeight * 6; //frame 6..
				}
				else
				{
					counting = 0.0;
				}
			}
			else if (frame == 8)
			{

				counting += 1.0;
				if (counting < 8.0)
				{
					npc.frame.Y = frameHeight * 7; //frame 7..
				}
				if (counting < 12.0)
				{
					npc.frame.Y = frameHeight * 8;
				}
				else if (counting < 24.0)
				{
					npc.frame.Y = frameHeight * 9;
				}
				else if (counting < 32.0)
				{
					npc.frame.Y = frameHeight * 10;
				}
				else if (counting < 44.0)
				{
					npc.frame.Y = frameHeight * 11;
				}
				else if (counting < 56.0)
				{
					npc.frame.Y = frameHeight * 12;
				}
				else if (counting < 68.0)
				{
					npc.frame.Y = frameHeight * 13;
				}
				else if (counting < 80.0)
				{
					npc.frame.Y = frameHeight * 14;
				}
				else if (counting < 92.0)
				{
					npc.frame.Y = frameHeight * 15;
				}
				else if (counting < 104.0)
				{
					npc.frame.Y = frameHeight * 16;
				}
				else if (counting < 116.0)
				{
					npc.frame.Y = frameHeight * 17;
				}
				else if (counting < 128.0)
				{
					npc.frame.Y = frameHeight * 18;
				}
				else if (counting < 140.0)
				{
					npc.frame.Y = frameHeight * 19;
				}
				else if (counting < 152.0)
				{
					npc.frame.Y = frameHeight * 20;
				}
				else if (counting < 164.0)
				{
					npc.frame.Y = frameHeight * 21;
				}
				else if (counting < 176.0)
				{
					npc.frame.Y = frameHeight * 22;
				}
				else if (counting < 188.0)
				{
					npc.frame.Y = frameHeight * 23;
				}
				else if (counting < 200.0)
				{
					npc.frame.Y = frameHeight * 24;
				}
				else
				{
					counting = 0.0;
				}
			}
		}
		//AI
		private int ai;
		private int attackTimer;
		private bool fastSpeed = false;
		private bool falling;
		private int fallingTimer;

		// Animation
		private int frame = 0;
		private double counting;

		public override void AI()
		{

			#region ActiveCheck

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
			#endregion

			#region AI

			if (falling)
			{
				fallingTimer++;

				if (fallingTimer >= 100)
				{
					falling = false;
					fallingTimer = 0;
				}
			}
			ai++;

			npc.ai[0] = ai * 1f;
			int distance = (int)Vector2.Distance(target, npc.Center);
			if ((double)npc.ai[0] < 300)
			{
				frame = 0;
				MoveTowards(npc, target, (float)(distance > 300 ? 10f : 5f), 20f);
				npc.netUpdate = true;
			} 
			else if ((double)npc.ai[0] >= 300 && npc.ai[0] < 450.0)
            {
				falling = true;
				frame = 8;
				npc.defense = 60;
				npc.damage = 60;
				MoveTowards(npc, target, (float)(distance > 300 ? 10f : 5f), 20f);
				npc.netUpdate = true;
            }
			else if ((double)npc.ai[0] >= 450.0)
            {
				frame = 0;
				falling = false;
				npc.damage = 30;
				npc.defense = 30;
				if (!fastSpeed)
                {
					fastSpeed = true;
                }
                else
                {
					if ((double)npc.ai[0] % 50 == 0)
                    {
						float speed = 20f;
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

				if ((double)npc.ai[0] % (Main.expertMode ? 100 : 150) == 0)
				{
					attackTimer++;
					if (attackTimer <= 2)
					{
					frame = 8;
					npc.velocity.X = 0f;
					Vector2 shootPos = npc.Center;
					float accuracy = 5f * (npc.life / npc.lifeMax);
					Vector2 shootVel = target - shootPos + new Vector2(Main.rand.NextFloat(-accuracy, accuracy), Main.rand.NextFloat(-accuracy, accuracy));
					shootVel.Normalize();
					shootVel *= 10.5f;
						for (int i = 0; i < (Main.expertMode ? 5 : 3); i++)
						{
							if (attackTimer == 104)
							{
								Projectile.NewProjectile(shootPos.X, shootPos.Y, shootVel.X, shootVel.Y, ModContent.ProjectileType<TimerPushBack>(), npc.damage / 3, 5f);
							}
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
					fastSpeed = false;
				}
			}
			if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
            {
				float speed = 1f;
				Vector2 moveto = Main.player[npc.target].Center + new Vector2(0f, -200f);
				Vector2 move = moveto - npc.Center;
				float magnitude = (move.X * move.X + move.Y * move.Y); //fun with the Pythagorean Theorem
				if (magnitude > speed)
				{
					move *= speed / magnitude;
				}
				float turnResistance = 10f; //the larger this is, the slower the npc will turn
				move = (npc.velocity * turnResistance + move) / (turnResistance + 1f);
				magnitude = (move.X * move.X + move.Y * move.Y);
				if (magnitude > speed)
				{
					move *= speed / magnitude;
				}
				npc.velocity = move;
			}

			#endregion
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