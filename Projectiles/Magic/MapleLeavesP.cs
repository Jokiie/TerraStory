using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TerraStory.Projectiles.Magic
{
	public class MapleLeavesP : ModProjectile
	{
		const int TileCollideDustType = 166;
		const int TileCollideDustCount = 6;
		const float TileCollideDustSpeedMulti = 0.2f;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple leaves");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 20;
			projectile.penetrate = 5;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.timeLeft = 600;
			projectile.tileCollide = true;
			projectile.ignoreWater = true;
			projectile.extraUpdates = 1;

		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(2) == 0)
			{
				target.StrikeNPC(projectile.damage / 2, 0f, 0, false);
			}
		}
		public override void AI()
		{
			projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= Main.projFrames[projectile.type])
				{
					projectile.frame = 0;
				}

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