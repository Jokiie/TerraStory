using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class RainProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("RainProj");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{

			projectile.hostile = true;
			projectile.width = 4;
			projectile.height = 40;
			projectile.aiStyle = 45;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.scale = 0.50f;
			projectile.timeLeft = 120;
			projectile.extraUpdates = 1;
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			if (++projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}
