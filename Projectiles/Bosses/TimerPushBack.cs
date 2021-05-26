using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerraStory.Projectiles.Bosses
{

	public class TimerPushBack : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Timer's special ability");
		}

		public override void SetDefaults()
		{
			projectile.width = 18;
			projectile.height = 42;
			projectile.scale = 1.2f;
			projectile.aiStyle = 1;
			projectile.penetrate = 3;
			projectile.timeLeft = 500;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;
		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void OnHitPlayer(Player target, int damage, bool crit)
		{

			Vector2 distanceBetweenTarget = target.Center - Main.player[projectile.owner].Center;
			target.velocity.X = 0 - target.velocity.X + 20;
			target.velocity.Y = 0 - target.velocity.Y + 20;
			for (int i = 0; i < 5; i++) //50
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}
			for (int g = 0; g < 1; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f; // 1.5f
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 0.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 0.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 0.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 0.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 0.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 0.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 0.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 0.5f;
			}
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