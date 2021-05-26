using TerraStory.Items.Weapons.Minions;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Minions.OrangeMushroom
{

	/*
	 * This minion shows a few mandatory things that make it behave properly. 
	 * Its attack pattern is simple: If an enemy is in range of 43 tiles, it will fly to it and deal contact damage
	 * If the player targets a certain NPC with right-click, it will fly through tiles to it
	 * If it isn't attacking, it will float near the player with minimal movement
	 */
	 
	public class OrangeMushroom : ModProjectile
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Orange Mushroom");			
			Main.projFrames[projectile.type] = 6;		
			projectile.spriteDirection = projectile.direction;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			Main.projPet[projectile.type] = true;		
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;		
			ProjectileID.Sets.Homing[projectile.type] = true;
			
			int frameSpeed = 15;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed) {
				projectile.frameCounter = 6; // Loop through the 4 animations frames.
				projectile.frame++;
				if (projectile.frame >= Main.projFrames[projectile.type]) {
					projectile.frame = 6;
				}
			}
		}

		public sealed override void SetDefaults()
		{
			
			aiType = ProjectileID.BabySlime;	
			projectile.scale = 0.50f;			
			projectile.aiStyle = 26;		
			projectile.width = 63;		
			projectile.height = 55;		
			projectile.alpha = 0;	
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.minion = true;
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
			if (player.dead || !player.active) {
			player.ClearBuff(BuffType<OrangeMushroomBuff>());
			}
			if (player.HasBuff(BuffType<OrangeMushroomBuff>())) {
				projectile.timeLeft = 2;
			}
            TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
            if (player.dead)
            {
                modPlayer.OrangeMushroom = false;
            }
            if (modPlayer.OrangeMushroom)
            {
                projectile.timeLeft = 2;
            }
            #endregion

            #region General Behavior
            projectile.velocity.Y = projectile.velocity.Y + 0.1f;
            if (projectile.velocity.Y > 15f)
            {
                projectile.velocity.Y = 15f;
            }
            Vector2 idlePosition = player.Center;
            idlePosition.Y -= 1f;
            idlePosition.X += 1f;
            Tile tileIdlePosition = Framing.GetTileSafely((int)idlePosition.X / 16, (int)idlePosition.Y / 16);
            Vector2 vectorToIdlePosition = idlePosition - projectile.Center;
            float distanceToIdlePosition = vectorToIdlePosition.Length();
            if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f && player.velocity.Y == 0 && tileIdlePosition.active())
            {
                projectile.position = player.Center;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }
            else if (Main.myPlayer == player.whoAmI && distanceToIdlePosition > 2000f && player.velocity.Y == 0 && !tileIdlePosition.active())
            {
                projectile.position = idlePosition;
                projectile.velocity *= 0.1f;
                projectile.netUpdate = true;
            }
            #endregion

            #region Movement
            float speed;
            float inertia;
            if (distanceToIdlePosition < 600f)
            {
                speed = 4f;
                inertia = 60f;
            }
            else if (projectile.wet)
            {
                speed = 2f;
                inertia = 30f;
            }
            else if (projectile.honeyWet)
            {
                speed = 1.5f;
                inertia = 20f;
            }
            else
            {
                speed = 8f; ;
                inertia = 80f;
            }
            if (distanceToIdlePosition > 20f)
            {
                vectorToIdlePosition.Normalize();
                vectorToIdlePosition *= speed;
                projectile.velocity = (projectile.velocity * (inertia - 1) + vectorToIdlePosition) / inertia;
            }
            else if (projectile.velocity == Vector2.Zero)
            {
                projectile.velocity.X = -0.15f;
                projectile.velocity.Y = -0.15f;
            }
            Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY);
            if (projectile.velocity.Y == 0f)
            {
                if (projectile.velocity.X < 0f || projectile.velocity.X > 0f)
                {
                    int stepX = (int)(projectile.position.X + (float)(projectile.width / 8)) / 16;
                    int stepY = (int)(projectile.position.Y + (float)(projectile.height / 8)) / 16 + 1;
                    WorldGen.SolidTile(stepX, stepY);
                }
            }
            #endregion
		}
	}
}
