using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class InfiniteThrowingKnifeP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Infinite Throwing Knife");
			Main.projFrames[projectile.type] = 2;
		}

		public override void SetDefaults()
		{
			projectile.width = 22;
			projectile.height = 44;
			projectile.penetrate = 6;
			projectile.aiStyle = -1;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
			
		}
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
		{
			// If attached to an NPC, draw behind tiles (and the npc) if that NPC is behind tiles, otherwise just behind the NPC.
			if (projectile.ai[0] == 1f) // or if(isStickingToTarget) since we made that helper method.
			{
				int npcIndex = (int)projectile.ai[1];
				if (npcIndex >= 0 && npcIndex < 200 && Main.npc[npcIndex].active)
				{
					if (Main.npc[npcIndex].behindTiles)
					{
						drawCacheProjsBehindNPCsAndTiles.Add(index);
					}
					else
					{
						drawCacheProjsBehindNPCs.Add(index);
					}

					return;
				}
			}
			// Since we aren't attached, add to this list
			drawCacheProjsBehindProjectiles.Add(index);
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{
			// For going through platforms and such, projectiles use a tad smaller size
			width = height = 10; // notice we set the width to the height, the height to 10. so both are 10
			return true;
		}

		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			// Inflate some target hitboxes if they are beyond 8,8 size
			if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
			{
				targetHitbox.Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
			}
			// Return if the hitboxes intersects, which means the projectile collides or not
			return projHitbox.Intersects(targetHitbox);
		}

		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item10, (int)projectile.position.X, (int)projectile.position.Y); // Play a death sound
			Vector2 usePos = projectile.position; // Position to use for dusts

			// Please note the usage of MathHelper, please use this!
			// We subtract 90 degrees as radians to the rotation vector to offset the sprite as its default rotation in the sprite isn't aligned properly.
			Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); // rotation vector to use for dust velocity
			usePos += rotVector * 16f;

			// Declaring a constant in-line is fine as it will be optimized by the compiler
			// It is however recommended to define it outside method scope if used elswhere as well
			// They are useful to make numbers that don't change more descriptive
			const int NUM_DUSTS = 20;

			// Spawn some dusts upon javelin death
			for (int i = 0; i < NUM_DUSTS; i++)
			{
				// Create a new dust
				Dust dust = Dust.NewDustDirect(usePos, projectile.width, projectile.height, 115);
				dust.position = (dust.position + projectile.Center) / 2f;
				dust.velocity += rotVector * 2f;
				dust.velocity *= 0.5f;
				dust.noGravity = true;
				usePos -= rotVector * 8f;
			}
			// Make sure to only spawn items if you are the projectile owner.
			// This is an important check as Kill() is called on clients, and you only want the item to drop once
			if (projectile.owner == Main.myPlayer)
			{
				// Drop a projectile item, 1 in 18 chance (~5.5% chance)
				int item =
				Main.rand.NextBool(18)
					? Item.NewItem(projectile.getRect(), ModContent.ItemType<InfiniteThrowingKnife>())
					: 0;

				// Sync the drop for multiplayer
				// Note the usage of Terraria.ID.MessageID, please use this!
				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
		}

		public int TargetWhoAmI
		{
			get => (int)projectile.ai[1];
			set => projectile.ai[1] = value;
		}

		public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
			target.AddBuff(ModContent.BuffType<DeepCut>(), 1200);
		}

		private const int MAX_TICKS = 90;

		private const int ALPHA_REDUCTION = 25;

		public override void AI()
		{
			int frameSpeed = 5;
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
			UpdateAlpha();
			NormalAI();
		}

		private void UpdateAlpha()
		{
			// Slowly remove alpha as it is present
			if (projectile.alpha > 0)
			{
				projectile.alpha -= ALPHA_REDUCTION;
			}

			// If alpha gets lower than 0, set it to 0
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
		}

		private void NormalAI()
		{
			TargetWhoAmI++;

			if (TargetWhoAmI >= MAX_TICKS)
			{
				const float velXmult = 0.98f;
				const float velYmult = 0.35f;
				TargetWhoAmI = MAX_TICKS;
				projectile.velocity.X *= velXmult;
				projectile.velocity.Y += velYmult;
			}

			// Make sure to set the rotation accordingly to the velocity, and add some to work around the sprite's rotation
			// Please notice the MathHelper usage, offset the rotation by 90 degrees (to radians because rotation uses radians) because the sprite's rotation is not aligned!
			projectile.rotation =
				projectile.velocity.ToRotation() +
				MathHelper.ToRadians(90f);
		}
	}
}