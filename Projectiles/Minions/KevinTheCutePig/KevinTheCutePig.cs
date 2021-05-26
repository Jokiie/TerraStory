using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Buffs;
using System;
using Microsoft.Xna.Framework;

namespace TerraStory.Projectiles.Minions.KevinTheCutePig
{

    public class KevinTheCutePig : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kevin");
            Main.projFrames[projectile.type] = 15;
            projectile.spriteDirection = projectile.direction;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {

            projectile.scale = 1f;
            projectile.width = 34;
            projectile.height = 28;
            projectile.alpha = 0;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.melee = false;
            //projectile.ignoreWater = false;
            projectile.minionSlots = 1f;
            projectile.penetrate = -1;
            projectile.netImportant = true;

        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            #region Active Check
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<KevinTheCutePigBuff>());
            }
            if (player.HasBuff(BuffType<KevinTheCutePigBuff>()))
            {
                projectile.timeLeft = 2;
            }
            TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
            if (player.dead)
            {
                modPlayer.KevinTheCutePig = false;
            }
            if (modPlayer.KevinTheCutePig)
            {
                projectile.timeLeft = 2;
				#endregion

				int frameSpeed = 15;
				Vector2 vector63 = player.Center;
				vector63.X -= (15 + player.width / 2) * player.direction;
				vector63.X -= projectile.minionPos * 40 * player.direction;
				int MinionTarget = -1;
				float MaxRange = 500f; // 800f
				bool flag20 = true;
				int num833 = 15;
				if (projectile.ai[0] == 0f && flag20)
				{
					NPC ownerMinionAttackTargetNPC4 = projectile.OwnerMinionAttackTargetNPC;
					if (ownerMinionAttackTargetNPC4 != null && ownerMinionAttackTargetNPC4.CanBeChasedBy(this))
					{
						float DistFromTarget = (ownerMinionAttackTargetNPC4.Center - projectile.Center).Length();
						if (DistFromTarget < MaxRange)
						{
							MinionTarget = ownerMinionAttackTargetNPC4.whoAmI;
							MaxRange = DistFromTarget;
						}
					}
					if (MinionTarget < 0)
					{
						for (int i = 0; i < 200; i++)
						{
							NPC TargetFound = Main.npc[i];
							if (TargetFound.CanBeChasedBy(this))
							{
								float DistFromTarget = (TargetFound.Center - projectile.Center).Length();
								if (DistFromTarget < MaxRange)
								{
									MinionTarget = i;
									MaxRange = DistFromTarget;
								}
							}
						}
					}
				}
				if (projectile.ai[0] == 1f)
				{
					projectile.tileCollide = false;
					float MinSpeedX = 0.2f;
					float MaxSpeedX = 10f;
					int num839 = 200;
					if (MaxSpeedX < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
					{
						MaxSpeedX = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
					}
					Vector2 vector67 = player.Center - projectile.Center;
					float Distance = vector67.Length();
					if (Distance > 2000f)
					{
						projectile.position = player.Center - new Vector2(projectile.width, projectile.height) / 2f;
					}
					if (Distance < (float)num839 && player.velocity.Y == 0f && projectile.position.Y + (float)projectile.height <= player.position.Y + (float)player.height && !Collision.SolidCollision(projectile.position, projectile.width, projectile.height))
					{
						projectile.ai[0] = 0f;
						projectile.netUpdate = true;
						if (projectile.velocity.Y < -6f)
						{
							projectile.velocity.Y = -6f;
						}
					}
					if (!(Distance < 60f))
					{
						vector67.Normalize();
						vector67 *= MaxSpeedX;
						if (projectile.velocity.X < vector67.X)
						{
							projectile.velocity.X += MinSpeedX;
							if (projectile.velocity.X < 0f)
							{
								projectile.velocity.X += MinSpeedX * 1.5f;
							}
						}
						if (projectile.velocity.X > vector67.X)
						{
							projectile.velocity.X -= MinSpeedX;
							if (projectile.velocity.X > 0f)
							{
								projectile.velocity.X -= MinSpeedX * 1.5f;
							}
						}
						if (projectile.velocity.Y < vector67.Y)
						{
							projectile.velocity.Y += MinSpeedX;
							if (projectile.velocity.Y < 0f)
							{
								projectile.velocity.Y += MinSpeedX * 1.5f;
							}
						}
						if (projectile.velocity.Y > vector67.Y)
						{
							projectile.velocity.Y -= MinSpeedX;
							if (projectile.velocity.Y > 0f)
							{
								projectile.velocity.Y -= MinSpeedX * 1.5f;
							}
						}
					}
					if (projectile.velocity.X != 0f)
					{
						projectile.spriteDirection = Math.Sign(projectile.velocity.X);
					}
					projectile.frameCounter++;
					if (projectile.frameCounter > 3)
					{
						projectile.frame++;
						projectile.frameCounter = 0;
					}
					if ((projectile.frame < 10) | (projectile.frame > 13))
					{
						projectile.frame = 10;
					}
					projectile.rotation = projectile.velocity.X * 0.1f;
				}

				if (projectile.ai[0] == 2f)
				{
					projectile.friendly = true;
					projectile.spriteDirection = projectile.direction;
					projectile.rotation = 0f;

					// ici frame a changer au besoin
					projectile.frame = 4 + (int)((float)num833 - projectile.ai[1]) / (num833 / 3);
					if (projectile.velocity.Y != 0f)
					{
						projectile.frame += 3;
					}

					// sert a empecher de sauter pour se deplacer?
					projectile.velocity.Y += 0.4f; // 0.4f
					if (projectile.velocity.Y > 10f)
					{
						projectile.velocity.Y = 10f;
					}
					projectile.ai[1]--;
					if (projectile.ai[1] <= 0f)
					{
						projectile.ai[1] = 0f;
						projectile.ai[0] = 0f;
						projectile.friendly = false;
						projectile.netUpdate = true;
						return;
					}
				}
				if (MinionTarget >= 0)
				{
					float num842 = 700f; // 400f
					float num843 = 20f;  // 20f
					if ((double)projectile.position.Y > Main.worldSurface * 16.0)
					{
						num842 *= 0.7f;
					}
					NPC nPC15 = Main.npc[MinionTarget];
					Vector2 center9 = nPC15.Center;
					float num844 = (center9 - projectile.Center).Length();
					bool flag21 = Collision.CanHit(projectile.position, projectile.width, projectile.height, nPC15.position, nPC15.width, nPC15.height);
					if (num844 < num842)
					{
						vector63 = center9;
						if (center9.Y < projectile.Center.Y - 30f && projectile.velocity.Y == 0f)
						{
							float num845 = Math.Abs(center9.Y - projectile.Center.Y);
							if (num845 < 120f)
							{
								projectile.velocity.Y = -10f;
							}


							else if (num845 < 210f)
							{
								projectile.velocity.Y = -13f;
							}
							else if (num845 < 270f)
							{
								projectile.velocity.Y = -15f;
							}
							else if (num845 < 310f)
							{
								projectile.velocity.Y = -17f;
							}
							else if (num845 < 380f)
							{
								projectile.velocity.Y = -18f;
							}
						}
					}
					if (num844 < num843)
					{
						projectile.ai[0] = 2f;
						projectile.ai[1] = num833;
						projectile.netUpdate = true;
					}
				}
				if (projectile.ai[0] == 0f && MinionTarget < 0)
				{
					float DistanceFromPlayerY = 500f;
					if (Main.player[projectile.owner].rocketDelay2 > 0)
					{
						projectile.ai[0] = 1f;
						projectile.netUpdate = true;
					}
					Vector2 DistanceFromPlayer = player.Center - projectile.Center;
					if (DistanceFromPlayer.Length() > 2000f)
					{
						projectile.position = player.Center - new Vector2(projectile.width, projectile.height) / 2f;
					}
					else if (DistanceFromPlayer.Length() > DistanceFromPlayerY || Math.Abs(DistanceFromPlayer.Y) > 300f)
					{
						projectile.ai[0] = 1f;
						projectile.netUpdate = true;
						if (projectile.velocity.Y > 0f && DistanceFromPlayer.Y < 0f)
						{
							projectile.velocity.Y = 0f;
						}
						if (projectile.velocity.Y < 0f && DistanceFromPlayer.Y > 0f)
						{
							projectile.velocity.Y = 0f;
						}
					}
				}
				if (projectile.ai[0] == 0f)
				{
					projectile.tileCollide = true;
					float num847 = 0.5f;
					float num848 = 4f;
					float num849 = 4f;
					float num850 = 0.1f;
					if (num849 < Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y))
					{
						num849 = Math.Abs(player.velocity.X) + Math.Abs(player.velocity.Y);
						num847 = 0.7f;
					}
					int num852 = 0;
					bool flag22 = false;
					float num853 = vector63.X - projectile.Center.X;
					if (Math.Abs(num853) > 5f)
					{
						if (num853 < 0f)
						{
							num852 = -1;
							if (projectile.velocity.X > 0f - num848)
							{
								projectile.velocity.X -= num847;
							}
							else
							{
								projectile.velocity.X -= num850;
							}
						}
						else
						{
							num852 = 1;
							if (projectile.velocity.X < num848)
							{
								projectile.velocity.X += num847;
							}
							else
							{
								projectile.velocity.X += num850;
							}
						}

						//flag22 = false;

					}
					else
					{
						projectile.velocity.X *= 0.9f;
						if (Math.Abs(projectile.velocity.X) < num847 * 2f)
						{
							projectile.velocity.X = 0f;
						}
					}
					if (num852 != 0)
					{
						int num854 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
						int num855 = (int)projectile.position.Y / 16;
						num854 += num852;
						num854 += (int)projectile.velocity.X;
						for (int num856 = num855; num856 < num855 + projectile.height / 16 + 1; num856++)
						{
							if (WorldGen.SolidTile(num854, num856))
							{
								flag22 = true;
							}
						}
					}
					if (projectile.velocity.X != 0f)
					{
						flag22 = true;
					}
					if (projectile.velocity.X != 0f)
					{
						flag22 = true;
					}
					Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
					if (projectile.velocity.Y == 0f && flag22)
					{
						for (int num857 = 0; num857 < 3; num857++)
						{
							int MinionTilePosX = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
							if (num857 == 0)
							{
								MinionTilePosX = (int)projectile.position.X / 16;
							}
							if (num857 == 2)
							{
								MinionTilePosX = (int)(projectile.position.X + (float)projectile.width) / 16;
							}
							int MinionTilePosY = (int)(projectile.position.Y + (float)projectile.height) / 16;
							if (!WorldGen.SolidTile(MinionTilePosX, MinionTilePosY) && !Main.tile[MinionTilePosX, MinionTilePosY].halfBrick() && Main.tile[MinionTilePosX, MinionTilePosY].slope() <= 0 && (!TileID.Sets.Platforms[Main.tile[MinionTilePosX, MinionTilePosY].type] || !Main.tile[MinionTilePosX, MinionTilePosY].active() || Main.tile[MinionTilePosX, MinionTilePosY].inActive()))
							{
								continue;
							}
							try
							{
								MinionTilePosX = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
								MinionTilePosY = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16;
								MinionTilePosX += num852;
								MinionTilePosX += (int)projectile.velocity.X;
								if (!WorldGen.SolidTile(MinionTilePosX, MinionTilePosY - 1) && !WorldGen.SolidTile(MinionTilePosX, MinionTilePosY - 2))
								{
									projectile.velocity.Y = -5.1f;
								}
								else if (!WorldGen.SolidTile(MinionTilePosX, MinionTilePosY - 2))
								{
									projectile.velocity.Y = -7.1f;
								}
								else if (WorldGen.SolidTile(MinionTilePosX, MinionTilePosY - 5))
								{
									projectile.velocity.Y = -11.1f;
								}
								else if (WorldGen.SolidTile(MinionTilePosX, MinionTilePosY - 4))
								{
									projectile.velocity.Y = -10.1f;
								}
								else
								{
									projectile.velocity.Y = -9.1f;
								}
							}
							catch
							{
								projectile.velocity.Y = -9.1f;
							}
						}
					}
					if (projectile.velocity.X > num849)
					{
						projectile.velocity.X = num849;
					}
					if (projectile.velocity.X < 0f - num849)
					{
						projectile.velocity.X = 0f - num849;
					}
					if (projectile.velocity.X < 0f)
					{
						projectile.direction = -1;
					}
					if (projectile.velocity.X > 0f)
					{
						projectile.direction = 1;
					}
					if (projectile.velocity.X > num847 && num852 == 1)
					{
						projectile.direction = 1;
					}
					if (projectile.velocity.X < 0f - num847 && num852 == -1)
					{
						projectile.direction = -1;
					}
					projectile.spriteDirection = projectile.direction;
					{
						projectile.rotation = 0f;
						if (projectile.velocity.Y == 0f)
						{
							if (projectile.velocity.X == 0f)
							{
								projectile.frame = 0;
								projectile.frameCounter = 0;
							}
							else if (Math.Abs(projectile.velocity.X) >= 0.1f) // 0.5f
							{
								projectile.frameCounter += (int)Math.Abs(projectile.velocity.X);
								projectile.frameCounter++;
								if (projectile.frameCounter > 10) // 10
								{
									projectile.frame++;
									projectile.frameCounter = 0;
								}
								if (projectile.frame >= 4) // 4
								{
									projectile.frame = 0;    //0
								}
							}
							else
							{
								projectile.frame = 0;
								projectile.frameCounter = 0;
							}
						}
						else if (projectile.velocity.Y != 0f)
						{
							projectile.frameCounter = 0;
							projectile.frame = 14;
						}
					}
					// hauteur du saut !
					projectile.velocity.Y += 1.1f; // 0.4f
					if (projectile.velocity.Y > 10f) //10f
					{
						projectile.velocity.Y = 10f; //10f
					}
				}
				projectile.frameCounter++;
				if (projectile.frameCounter >= frameSpeed)
				{
					projectile.frameCounter = 6;
					projectile.frame++;
					if (projectile.frame >= 7)
					{
						projectile.frame = 1;
					}
					else if (projectile.velocity.X == 0f)
					{
						projectile.frame = 0;
					}
				}
			}
		}
	}
}