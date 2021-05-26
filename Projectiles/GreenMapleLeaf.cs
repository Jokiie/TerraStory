using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System;

namespace TerraStory.Projectiles
{
    /// <summary>
    /// Spins around a player, re-orients when player is attacking
    /// </summary>
    public class GreenMapleLeaf : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("GreenMapleLeaf");
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.penetrate = 5;
            projectile.timeLeft = 600;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
        }

        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}