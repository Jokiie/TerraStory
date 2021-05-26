using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class MapleShurikenP : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Maple shuriken");
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Shuriken);
			aiType = ProjectileID.Shuriken;
			projectile.penetrate = 10;
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 14);
			ProjectileAnimations.Explode(projectile.whoAmI, 120, 120,
				delegate
				{
					for (int i = 0; i < 10; i++)
					{
						int num = Projectile.NewProjectile(projectile.position, projectile.velocity, ModContent.ProjectileType<MiniMapleShuriken>(), 10, 0, default, 2f);
						Main.projectile[num].position.X += Main.rand.Next(-50, 51) * .05f - 1.5f;
						Main.projectile[num].position.Y += Main.rand.Next(-50, 51) * .05f - 1.5f;

					}
				});
		}
	}
}