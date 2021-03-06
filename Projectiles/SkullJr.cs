using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class SkullJr : ModProjectile
	{
		public override void SetStaticDefaults() 
        {
			DisplayName.SetDefault("SkullJr");
		}
        const int TileCollideDustType = 109;

        const int TileCollideDustCount = 6;

        const float TileCollideDustSpeedMulti = 0.2f;

        public override void SetDefaults()
		{
			projectile.width = 27;
			projectile.height = 27;
			projectile.scale = 0.70f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.timeLeft = 100;
			projectile.alpha = 10;
			projectile.light = 0.1f;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.HarpyFeather;
			projectile.penetrate = 1;
		}

        public override void AI()
        {
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
            if (projectile.ai[1] >= 1f)
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
                int dust = Dust.NewDust(pos, w, h, DustID.Shadowflame, 0f, 0f, projectile.alpha, default(Color), 1f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < TileCollideDustCount; i++)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, TileCollideDustType, projectile.velocity.X * TileCollideDustSpeedMulti, projectile.velocity.Y * TileCollideDustSpeedMulti);
            return true;
        }
    }
}