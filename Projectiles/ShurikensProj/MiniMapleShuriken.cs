using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Dusts;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class MiniMapleShuriken : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
			DisplayName.SetDefault("Minis maple Shurikens");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 10;
			projectile.timeLeft = 120;
			projectile.aiStyle = 45;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
			projectile.scale = 0.5f;
			
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
