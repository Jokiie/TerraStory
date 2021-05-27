using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Weapons.Ranger;

namespace TerraStory.Projectiles.Ranger
{
	public class CopperArrowP : ModProjectile
	{
		public override void SetDefaults()
		{

			projectile.width = 7;
			projectile.height = 38;
			projectile.scale = 0.80f;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 1;
			projectile.ranged = true;
			projectile.ignoreWater = true;
			projectile.arrow = true;
			aiType = 1;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Arrow");

		}

		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			if (projectile.owner == Main.myPlayer)
			{
				// Drop the related item, 1 in 18 chance (~5.5% chance)
				int item =
				Main.rand.NextBool(18)
					? Item.NewItem(projectile.getRect(), ModContent.ItemType<CopperArrow>())
					: 0;

				// Sync the drop for multiplayer
				// Note the usage of Terraria.ID.MessageID, please use this!
				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 9, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[dust].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].type++;
				}
				else
				{
					Main.dust[dust].scale = 1.2f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2.5f;
				Main.dust[dust].velocity -= projectile.oldVelocity / 10f;


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
					int num92 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height, 9, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.1f, 100, default(Color), 0.4f);
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