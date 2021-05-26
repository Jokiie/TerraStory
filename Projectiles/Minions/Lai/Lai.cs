using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static TerraStory.TerraStoryPlayer;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Minions.Lai
{
	public class Lai : ModProjectile
	{
		string phase = "";
		private bool fall;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lai");
			Main.projFrames[projectile.type] = 22;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			//ProjectileID.Sets.Homing[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.scale = 0.7f;
			projectile.width = 40;
			projectile.height = 65;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.sentry = true;
			projectile.netImportant = true;
			drawOffsetX = -35;
			drawOriginOffsetY = -50;

		}

		public override bool? CanCutTiles()
		{
			return true;
		}

		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
		{

			fallThrough = fall;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.CanBeChasedBy())
				{
					Vector2 toTarget = Main.npc[i].Center - projectile.Center;
					// Here we check if the NPC is below the minion and 300/16 = 18.25 tiles away horizontally
					if (toTarget.Y > 0 && Math.Abs(toTarget.X) < 300)
					{
						fall = true;
					}
					else
					{
						fall = false;
					}
				}
			}
			return base.TileCollideStyle(ref width, ref height, ref fallThrough);
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
		{
			if (oldVelocity.X != projectile.velocity.X)
				projectile.velocity.Y = -6.5f;
			return false;
		}
		public override bool CanDamage()
		{
			return true;
		}
		public override bool MinionContactDamage()
		{
			return true;
		}
		/// <summary>
		///(int)frame = the frame # in the spritesheet that this projectile will be drawn with.
		///Example: projectile has 4 frames, then frame can have values between 0 and 3
		///
		///(int)frameCounter = Used as a timer to decide when to change frame
		/// </summary>
		public override void AI()
		{

			Player player = Main.player[projectile.owner];
			int frameSpeed = 15;

			if (projectile.velocity.Y < 8)
				projectile.velocity.Y += 0.3f;

			if (projectile.velocity.Y > 8)
				projectile.velocity.Y -= 0.3f;

			#region Bro, Sautes!
			if (phase == "DansLesAirs")
			{
				projectile.spriteDirection = projectile.direction;

				// This is a simple "loop through all frames from top to bottom" animation
				projectile.frameCounter++;
				if (projectile.frameCounter >= frameSpeed * 5)
				{
					projectile.frameCounter = 9; // Loop through the 9 animations frames.
					projectile.frame++;
					if (projectile.frame >= 13)
					{
						projectile.frame = 5;
					}
				}
			}

			#endregion

			#region Deplacement
			if (phase == "Deplacement")
			{
				projectile.spriteDirection = projectile.direction;

				// This is a simple "loop through all frames from top to bottom" animation
				projectile.frameCounter++;
				if (projectile.frameCounter >= frameSpeed)
				{
					projectile.frameCounter = 4; // Loop through the 9 animations frames.
					projectile.frame++;
					if (projectile.frame >= 17)
					{
						projectile.frame = 14;

					}
				}
			}
			#endregion

			#region bro attack!
			if (phase == "BroAttack")
			{
				projectile.spriteDirection = projectile.direction;

				// This is a simple "loop through all frames from top to bottom" animation
				projectile.frameCounter++;
				if (projectile.frameCounter >= frameSpeed)
				{
					projectile.frameCounter = 4; // Loop through the 9 animations frames.
					projectile.frame++;
					if (projectile.frame >= 21)
					{
						projectile.frame = 18;

					}
				}
			}
			#endregion

			NPC target = projectile.OwnerMinionAttackTargetNPC;

			Vector2 targetPosition = projectile.position;

			float distanceFromTarget = 300f;

			bool foundTarget = false;

			if (target != null && target.CanBeChasedBy(this))
			{
				float LaiDistanceFromTarget = projectile.Distance(target.Center);

				if (LaiDistanceFromTarget < distanceFromTarget)
				{
					distanceFromTarget = LaiDistanceFromTarget;
					targetPosition = target.Center;
					foundTarget = true;
				}
			}
			if (!foundTarget)
			{
				for (int i = 0; i < 200; i++)
				{
					if (Main.npc[i].CanBeChasedBy(this))
					{
						float LaiDistanceFromTarget = Vector2.Distance(Main.npc[i].Center, projectile.Center);

						bool lineOfSight = Collision.CanHitLine(projectile.position, projectile.width, projectile.height, Main.npc[i].position, Main.npc[i].width, Main.npc[i].height);

						bool closest = Vector2.Distance(projectile.Center, targetPosition) > LaiDistanceFromTarget;

						bool inRange = LaiDistanceFromTarget < distanceFromTarget;

						bool closeThroughWall = LaiDistanceFromTarget < 100f;


						if (((closest && inRange) || !foundTarget) && (lineOfSight || closeThroughWall) )
						{
							distanceFromTarget = LaiDistanceFromTarget;
							target = Main.npc[i];
							targetPosition = target.Center;
							foundTarget = true;
						}
					}
				}

				float speed = 8f;
				float VitesseDeRetour = 8f;

				if (foundTarget)
				{
					if (distanceFromTarget < 300f)
					{
						phase = "BroAttack";
						Vector2 direction = targetPosition - projectile.Center + new Vector2(40 * projectile.direction, 0) / 2;
						direction.Normalize();
						direction *= speed;
						projectile.velocity = (projectile.velocity * (VitesseDeRetour - 1) + direction) / VitesseDeRetour;

						if (projectile.frame >= 18 && projectile.frame <= 21)
						{
							Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Bewildered"));

						}
					}
					else
					{
						phase = "Deplacement";
						if (projectile.frame >= 14 && projectile.frame <= 17)
						{
							if (projectile.position.X - target.position.X > 0)
							{
								projectile.velocity.X = -4;
							}
							else
							{
								projectile.velocity.X = 4;
							}

							if (distanceFromTarget < 10f && !Collision.CanHitLine(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
							{
								projectile.velocity.X *= 1;
								projectile.direction *= 1;
								projectile.velocity.Y *= 1;
								projectile.velocity.Y *= 2f;

							}
							if (projectile.velocity.Y != 0f)
							{
								phase = "DansLesAirs";
							
							}
						}
					}
				}
				else if (projectile.velocity.X == 0f)
				{
					phase = "Chilling";
					projectile.spriteDirection = projectile.direction;

					// This is a simple "loop through all frames from top to bottom" animation
					projectile.frameCounter++;
					if (projectile.frameCounter >= frameSpeed)
					{
						projectile.frameCounter = 5; // Loop through the 5 animations frames.
						projectile.frame++;
						if (projectile.frame >= 4)
						{
							projectile.frame = 0;

						}
					}
				}
			}
		}
		public override void ModifyDamageHitbox(ref Rectangle hitbox)
		{
			if (phase == "BroAttack")
			{
				projectile.spriteDirection = projectile.direction;
				if (projectile.frame >= 19 && projectile.frame <= 21)
				{
					if (projectile.direction == 1)
					hitbox.Inflate(20, 20);
					hitbox.Offset(-20, -15);
					hitbox.X += 20;
					if (projectile.direction == -1)
					{
						hitbox.Inflate(20, 20);
						hitbox.Offset(-15, -2);
						hitbox.X += 20;
					}
					projectile.spriteDirection = projectile.direction;
				}
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			projectile.spriteDirection = projectile.direction;
			if (projectile.frame >= 19 && projectile.frame <= 21)
			{
				Main.PlaySound(SoundLoader.customSoundType, (int)projectile.position.X, (int)projectile.position.Y, mod.GetSoundSlot(SoundType.Custom, "Sounds/Custom/Bewildered"));
			}
		}
	}
}