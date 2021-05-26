using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Dusts;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Magic
{
	public class MiniBones : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 3;
			DisplayName.SetDefault("Minis dark stars");
		}

		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.penetrate = 2;
			projectile.timeLeft = 120;
			projectile.aiStyle = 45;
			aiType = ProjectileID.Bullet;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.ignoreWater = true;

		}
		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
			//target.AddBuff(mod.BuffType("PoisonedShuriken"), 100);
		}
		public override void AI()
		{
			//projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
			if (++projectile.frameCounter >= 3)
			{
				
				projectile.frameCounter = 0;
				if (++projectile.frame >= 3)
				{
					projectile.frame = 0;
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			//If collide with tile, reduce the penetrate.
			//So the projectile can reflect at most 5 times
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				SoundManager.PlaySound(Sounds.Drip, projectile.position);
			}
			return false;
		}
	}
}
