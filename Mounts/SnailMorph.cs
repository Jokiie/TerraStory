using TerraStory.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using Terraria.Enums;
using System.IO;

namespace TerraStory.Mounts
{
	public class SnailMorph : ModMountData
	{

		public override void SetDefaults()
		{
			mountData.spawnDust = DustID.Smoke;
			mountData.buff = BuffType<SnailMorphBuff>();
			mountData.heightBoost = 0;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 3f;
			mountData.dashSpeed = 0f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 15;
			mountData.acceleration = 0.5f;
			mountData.jumpSpeed = 0f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 6;
			mountData.abilityDuration = 1800;
			mountData.abilityCooldown = 0;
			mountData.constantJump = false;
			int[] array = new int[mountData.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				if (i == 1)
				{
					array[i] = 0;
				}
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = +5;
			mountData.bodyFrame = -1;
			mountData.yOffset = +10;
			mountData.playerHeadOffset = 10;

			mountData.standingFrameCount = 1;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;

			mountData.runningFrameCount = 5;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 1;

			mountData.flyingFrameCount = 0;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;

			mountData.inAirFrameCount = 0;
			mountData.inAirFrameDelay = 0;
			mountData.inAirFrameStart = 0;

			mountData.idleFrameCount = 5;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 1;

			mountData.idleFrameLoop = true;

			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != NetmodeID.Server)
			{
				mountData.textureWidth = mountData.frontTexture.Width;
				mountData.textureHeight = mountData.frontTexture.Height;
			}
		}
		/// <summary>
		/// Allows you to make things happen when mount is used (creating dust etc.) Can also be used for mount special abilities.
		/// </summary>
		/// <param name="player"></param>
		public override void UpdateEffects(Player player)
		{
			Main.armorHide = true;
			// Do not allow the mount to be ridden in water, honey or lava.
			if (player.wet || player.honeyWet || player.lavaWet)
			{
				player.mount.Dismount(player);
				return;
			}
			if (player.controlUseItem)
			{
				player.mount.Dismount(player);
				return;
			}

		}

		public bool collideX;

		public bool collideY;

		public int spriteDirection = -1;

		public bool noGravity = false;

		//public float rotation;

		public int directionY = 1;

		public float joueur0;
		public float joueur1;
		public float joueur2;
		public float scale2;
		public float joueur33;
		public float joueur11;
		public float joueur22;
		public float scale = 0f;

