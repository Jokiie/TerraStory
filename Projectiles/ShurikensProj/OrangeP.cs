using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class OrangeP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orange");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 1;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
		}

		public override void AI()
		{
			int frameSpeed = 5;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 4; // Loop through the 4 animations frames.
				projectile.frame++;
				if (projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			SoundManager.PlaySound(Sounds.PlayerKilled, (int)projectile.position.X, (int)projectile.position.Y, 14);
			for (int num158 = 0; num158 < 20; num158++)
			{
				int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 170, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[num159].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[num159].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[num159].type++;
				}
				else
				{
					Main.dust[num159].scale = 1.2f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[num159].noGravity = true;
				Main.dust[num159].velocity *= 2.5f;
				Main.dust[num159].velocity -= projectile.oldVelocity / 10f;


			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				SoundManager.PlaySound(Sounds.PlayerKilled, projectile.position);
				for (int num158 = 0; num158 < 20; num158++)
				{
					int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 170, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
					if (Main.rand.NextBool(3))
					{
						Main.dust[num159].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
						Main.dust[num159].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
						Main.dust[num159].type++;
					}
					else
					{
						Main.dust[num159].scale = 1.2f + Main.rand.Next(-10, 11) * 0.01f;
					}
					Main.dust[num159].noGravity = true;
					Main.dust[num159].velocity *= 2.5f;
					Main.dust[num159].velocity -= projectile.oldVelocity / 10f;
				}
			}
			return false;
		}
	}
}