using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class Brilliance : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brilliance");
		}
		const int TileCollideDustType = 156;
		const int TileCollideDustCount = 4;
		const float TileCollideDustSpeedMulti = 0.2f;
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

			Dust.NewDust(projectile.position, projectile.width, projectile.height, TileCollideDustType, projectile.velocity.X * TileCollideDustSpeedMulti, projectile.velocity.Y * TileCollideDustSpeedMulti);
			return true;
		}
	}
}