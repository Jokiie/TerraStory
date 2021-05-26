using TerraStory.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System;
using Terraria.Enums;
using System.IO;
using TerraStory.Dusts;
using TerraStory.NPCs.Bosses;

namespace TerraStory.Mounts
{
	public class SnailMorphDB : ModMountData
	{

		public override void SetDefaults()
		{
			mountData.spawnDust = DustType<TransparentDust>();
			mountData.buff = BuffType<SnailMorphDebuff>();
			mountData.heightBoost = 0;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 2f;
			mountData.dashSpeed = 0f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 20;
			mountData.acceleration = 0.5f;
			mountData.jumpSpeed = 10f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 6;
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

		public override void UseAbility(Player player, Vector2 mousePosition, bool toggleOn)
		{
			if (player.GetModPlayer<TerraStoryPlayer>().SnailMorphdebuff)
			{
				player.controlUseItem = false;
				player.controlHook = false;
			}
			if (player.GetModPlayer<TerraStoryPlayer>().Slow)
			{
				player.mount.Dismount(player);
            }
		}
	}
}