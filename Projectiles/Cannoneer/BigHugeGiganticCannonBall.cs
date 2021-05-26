using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Projectiles.Cannoneer
{
	public class BigHugeGiganticCannonBall : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault(" ");
			Main.projFrames[projectile.type] = 1;
			projectile.spriteDirection = projectile.direction;
		}

		public override void SetDefaults()
		{
			projectile.width = 150;
			projectile.height = 125;
			projectile.penetrate = -1;
			projectile.aiStyle = -1;
			projectile.tileCollide = false;
			projectile.friendly = true;
			projectile.ignoreWater = true;

		}
		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			// Vanilla explosions do less damage to Eater of Worlds in expert mode, so we will too.
			if (Main.expertMode)
			{
				if (target.type >= NPCID.EaterofWorldsHead && target.type <= NPCID.EaterofWorldsTail)
				{
					damage /= 5;
				}
			}
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			width = height = 125;
			return true;
		}

		public override void AI()
		{
			NormalAI();
			projectile.spriteDirection = projectile.direction;
			if (projectile.owner == Main.myPlayer && projectile.timeLeft <= 3)
			{
				projectile.tileCollide = false;
				// Set to transparent. This projectile technically lives as  transparent for about 30 frames
				projectile.alpha = 255;

				projectile.position = projectile.Center;

				projectile.width = 150;
				projectile.height = 125;
				projectile.Center = projectile.position;

				projectile.damage = 40;
				projectile.knockBack = 0f;
			}
			for (int i = 0; i < 5; i++)
			{
				Vector2 PPos = projectile.position;
				Vector2 boxCenter = projectile.Center - Vector2.Normalize(projectile.velocity)* 60;
				int PWidth= projectile.width;
				int PHeight = projectile.height;
				boxCenter -= new Vector2(projectile.width, projectile.height) / 4f;

				if (projectile.direction == -1)
				{
					int dustIndex = Dust.NewDust(new Vector2(boxCenter.X , boxCenter.Y), PWidth / 2, PHeight / 2, 31, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].fadeIn = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].noGravity = true;

					dustIndex = Dust.NewDust(new Vector2(boxCenter.X , boxCenter.Y), PWidth / 2, PHeight / 2, 6, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].noGravity = true;
				}
				else
				{
					int dustIndex = Dust.NewDust(new Vector2(boxCenter.X - 50, boxCenter.Y), PWidth / 2, PHeight / 2, 31, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].fadeIn = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].noGravity = true;

					dustIndex = Dust.NewDust(new Vector2(boxCenter.X - 50, boxCenter.Y), PWidth / 2, PHeight / 2, 6, 0f, 0f, 100, default(Color), 1f);
					Main.dust[dustIndex].scale = 0.5f + (float)Main.rand.Next(5) * 0.5f;
					Main.dust[dustIndex].noGravity = true;
				}
			}
		}

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			// Play explosion sound
			Main.PlaySound(SoundID.Item14, projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 30; i++) //50
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}

			// Fire Dust spawn
			for (int i = 0; i < 60; i++) //80
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}

			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f; // 1.5f
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}
		}

		public override void Kill(int timeLeft)
		{
			Vector2 usePos = projectile.position; // Position to use for dusts

			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
			usePos += rotVector * 16f;

			// Play explosion sound
			Main.PlaySound(SoundID.Item14, projectile.position);
			// Smoke Dust spawn
			for (int i = 0; i < 30; i++) //50
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 1.4f;
			}

			// Fire Dust spawn
			for (int i = 0; i < 60; i++) //80
			{
				int dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].velocity *= 5f;
				dustIndex = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
				Main.dust[dustIndex].velocity *= 3f;
			}

			// Large Smoke Gore spawn
			for (int g = 0; g < 2; g++)
			{
				int goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f; // 1.5f
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y + 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X + 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
				goreIndex = Gore.NewGore(new Vector2(projectile.position.X + (float)(projectile.width / 2) - 24f, projectile.position.Y + (float)(projectile.height / 2) - 24f), default(Vector2), Main.rand.Next(61, 64), 1f);
				Main.gore[goreIndex].scale = 0.8f;
				Main.gore[goreIndex].velocity.X = Main.gore[goreIndex].velocity.X - 1.5f;
				Main.gore[goreIndex].velocity.Y = Main.gore[goreIndex].velocity.Y - 1.5f;
			}

			// reset size to normal width and height.
			projectile.position.X = projectile.position.X + (float)(projectile.width);
			projectile.position.Y = projectile.position.Y + (float)(projectile.height);
			projectile.width = 150;
			projectile.height = 125;
			projectile.position.X = projectile.position.X - (float)(projectile.width);
			projectile.position.Y = projectile.position.Y - (float)(projectile.height);
		}

		private void NormalAI()
		{
			projectile.rotation =
			projectile.velocity.ToRotation() +
			MathHelper.ToRadians(90f);
		}
	}
}