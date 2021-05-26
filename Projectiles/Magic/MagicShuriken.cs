using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Dusts;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Magic
{
	public class MagicShuriken : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("MagicShuriken");
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.ignoreWater = true;
		}
		public override void AI()
		{
			Color color;
			color = Color.White;
			// Projectiles have an array of floats called AI that work like
			// timers. These timers all start at 0.
			// These timers can help determine a charge of movement.
			// another timer could be to play sounds.

			// this bit of code will run instantly due to the timer.
			// being set to 0 .
			if (projectile.ai[0] == 0f)
			{
				projectile.ai[0] = 1f;
				// We create a sound at the projectile's direction.
				Main.PlaySound(SoundID.Item8, projectile.position);
			}

			// We charge the rotation based on the projectile's direction.
			projectile.rotation += (float)projectile.direction + 0.95f;
			// We have to increment the timer as it isn't done automatically.
			projectile.ai[1] += 1f;
			// Between AI value of 50 and 100 . We are multiplying the velocity.
			if (projectile.ai[1] >= 10f)
			{
				if (projectile.ai[1] < 100f)
				{
					// We increase the X velocity speeding it up.
					projectile.velocity.X *= 1.1f;
				}
				else
				{
					// These keep the AI timer at 200 making it not usable anymore.
					projectile.ai[1] = 200f;
				}
			}

			// we can also create dust durint the AI animation.
			for (int i = 0; i < 2; i++)
			{
				// We get the projectile position,width and height
				// Which are passed into the NewDust method.
				Vector2 pos = projectile.position;
				int w = projectile.width;
				int h = projectile.height;
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<MiniMagicShurikenDust>(), projectile.velocity.X, projectile.velocity.Y, 0, color, 1f);
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Color color;
			color = Color.White;
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
				Dust.NewDust(projectile.position, projectile.width, projectile.height, ModContent.DustType<MiniMagicShurikenDust>(), projectile.velocity.X, projectile.velocity.Y, 0, color, 1f);
			}
			return false;
		}
		public override void Kill(int timeLeft)
		{
			SoundManager.PlaySound(Sounds.LegacySoundStyle_Item10, (int)projectile.position.X, (int)projectile.position.Y, 14);
			ProjectileAnimations.Explode(projectile.whoAmI, 120, 120,
				delegate
				{
					for (int i = 0; i < 20; i++)
					{
						int num = Projectile.NewProjectile(projectile.position, projectile.velocity, ProjectileType<MiniMagicShuriken>(), 4, 0, default, 2f);
						Main.projectile[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
						Main.projectile[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;

					}
				});
		}
	}
}