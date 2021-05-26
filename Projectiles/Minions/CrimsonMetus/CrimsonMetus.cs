using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Projectiles.Minions.CrimsonMetus
{
	public class CrimsonMetus : HoverShooter
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Metus");
			Main.projFrames[projectile.type] = 6;
			projectile.spriteDirection = projectile.direction;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;

		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 50;
			projectile.height = 66;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1;
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			inertia = 20f;
			shoot = ProjectileType<CrimsonGhost>();
			shootSpeed = 12f;
			projectile.scale = 0.80f;
		}

		public override void CheckActive()
		{
			Player player = Main.player[projectile.owner];
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.dead)
			{
				modPlayer.CrimsonMetus = false;
			}
			if (modPlayer.CrimsonMetus)
			{ // Make sure you are resetting this bool in ModPlayer.ResetEffects. See ExamplePlayer.ResetEffects
				projectile.timeLeft = 2;
			}
		}

		public override void CreateDust() {
			if (projectile.ai[0] == 0f) {
				if (Main.rand.NextBool(5)) {
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height / 2, DustID.Fire);
					Main.dust[dust].velocity.Y -= 1.2f;
				}
			}
			else 
			{
				if (Main.rand.NextBool(3)) {
					Vector2 dustVel = projectile.velocity;
					if (dustVel != Vector2.Zero) {
						dustVel.Normalize();
					}
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fireworks);
					Main.dust[dust].velocity -= 1.2f * dustVel;
				}
			}
			Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
		}

		public override void SelectFrame()
		{
			projectile.frameCounter++;
			if (projectile.frameCounter >= 12)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 3;
			}
		}
	}
}