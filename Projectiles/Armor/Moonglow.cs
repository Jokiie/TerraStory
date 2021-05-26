using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Buffs;

namespace TerraStory.Projectiles.Armor
{
    public class Moonglow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moonglow");
        }
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 8;
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
			projectile.light = 0.1f;
			projectile.aiStyle = -1;
			projectile.penetrate = 1;
		}
        public override void AI()
        {
			Player player = Main.player[projectile.owner];
			#region Active Check
			if (player.dead || !player.active)
			{
				player.ClearBuff(BuffType<JungleArmorBuff>());
			}
			if (player.HasBuff(BuffType<JungleArmorBuff>()))
			{
				projectile.timeLeft = 2;
			}
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.dead)
			{
				modPlayer.moonGlow = false;
			}
			if (modPlayer.moonGlow)
			{
				projectile.timeLeft = 2;
				#endregion
			}
			projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2) + Main.player[projectile.owner].gfxOffY - 60f;
			if (Main.player[projectile.owner].gravDir == -1f)
			{
				projectile.position.Y += 120f;
				projectile.rotation = 3.14f;
			}
			else
			{
				projectile.rotation = 0f;
			}
			projectile.position.X = (int)projectile.position.X;
			projectile.position.Y = (int)projectile.position.Y;
			float num535 = (float)(int)Main.mouseTextColor / 200f - 0.35f;
			num535 *= 0.2f;
			projectile.scale = num535 + 0.95f;
			if (projectile.owner != Main.myPlayer)
			{
				return;
			}
			if (this.projectile.ai[0] == 0f)
			{
				float num536 = projectile.position.X;
				float num537 = projectile.position.Y;
				float num538 = 700f;
				bool flag3 = false;
				for (int num539 = 0; num539 < 200; num539++)
				{
					if (Main.npc[num539].CanBeChasedBy(this, ignoreDontTakeDamage: true))
					{
						float num540 = Main.npc[num539].position.X + (float)(Main.npc[num539].width / 2);
						float num542 = Main.npc[num539].position.Y + (float)(Main.npc[num539].height / 2);
						float num543 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num540) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num542);
						if (num543 < num538 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num539].position, Main.npc[num539].width, Main.npc[num539].height))
						{
							num538 = num543;
							num536 = num540;
							num537 = num542;
							flag3 = true;
						}
					}
				}
				if (flag3)
				{
					float num544 = 12f;
					Vector2 vector48 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num545 = num536 - vector48.X;
					float num546 = num537 - vector48.Y;
					float num547 = (float)Math.Sqrt(num545 * num545 + num546 * num546);
					float num548 = num547;
					num547 = num544 / num547;
					num545 *= num547;
					num546 *= num547;
					Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num545, num546, ProjectileType<MiniMoonglow>(), 20, 10, projectile.owner);
					this.projectile.ai[0] = 50f;
				}
			}
			else
			{
				this.projectile.ai[0] -= 1f;
			}
		}
	}
}
