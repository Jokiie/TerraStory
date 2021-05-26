using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Dusts;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Magic
{
	public class MiniMagicShuriken : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
			DisplayName.SetDefault(" Minis magics Shurikens");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 2;
			projectile.timeLeft = 120;
			projectile.aiStyle = 45;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.ignoreWater = true;
			
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			if (++projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 5)
				{
					projectile.frame = 0;
				}
			}
		}
	}
}
