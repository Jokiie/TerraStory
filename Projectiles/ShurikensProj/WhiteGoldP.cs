using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class WhiteGoldP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White gold");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 7;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
		}

		public override void PostAI()
		{
			int frameSpeed = 15;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 2; // Loop through the 4 animations frames.
				projectile.frame++;
				if (projectile.frame >= 2)
				{
					projectile.frame = 0;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int num158 = 0; num158 < 20; num158++)
			{
				int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 228, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
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
			if (projectile.owner == Main.myPlayer)
			{
				int item =
				Main.rand.NextBool(18)
					? Item.NewItem(projectile.getRect(), ModContent.ItemType<BalancedFury>())
					: 0;

				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
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
				SoundManager.PlaySound(Sounds.LegacySoundStyle_Item10, projectile.position);
				for (int num158 = 0; num158 < 20; num158++)
				{
					int num159 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 228, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
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
			return false;
		}
	}
}