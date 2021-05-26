using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Boss;
using TerraStory.Projectiles;

namespace TerraStory.NPCs
{
	public class Rain : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rain");
			Main.npcFrameCount[npc.type] = 7;
		}

		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 26;
			npc.scale = 1f;
			npc.aiStyle = 14;
			npc.damage = 9;
			npc.defense = 2;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.30f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(0, 0, 3, 0);
			npc.npcSlots = 1.5f;
			npc.netAlways = true;
			npc.noGravity = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& player.ZoneOverworldHeight
			&& player.ZoneSkyHeight
			&& Main.raining ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -6.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void AI()
		{
			npc.TargetClosest(true);
			float num1164 = 4f;
			float num1165 = 0.75f;
			Vector2 vector133 = new Vector2(npc.Center.X, npc.Center.Y);
			float num1166 = Main.player[npc.target].Center.X - vector133.X;
			float num1167 = Main.player[npc.target].Center.Y - vector133.Y - 200f;
			float num1168 = (float)Math.Sqrt((double)(num1166 * num1166 + num1167 * num1167));
			if (num1168 < 20f)
			{
				num1166 = npc.velocity.X;
				num1167 = npc.velocity.Y;
			}
			else
			{
				num1168 = num1164 / num1168;
				num1166 *= num1168;
				num1167 *= num1168;
			}
			if (npc.velocity.X < num1166)
			{
				npc.velocity.X = npc.velocity.X + num1165;
				if (npc.velocity.X < 0f && num1166 > 0f)
				{
					npc.velocity.X = npc.velocity.X + num1165 * 2f;
				}
			}
			else if (npc.velocity.X > num1166)
			{
				npc.velocity.X = npc.velocity.X - num1165;
				if (npc.velocity.X > 0f && num1166 < 0f)
				{
					npc.velocity.X = npc.velocity.X - num1165 * 2f;
				}
			}
			if (npc.velocity.Y < num1167)
			{
				npc.velocity.Y = npc.velocity.Y + num1165;
				if (npc.velocity.Y < 0f && num1167 > 0f)
				{
					npc.velocity.Y = npc.velocity.Y + num1165 * 2f;
				}
			}
			else if (npc.velocity.Y > num1167)
			{
				npc.velocity.Y = npc.velocity.Y - num1165;
				if (npc.velocity.Y > 0f && num1167 < 0f)
				{
					npc.velocity.Y = npc.velocity.Y - num1165 * 2f;
				}
			}
			if (npc.position.X + (float)npc.width > Main.player[npc.target].position.X && npc.position.X < Main.player[npc.target].position.X + (float)Main.player[npc.target].width && npc.position.Y + (float)npc.height < Main.player[npc.target].position.Y && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].height) && Main.netMode != NetmodeID.MultiplayerClient)
			{
				npc.ai[0] += 4f;
				if (npc.ai[0] > 32f)
				{
					npc.ai[0] = 0f;
					int num1169 = (int)(npc.position.X + 10f + (float)Main.rand.Next(npc.width - 20));
					int num1170 = (int)(npc.position.Y + (float)npc.height + 4f);
					int num184 = 26;
					if (Main.expertMode)
					{
						num184 = 14;
					}
					Projectile.NewProjectile((float)num1169, (float)num1170, 0f, 5f, ModContent.ProjectileType<RainProj>(), num184, 0f, Main.myPlayer, 0f, 0f);
					Gore.NewGore(npc.position, npc.velocity, GoreID.WaterDrip, 0.60f);
					return;
				}
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.Cloud, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MapleLeaf>());
		}
	}
}