using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace TerraStory.Projectiles.Magic
{
	public class Bubbles : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubbles");
		}
		public override void SetDefaults()
		{
			projectile.hostile = false;
			projectile.magic = true;
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = -1;
			projectile.friendly = false;
			projectile.penetrate = 1;
			projectile.tileCollide = false;
			projectile.alpha = 255;
			projectile.timeLeft = 999999;
		}

		int counter = 7;
		bool firing = false;
		Vector2 direction = Vector2.Zero;
		public override bool PreAI()
		{
			Player player = Main.player[projectile.owner];
			player.heldProj = projectile.whoAmI;
			if (player.statMana <= 0)
			{
				projectile.Kill();
			}
			player.itemTime = 5;
			player.itemAnimation = 5;
			player.velocity.X *= 0.97f;
			if (counter == 7)
			{
				direction = Main.MouseWorld - (player.Center - new Vector2(4, 4));
				direction.Normalize();
				direction *= 7f;
				if (player.statMana > 0)
				{
					player.statMana -= 20;
					player.manaRegenDelay = 60;
				}
				else
				{
					firing = true;
				}
			}
			if (player.channel && !firing)
			{
				projectile.position = player.Center;
				if (counter < 100)
				{
					counter++;
					if (counter % 20 == 19)
					{
						Main.PlaySound(SoundID.MaxMana, (int)projectile.position.X, (int)projectile.position.Y);
					}
					projectile.ai[1]++;
					if (projectile.ai[1] % 20 == 19)
					{
						if (player.statMana > 0)
						{
							player.statMana -= 20;
							player.manaRegenDelay = 90;
						}
						else
						{
							firing = true;
						}
					}
				}
				if (counter == 100)
				{
					counter = 130;
				}
			}
			else
			{
				firing = true;
				if (counter > 0)
				{
					counter -= 2;
					if (counter % 5 == 0)
					{
						int bubbleproj;
						bubbleproj = Main.rand.Next(new int[] {
							ModContent.ProjectileType<BubbleFish>(),
							ModContent.ProjectileType<BubbleFish>(),
							ModContent.ProjectileType<BubbleFish>(),
							ModContent.ProjectileType<BubbleFish>(),
							ModContent.ProjectileType<BubbleFish>()
						});
						Main.PlaySound(SoundID.Item, (int)projectile.position.X, (int)projectile.position.Y, 85);
						Projectile.NewProjectile(player.Center + (direction * 5), direction.RotatedBy(Main.rand.NextFloat(-0.3f, 0.3f)) * Main.rand.NextFloat(0.85f, 1.15f), bubbleproj, projectile.damage, projectile.knockBack, projectile.owner);
						projectile.spriteDirection = projectile.direction;
					}
				}
				else
				{
					projectile.active = false;
				}
			}
			return true;
		}
	}
}