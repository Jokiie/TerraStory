using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class BlueSnailShellP : ModProjectile
	{
		const int TileCollideDustType = 194;
		const int TileCollideDustCount = 7;
		const float TileCollideDustSpeedMulti = 0.2f;

		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = false;
		}

		public override void SetDefaults()
		{
			projectile.scale = 1f;
			projectile.width = 20;
			projectile.height = 20;
			projectile.alpha = 10;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.timeLeft = 60;
			projectile.penetrate = 1;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			for (int i = 0; i < TileCollideDustCount; i++)
				Dust.NewDust(projectile.position, projectile.width, projectile.height, TileCollideDustType, projectile.velocity.X * TileCollideDustSpeedMulti, projectile.velocity.Y * TileCollideDustSpeedMulti);
			return true;
		}
	}
}