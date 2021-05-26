using TerraStory.Dusts;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles
{
	//ported from my tAPI mod because I'm lazy
	public class Rock : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			ProjectileID.Sets.Homing[projectile.type] = false;
		}

		public override void SetDefaults()
		{
			projectile.aiStyle = 1;
			projectile.scale = 0.60f;
			projectile.width = 20;
			projectile.height = 20;
			projectile.alpha = 10;
			projectile.friendly = true;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.ranged = true;
			aiType = ProjectileID.Bullet;
			projectile.timeLeft = 600;
			projectile.penetrate = 3;

		}
	}
}