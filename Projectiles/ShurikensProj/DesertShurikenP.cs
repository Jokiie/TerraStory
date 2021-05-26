using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Dusts;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class DesertShurikenP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sand Shuriken");
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
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
		public override void Kill(int timeLeft)
		{
			SoundManager.PlaySound(Sounds.LegacySoundStyle_Item7, (int)projectile.position.X, (int)projectile.position.Y, 14);
			ProjectileAnimations.Explode(projectile.whoAmI, 120, 120,
				delegate
				{
					for (int i = 0; i < 5; i++)
					{
						int num = Projectile.NewProjectile(projectile.position, projectile.velocity, ProjectileType<MiniDesertShurikenP>(), 5, 0, default, 2f);
						Main.projectile[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
						Main.projectile[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;
						if (Main.projectile[num].position != projectile.Center)
						{
							Main.projectile[num].velocity = projectile.DirectionTo(Main.dust[num].position) * 6f;

						}
					}
				});
		}
	}
}