using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework.Graphics;

namespace TerraStory.Projectiles.Movement
{
	public class DoubleJump : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DoubleJump");
			Main.projFrames[projectile.type] = 5;
		}

		public override void SetDefaults()
		{
			projectile.width = 181;
			projectile.height = 119;
			projectile.damage = 2;
			projectile.penetrate = 8;
			projectile.alpha = 0;
			projectile.timeLeft = 20;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.hostile = false;
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
			if (projectile.ai[0] >= 120f)
			{
				projectile.Kill();
			}
			projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
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
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}
	}
}