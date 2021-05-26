using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Projectiles.Ranger
{
	public class GoldenArrowP : ModProjectile
	{
		public override void SetDefaults()
		{

			projectile.width = 7;
			projectile.height = 38;
			projectile.scale = 0.80f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 3;
			projectile.ranged = true;
			projectile.ignoreWater = true;
			projectile.arrow = true;
			aiType = 1;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Golden Arrow");

		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void Kill(int timeLeft)
		{
			if (projectile.owner == Main.myPlayer)
			{
				int item = Main.rand.NextBool(5) ? Item.NewItem(projectile.getRect(), mod.ItemType("GoldenArrow")) : 0;
			}
			Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 64);
			for (int num158 = 0; num158 < 20; num158++)
			{
				int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 10, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[num159].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[num159].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[num159].type++;
				}
				else
				{
					Main.dust[num159].scale = 1.2f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[num159].noGravity = true;
				Main.dust[num159].velocity *= 2.5f;
				Main.dust[num159].velocity -= projectile.oldVelocity / 10f;
			}
		}
		public override void AI()
		{
			if (projectile.localAI[0] == 0f)
			{
				Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 20);
			}
			projectile.localAI[0] += 1f;
			if (projectile.localAI[0] > 3f)
			{
				int num90 = 1;
				if (projectile.localAI[0] > 5f)
				{
					num90 = 2;
				}
				for (int num91 = 0; num91 < num90; num91++)
				{
					int num92 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 10, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.1f, 100, default(Color), 0.4f);
					Main.dust[num92].noGravity = true;
					Dust expr_46AC_cp_0 = Main.dust[num92];
					expr_46AC_cp_0.velocity.X = expr_46AC_cp_0.velocity.X * 0.3f;
					Dust expr_46CA_cp_0 = Main.dust[num92];
					expr_46CA_cp_0.velocity.Y = expr_46CA_cp_0.velocity.Y * 0.3f;
					Main.dust[num92].noLight = true;
				}
			}
		}
	}
}