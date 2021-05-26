using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles.Minions.SoldierHong
{
    public class SoldierHong : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soldier Hong");
            Main.projFrames[projectile.type] = 15;
            projectile.spriteDirection = projectile.direction;
            ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
            Main.projPet[projectile.type] = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
        }

        public sealed override void SetDefaults()
        {

            //projectile.CloneDefaults(ProjectileID.PirateCaptain);
            aiType = ProjectileID.PirateCaptain;
            projectile.scale = 0.70f;
            projectile.aiStyle = 67;
            projectile.width = 40;
            projectile.height = 70;
            projectile.alpha = 0;
            projectile.tileCollide = true;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.melee = true;
            projectile.timeLeft = 300;
            //projectile.minionSlots = 0f;
            projectile.penetrate = 4;
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
            /*#region Active Check
            if (player.dead || !player.active)
            {
                player.ClearBuff(BuffType<SoldierHongBuff>());
            }
            if (player.HasBuff(BuffType<SoldierHongBuff>()))
            {
                projectile.timeLeft = 2;
            }
            TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
            if (player.dead)
            {
                modPlayer.SoldierHong = false;
            }
            if (modPlayer.SoldierHong)
            {
                projectile.timeLeft = 2;

            }
            #endregion*/

            #region Animation and visuals
            // So it will lean slightly towards the direction it's moving
            projectile.rotation = projectile.velocity.X * 0.01f;
            projectile.spriteDirection = projectile.direction;



            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 15;
            projectile.frameCounter++;
            if (projectile.frameCounter >= frameSpeed)
            {
                projectile.frameCounter = 15; // Loop through the 4 animations frames.
                projectile.frame++;
                if (projectile.frame >= Main.projFrames[projectile.type])
                {
                    projectile.frame = 0;

                }
                #endregion
            }
        }
    }
}