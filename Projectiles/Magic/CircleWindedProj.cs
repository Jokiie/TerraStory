using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Projectiles.Magic
{
	public class CircleWindedProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Circle winded proj");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.magic = true;
			projectile.timeLeft = 60;
		}

		public override void AI()
		{
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}
			}
			// Or, more compactly:
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				projectile.frame = ++projectile.frame % Main.projFrames[projectile.type];
			}
			// Optional: if the projectile should fade in, fade it in:
			if (projectile.alpha > 0)
				projectile.alpha -= 15;
			if (projectile.alpha < 0)
				projectile.alpha = 0;
			// Set the rotation to face the current trajectory:
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			// Or, this version is easier to read:
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.PiOver2;
			// Cap downward velocity, in case you add gravity to this projectile
			if (projectile.velocity.Y > 16f)
				projectile.velocity.Y = 16f;

		}
	}
}