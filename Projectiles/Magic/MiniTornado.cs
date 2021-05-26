using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;

namespace TerraStory.Projectiles.Magic
{
    public class MiniTornado : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 9;
			DisplayName.SetDefault("Minis pink stars");
		}

		public override void SetDefaults()
		{
			projectile.scale = 0.50f;
			projectile.width = 125;
			projectile.height = 125;
			projectile.penetrate = 2;
			projectile.aiStyle = -1;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.alpha = 255;
			projectile.ignoreWater = true;

		}
		public override Color? GetAlpha(Color lightColor)
		{
			//return Color.White;
			return new Color(255, 255, 255, 0) * (1f - (float)projectile.alpha / 255f);
		}
		public override void AI()
		{
			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 50f)
			{
				// Fade out
				projectile.alpha += 25;
				if (projectile.alpha > 255)
				{
					projectile.alpha = 255;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 25;
				if (projectile.alpha < 100)
				{
					projectile.alpha = 100;
				}
			}
			// Slow down
			projectile.velocity *= 0.98f;
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			// Kill this projectile after 1 second
			if (projectile.ai[0] >= 60f)
			{
				projectile.Kill();
			}
			projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.velocity.Y > 16f)
			{
				projectile.velocity.Y = 16f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation += MathHelper.Pi;
			}
		}
	}
}