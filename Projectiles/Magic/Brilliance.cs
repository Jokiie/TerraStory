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
			projectile.scale = 0.80f;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
			projectile.alpha = 100;
			projectile.light = 0.5f;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			SoundManager.PlaySound(Sounds.LegacySoundStyle_Item9, projectile.position);
			projectile.Kill();
			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[dust].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].type++;
				}
				else
				{
					Main.dust[dust].scale = 0.50f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2.5f;
				Main.dust[dust].velocity -= projectile.oldVelocity / 10f;


			}

			return true;
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			SoundManager.PlaySound(Sounds.LegacySoundStyle_Item9, projectile.position);
			projectile.Kill();
			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 135, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[dust].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].type++;
				}
				else
				{
					Main.dust[dust].scale = 0.50f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2.5f;
				Main.dust[dust].velocity -= projectile.oldVelocity / 10f;


			}
		}
	}
}