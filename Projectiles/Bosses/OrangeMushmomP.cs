using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerraStory.Projectiles.Bosses
{
	
	public class OrangeMushmomP : ModProjectile
	{
		
        public override void SetStaticDefaults()
		{
		    DisplayName.SetDefault("Orange Mushmom Spores!");
	    }
	
	    public override void SetDefaults()
	    {
		    projectile.width = 18;
		    projectile.height = 42;
		    projectile.aiStyle = 1;
		    aiType = ProjectileID.Bullet;
		    projectile.friendly = false;
		    projectile.ignoreWater = true;
		    projectile.penetrate = 1;
		    projectile.timeLeft = 500;
		    projectile.tileCollide = false;
		    projectile.hostile = true;
			projectile.scale = 1.2f;
		    ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
		    ProjectileID.Sets.TrailingMode[projectile.type] = 0;
     	}
	
	    public override Color? GetAlpha(Color lightColor)
	    {
		    return Color.White;
	    }
	
	    public override void AI()
	    {
		    projectile.velocity.Y += projectile.ai[0];
	    }
    }
}