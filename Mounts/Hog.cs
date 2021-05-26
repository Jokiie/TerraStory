using TerraStory.Buffs;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using static Terraria.ModLoader.ModContent;


namespace TerraStory.Mounts
{
	public class Hog : ModMountData
	{

		public override void SetDefaults()
		{

			mountData.spawnDust = DustID.Smoke;
			mountData.buff = BuffType<HogMount>();
			mountData.heightBoost = 20;
			mountData.fallDamage = 0f;
			mountData.runSpeed = 9f;
			mountData.dashSpeed = 3f;
			mountData.flightTimeMax = 0;
			mountData.fatigueMax = 0;
			mountData.jumpHeight = 10;
			mountData.acceleration = 0.05f;
			mountData.jumpSpeed = 5f;
			mountData.blockExtraJumps = false;
			mountData.totalFrames = 13;
			mountData.constantJump = true;

			int[] array = new int[mountData.totalFrames];
			for (int i = 0; i < array.Length; i++)
			{
				if (i == 1)
				{
					array[i] = 21;
				}
				else if (i == 3 || i == 4 || i == 5)
				{
					array[i] = 20;
				}
				else
				{
					array[i] = 21;
				}
			}
			mountData.playerYOffsets = array;
			mountData.xOffset = 15;
			mountData.bodyFrame = 3;
			mountData.yOffset = +10;
			mountData.playerHeadOffset = 22;

			mountData.standingFrameCount = 3;
			mountData.standingFrameDelay = 12;
			mountData.standingFrameStart = 0;

			mountData.runningFrameCount = 6;
			mountData.runningFrameDelay = 12;
			mountData.runningFrameStart = 5;

			mountData.flyingFrameCount = 1;
			mountData.flyingFrameDelay = 0;
			mountData.flyingFrameStart = 0;

			mountData.inAirFrameCount = 1;
			mountData.inAirFrameDelay = 12;
			mountData.inAirFrameStart = 12;

			mountData.idleFrameCount = 1;
			mountData.idleFrameDelay = 12;
			mountData.idleFrameStart = 0;

			mountData.idleFrameLoop = true;

			mountData.swimFrameCount = mountData.inAirFrameCount;
			mountData.swimFrameDelay = mountData.inAirFrameDelay;
			mountData.swimFrameStart = mountData.inAirFrameStart;
			if (Main.netMode != NetmodeID.Server)
			{
				mountData.textureWidth = mountData.backTexture.Width;
				mountData.textureHeight = mountData.backTexture.Height;
			}
		}
		public override void UpdateEffects(Player player)
		{
			// Do not allow the mount to be ridden in water, honey or lava.
			if (player.wet || player.honeyWet || player.lavaWet)
			{
				player.mount.Dismount(player);
				return;
			}
		}
	}
}