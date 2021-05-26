using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.NPCs;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles
{

	internal class RollingBee : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults()
		{
			projectile.width = 28;
			projectile.height = 30;
			projectile.scale = 1f;
			projectile.hostile = true;
			projectile.magic = true;
			projectile.melee = false;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.alpha = 0;
			projectile.penetrate = 1;
		}

		public override void AI()
		{

			projectile.ai[0] += 1f;
			if (projectile.ai[0] > 50f)
			{
				// Fade out
				projectile.alpha += 2;
				if (projectile.alpha > 5)
				{
					projectile.alpha = 5;
				}
			}
			else
			{
				// Fade in
				projectile.alpha -= 2;
				if (projectile.alpha < 2)
				{
					projectile.alpha = 2;
				}
			}
			// Slow down
			projectile.velocity *= 0.98f;
			// Loop through the 4 animation frames, spending 5 ticks on each.
			if (++projectile.frameCounter >= 5)
			{
				projectile.frameCounter = 0;
				if (++projectile.frame >= 4)
				{
					projectile.frame = 0;
				}
			}
			// Kill this projectile after 1 second
			if (projectile.ai[0] >= 400f)
			{
				projectile.Kill();
			}
			projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0f ? 1 : -1;
			projectile.rotation = projectile.velocity.ToRotation();
			if (projectile.velocity.Y > 30f)
			{
				projectile.velocity.Y = 30f;
			}
			// Since our sprite has an orientation, we need to adjust rotation to compensate for the draw flipping.
			if (projectile.spriteDirection == -1)
			{
				projectile.rotation += MathHelper.Pi;
			}
		}

		// Some advanced drawing because the texture image isn't centered or symetrical.
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
			}
			Texture2D texture = Main.projectileTexture[projectile.type];
			int frameHeight = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int startY = frameHeight * projectile.frame;
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);
			Vector2 origin = sourceRectangle.Size() / 2f;
			origin.X = (float)(projectile.spriteDirection == 1 ? sourceRectangle.Width - 20 : 20);

			Color drawColor = projectile.GetAlpha(lightColor);
			Main.spriteBatch.Draw(texture,
				projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY),
				sourceRectangle, drawColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);

			return false;
		}

		public override void Kill(int timeLeft)
		{
			if (Collision.CanHit(projectile.position, 1, 1, projectile.position, projectile.width, projectile.height))
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, projectile.direction, -1f, 0);
				Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/HornetAtt"));
				int ChooseRollingChild = NPCType<NormalBee>();
				int ChoosenRollingChild = NPC.NewNPC((int)projectile.position.X, (int)projectile.position.Y, ChooseRollingChild);
				Main.projectile[ChoosenRollingChild].velocity.X = (float)Main.rand.Next(-200, 201) * 0.010f;
				Main.projectile[ChoosenRollingChild].velocity.Y = (float)Main.rand.Next(-200, 201) * 0.010f;
				Main.projectile[ChoosenRollingChild].localAI[0] = 60f;
				Main.projectile[ChoosenRollingChild].netUpdate = true;
			}
		}
	}
}