		public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
		{
			int playerTileX = (int)(player.position.X + (float)(player.width / 2)) / 16;
			int playerTileY = (int)(player.position.Y + (float)(player.height / 2)) / 16;
			Tile tile = Main.tile[playerTileX, playerTileY];
			if (Main.tile[playerTileX, playerTileY] != null && Main.tile[playerTileX, playerTileY].bottomSlope() && Main.tile[playerTileX, playerTileY].halfBrick() && Main.tile[playerTileX, playerTileY].type == TileID.Platforms)
			{
				player.direction += -1;
				player.fullRotation -= 3.14f;
			}
			if (Main.tile[playerTileX, playerTileY] != null && Main.tile[playerTileX, playerTileY].topSlope() && collideY)
			{
				player.fullRotation += 3.14f;
			}
			NPC npc = Main.npc[Main.myPlayer];
			if (scale2 != 0f)
			{

				scale = scale2;
				int snailWidth = (int)(35f * scale);
				int snailHeight = (int)(35f * scale);
				if (snailWidth != player.width)
				{
					player.position.X = player.position.X + (float)(player.width / 2) - (float)snailWidth - 2f;
					player.width = snailWidth;
				}
				if (snailHeight != player.height)
				{
					player.position.Y = player.position.Y + (float)player.height - (float)snailHeight;
					player.height = snailHeight;
				}
			}
			if (scale2 == 0f)
			{
				scale2 = (float)Main.rand.Next(80, 111) * 0.01f;
				npc.netUpdate = true;
			}
			float ScaleRange = 0.3f;
			{
				ScaleRange = 0.6f;
			}
			if (joueur0 == 0f)
			{
				npc.netUpdate = true;
				npc.TargetClosest(true);
				directionY = 1;
				joueur0 = 1f;
				if (player.direction > 0)
				{
					spriteDirection = 1;

				}
			}
			bool CollerSurUnMur = false;
			if (joueur2 == 0f && Main.rand.Next(7200) == 0)
			{
				joueur2 = 2f;
			}
			if (!collideX && !collideY)
			{
				joueur33 += 1f;
				if (joueur33 > 5f)
				{
					joueur2 = 2f;
					npc.netUpdate = true;
				}
			}
			else
			{
				joueur33 = 0f;
			}

			if (joueur2 > 0f)
			{
				joueur1 = 0f;
				joueur0 = 1f;
				directionY = 1;
				if (player.velocity.Y > ScaleRange)
				{

					player.fullRotation += (float)player.direction * 0.1f;
				}
				else
				{
					player.fullRotation = 0f;
				}
				spriteDirection = player.direction;
				player.velocity.X = ScaleRange * (float)player.direction;
				noGravity = false;
				int x = (int)(player.Center.X + (float)(player.width / 2 * -player.direction)) / 16;
				int y = (int)(player.position.Y + (float)player.height + 8f) / 16;
				if (Main.tile[x, y] != null && !Main.tile[x, y].topSlope() && collideY)
				{
					joueur2 -= 1f;
				}
				y = (int)(player.position.Y + (float)player.height - 4f) / 16;
				x = (int)(player.Center.X + (float)(player.width / 2 * player.direction)) / 16;
				if (Main.tile[x, y] != null && Main.tile[x, y].bottomSlope())
				{
					player.direction += -1;
				}
				if (collideX && player.velocity.Y == 0f)
				{
					CollerSurUnMur = true;
					joueur2 = 0f;
					directionY = -1;
					joueur1 = 1f;
				}
				if (player.velocity.Y == 0f)
				{
					if (joueur11 == player.position.X)
					{
						joueur22 += 1f;
						if (joueur22 > 10f)
						{
							player.direction = 1;
							player.velocity.X = (float)player.direction * ScaleRange;
							joueur22 = 0f;
						}
					}
					else
					{
						joueur22 = 0f;
						joueur11 = player.position.X;
					}
				}
			}
			if (joueur2 != 0f)
			{
				return;
			}
			noGravity = true;
			if (joueur1 == 0f)
			{
				if (collideY)
				{
					joueur0 = 2f;
				}
				if (!collideY && joueur0 == 2f)
				{
					player.direction = -player.direction;
					joueur1 = 1f;
					joueur0 = 1f;
				}
				if (collideX)
				{
					directionY = -directionY;
					joueur1 = 1f;
				}
			}
			else
			{
				if (collideX)
				{
					joueur0 = 2f;
				}
				if (!collideX && joueur0 == 2f)
				{
					directionY = -directionY;
					joueur1 = 0f;
					joueur0 = 1f;
				}
				if (collideY)
				{
					player.direction = -player.direction;
					joueur1 = 0f;
				}
			}
			// Si tombe
			if (!CollerSurUnMur)
			{
				float num21 = player.fullRotation;
				if (directionY < 0)
				{
					if (player.direction < 0)
					{
						if (collideX)
						{
							player.fullRotation = 1.57f;
							spriteDirection = -1;
						}
						else if (collideY)
						{
							player.fullRotation = 3.14f;
							spriteDirection = 1;
						}
					}
					else if (collideY)
					{
						player.fullRotation = 3.14f;
						spriteDirection = -1;
					}
					else if (collideX)
					{
						player.fullRotation = 4.71f;
						spriteDirection = 1;
					}
				}
				else if (directionY < 0)
				{
					if (collideY)
					{
						player.fullRotation = 0f;
						spriteDirection = -1;
					}
				}
				else if (collideX)
				{
					player.fullRotation = 4.71f;
					spriteDirection = -1;
				}
				else if (collideY)
				{
					player.fullRotation = 0f;
					spriteDirection = 1;
				}
				float num22 = player.fullRotation;
				player.fullRotation = num21;
				if ((double)player.fullRotation > 6.28)
				{
					player.fullRotation -= 6.28f;
				}
				if (player.fullRotation < 0f)
				{
					player.fullRotation += 6.28f;
				}
				float num23 = Math.Abs(player.fullRotation - num22);
				float num24 = 0.1f;
				if (player.fullRotation > num22)
				{
					if ((double)num23 > 3.14)
					{
						player.fullRotation += num24;
					}
					else
					{
						player.fullRotation -= num24;
						if (player.fullRotation < num22)
						{
							player.fullRotation = num22;
						}
					}
				}
				if (player.fullRotation < num22)
				{
					if ((double)num23 > 3.14)
					{
						player.fullRotation -= num24;
					}
					else
					{
						player.fullRotation += num24;
						if (player.fullRotation > num22)
						{
							player.fullRotation = num22;
						}
					}
				}
				if (directionY == -1 && (double)player.velocity.Y > -1.5)
				{
					player.velocity.Y -= 0.04f;
					if ((double)player.velocity.Y > 1.5)
					{
						player.velocity.Y -= 0.05f;
					}
					else if (player.velocity.Y > 0f)
					{
						player.velocity.Y += 0.03f;
					}
					if ((double)player.velocity.Y < -1.5)
					{
						player.velocity.Y = -1.5f;
					}
				}
				else if (directionY == 1 && (double)player.velocity.Y < 1.5)
				{
					player.velocity.Y += 0.04f;
					if ((double)player.velocity.Y < -1.5)
					{
						player.velocity.Y += 0.05f;
					}
					else if (player.velocity.Y < 0f)
					{
						player.velocity.Y -= 0.03f;
					}
					if ((double)player.velocity.Y > 1.5)
					{
						player.velocity.Y = 1.5f;
					}
				}
			}

			player.velocity.X = ScaleRange * (float)player.direction;
			player.velocity.Y = ScaleRange * (float)directionY;
		}
	}
}