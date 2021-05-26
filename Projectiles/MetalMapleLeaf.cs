using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace TerraStory.Projectiles
{
    /// <summary>
    /// Spins around a player, re-orients when player is attacking
    /// </summary>
    public class MetalMapleLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("MetalMapleLeaf");
        }
        public override void SetDefaults()
        {
            projectile.scale = 0.80f;
            projectile.width = 20;
            projectile.height = 20;
            projectile.penetrate = 2;
            projectile.timeLeft = 200;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}