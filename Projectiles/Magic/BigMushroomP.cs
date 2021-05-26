using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Dusts;

namespace TerraStory.Projectiles.Magic
{
	public class BigMushroomP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big mushroom dust");
		}

		public override void SetDefaults()
		{
			projectile.hostile = false;
			projectile.magic = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.alpha = 255;
			projectile.timeLeft = 120;
			
		}

		public override bool PreAI()
		{
			projectile.tileCollide = true;
			int dust0 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 152, 0f, 0f, 100);
			int dust1 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height,170, 0f, 0f, 100);
			int dust2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 152, 0f, 0f, 100);

			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}

			Vector2 move = Vector2.Zero;
			float distance = 400f;
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}

			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 11f;
				AdjustMagnitude(ref projectile.velocity);
			}

			return false;
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.Kill();
			Dust.NewDust(projectile.position + projectile.velocity * 0, projectile.width, projectile.height, 153, projectile.oldVelocity.X * 0, projectile.oldVelocity.Y * 0);
			return false;
		}

		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
				vector *= 6f / magnitude;
		}
	}
}