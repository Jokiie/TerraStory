using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles.Armor
{
    public class MiniMoonglow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mini Moonglow");
            Main.projFrames[projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 8;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.light = 0.1f;
            projectile.aiStyle = -1;
            projectile.penetrate = 1;
        }
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
		public override void AI()
		{

			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 50f)
			{
				// Fade out
				projectile.alpha += 2;
				if (projectile.alpha > 5)
				{
					projectile.alpha = 5;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 2;
				if (projectile.alpha < 2)
				{
					projectile.alpha = 2;
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
			if (projectile.ai[0] >= 400f)
			{
				projectile.Kill();
			}
			projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.velocity.Y > 30f)
			{
				projectile.velocity.Y = 30f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation += MathHelper.Pi;
			}
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 14);
			Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Sandstorm, projectile.direction, -1f, 0);
		}
	}
}