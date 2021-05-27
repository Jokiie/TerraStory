using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;

namespace TerraStory.Projectiles.Magic
{
	public class Brilliance : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brilliance");
		}

		public override void SetDefaults()
		{
			projectile.width = 27;
			projectile.height = 27;
			projectile.scale = 0.90f;
			projectile.aiStyle = 1;
			aiType = ProjectileID.HarpyFeather;
			projectile.friendly = true;
			projectile.timeLeft = 100;
			projectile.alpha = 25;
			projectile.light = 0.5f;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.penetrate = 1;
		}
		//Additional Hooks/methods here.
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundManager.PlaySound(Sounds.Splash, projectile.position);
			projectile.Kill();
			for (int i = 0; i < 30; i++)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, 102, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);

			return true;
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

			if (projectile.ai[0] >= projectile.timeLeft)
			{
				SoundManager.PlaySound(Sounds.Splash, projectile.position);
				projectile.Kill();
				for (int i = 0; i < 30; i++)
					Dust.NewDust(projectile.position, projectile.width, projectile.height, 102, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			}
			projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.velocity.Y > 30f)
			{
				projectile.velocity.Y = 30f;
			}
		}
	}
}