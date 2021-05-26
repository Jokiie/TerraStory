using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Boss;
using TerraStory.Projectiles;

namespace TerraStory.NPCs
{
    public class CryBee : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crying bee");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 26;
			npc.scale = 1f;
			npc.aiStyle = 14;
			npc.damage = 20;
			npc.defense = 5;
			npc.lifeMax = 80;
			npc.knockBackResist = 0.40f;
			npc.noGravity = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/HornetHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/HornetDie");
			npc.value = Item.buyPrice(0, 0, 0, 0);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.hardMode
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& player.ZoneDirtLayerHeight
			&& player.ZoneRockLayerHeight
			&& player.ZoneJungle ? 2.09f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
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
					Projectile.NewProjectile((float)num1169, (float)num1170, 0f, 5f, ModContent.ProjectileType<CryBeeTear>(), num184, 0f, Main.myPlayer, 0f, 0f);
					return;
				}
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .01f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<BeeCorpse>(), 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MapleLeaf>());
		}
	}
}