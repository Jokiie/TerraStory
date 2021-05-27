using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class SubiP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Subi Throwing-Stars");
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
			}
			return false;
		}

		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			if (projectile.owner == Main.myPlayer)
			{
				// Drop a shuriken item, 1 in 18 chance (~5.5% chance)
				int item =
				Main.rand.NextBool(18)
					? Item.NewItem(projectile.getRect(), ModContent.ItemType<Subi>())
					: 0;

				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
			for (int i = 0; i < 20; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 11, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
				if (Main.rand.NextBool(3))
				{
					Main.dust[dust].fadeIn = 1.1f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].scale = 0.35f + Main.rand.Next(-10, 11) * 0.01f;
					Main.dust[dust].type++;
				}
				else
				{
					Main.dust[dust].scale = 0.50f + Main.rand.Next(-10, 11) * 0.01f;
				}
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 2.5f;
				Main.dust[dust].velocity -= projectile.oldVelocity / 10f;


			}
		}
	}
}