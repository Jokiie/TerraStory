using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Projectiles
{
	public class CorruptedSwordP : ModProjectile
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("CorruptedSwordP");

		}
		public override void SetDefaults()
		{

			projectile.CloneDefaults(301);
			projectile.width = 32;
			projectile.height = 32;
			projectile.penetrate = 4;
			aiType = 301;
		}

		public override bool CanHitPlayer(Player target)
		{
			return false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockBack, bool crit)
		{
			target.AddBuff(mod.BuffType("PoisonedShuriken"), 100);
		}
		public override bool? CanHitNPC(NPC target)
		{
			return (target.friendly) ? false : true;
		}

		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + 1.57f;
		}
	}
}