using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using TerraStory.Items.Weapons.Thief.Shurikens;

namespace TerraStory.Projectiles.ShurikensProj
{
	public class BalancedFuryP : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Balanced Fury");
			Main.projFrames[projectile.type] = 4;
		}

		public override void SetDefaults() 
		{
			projectile.width = 22;
			projectile.height = 22;
			projectile.penetrate = 6;
			projectile.aiStyle = ProjectileID.Bullet;
			aiType = ProjectileID.Shuriken;
			projectile.tileCollide = true;
			projectile.friendly = true;
			projectile.thrown = true;
			projectile.ignoreWater = true;
		}
		public override void PostAI()
		{
			int frameSpeed = 15;
			projectile.frameCounter++;
			if (projectile.frameCounter >= frameSpeed)
			{
				projectile.frameCounter = 4; // Loop through the 4 animations frames.
				projectile.frame++;
				if (projectile.frame >= 3)
				{
					projectile.frame = 0;
				}
			}
		}
		public override void Kill(int timeLeft)
		{
			Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
			Main.PlaySound(SoundID.Item10, projectile.position);
			if (projectile.owner == Main.myPlayer)
			{
				int item =
				Main.rand.NextBool(18)
					? Item.NewItem(projectile.getRect(), ModContent.ItemType<BalancedFury>())
					: 0;

				if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
				{
					NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
				}
			}
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			projectile.penetrate--;
			if (projectile.penetrate <= 0)
			{
				projectile.Kill();
			}
			else
			{
				Collision.HitTiles(projectile.position + projectile.velocity, projectile.velocity, projectile.width, projectile.height);
				SoundManager.PlaySound(Sounds.LegacySoundStyle_Item10, projectile.position);
			}
			return false;
		}
	}
}