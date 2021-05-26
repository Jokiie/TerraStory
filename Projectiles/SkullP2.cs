using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class SkullP2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SkullP2");
		}
		public override void SetDefaults()
		{

			projectile.width = 18;
			projectile.height = 32;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = 6;
			projectile.aiStyle = 1;
			projectile.timeLeft = 600;
		}

		public override void Kill(int timeLeft)
		{
			for (int k = 0; k < 5; k++)
			{
				int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 0, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
			}
			Main.PlaySound(SoundID.Dig, (int)projectile.position.X, (int)projectile.position.Y, 0);
		}

	}
}