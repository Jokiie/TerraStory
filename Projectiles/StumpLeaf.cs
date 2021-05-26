using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class StumpLeaf : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Stump's Leaf");
		}

		public override void SetDefaults()
		{
			projectile.magic = true;
			projectile.scale = 0.80f;
			projectile.width = 20;
			projectile.height = 15;
			projectile.aiStyle = 40;
			projectile.scale = 1.5f;
			projectile.friendly = true;
			projectile.timeLeft = 80;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.Leaf;
			projectile.penetrate = 1;
			projectile.timeLeft = 120;
			
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
				Main.PlaySound(SoundID.Grass, projectile.position);
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