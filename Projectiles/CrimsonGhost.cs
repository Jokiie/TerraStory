using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class CrimsonGhost : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Crimson ghost");
		}

		public override void SetDefaults()
		{
			projectile.width = 27;
			projectile.height = 27;
			projectile.scale = 0.90f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.timeLeft = 100;
			projectile.alpha = 10;
			projectile.light = 0.5f;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.HarpyFeather;
			projectile.penetrate = 3;
		}
		    //Additional Hooks/methods here.
		public override bool OnTileCollide(Vector2 oldVelocity) {
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			projectile.penetrate--;
			if (projectile.penetrate <= 0) {
				projectile.Kill();
			}
			else {
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				if (projectile.velocity.X != oldVelocity.X) {
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y) {
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			return false;
		}
	}
}