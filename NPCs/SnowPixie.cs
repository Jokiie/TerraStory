using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class SnowPixie : Hover
	{
		public SnowPixie()
		{
			acceleration = 0.06f;
			accelerationY = 0.025f;
		}

		public override void SetStaticDefaults()
		{
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.CloneDefaults(NPCID.Wraith);
			npc.aiStyle = 22;
			npc.width = 28;
			npc.height = 36;
			npc.scale = 0.60f;
			npc.damage = 17;
			npc.defense = 9;
			npc.lifeMax = 60;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/LusterPixieHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/LusterPixieDie");
			animationType = NPCID.Wraith;
			npc.value = Item.buyPrice(0, 0, 2, 0);
			npc.npcSlots = 1f;
			npc.netAlways = true;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& !spawnInfo.invasion
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& player.ZoneSnow
			&& !player.ZoneJungle
			&& !player.ZoneDesert
			&& !player.ZoneBeach
			&& !spawnInfo.playerSafe
			&& player.ZoneOverworldHeight ? 1f : 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life > 0)
			{
				for (int i = 0; i < damage / npc.lifeMax * 100; i++)
				{
					Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, hitDirection, -1f, 100, new Color(100, 100, 100, 100), 1f);
					dust.noGravity = true;
				}
				return;
			}
			for (int i = 0; i < 50; i++)
			{
				Dust dust = Dust.NewDustDirect(npc.position, npc.width, npc.height, 192, 2 * hitDirection, -2f, 100, new Color(100, 100, 100, 100), 1f);
				dust.noGravity = true;
			}
		}

		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 200)
			{
				if (distance > 100)
				{
					scale *= (100 - (distance - 100)) / 100;
				}
				return null;
			}
			return false;
		}

		public override void CustomBehavior(ref float ai)
		{
			float distance = npc.Distance(Main.player[npc.target].Center);
			if (distance <= 250)
			{
				npc.alpha = 100;
				if (distance > 100)
				{
					npc.alpha += (int)(155 * ((distance - 100) / 150));
				}
				return;
			}
			npc.alpha = 150;
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .01f)
				Item.NewItem(npc.getRect(), ItemID.FastClock, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
		private int timer;
		public override void AI()
		{
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			Vector2 direction = npc.DirectionTo(player.Center);
			direction *= 8f;

			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				timer++;
				if (timer > 120)
				{
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X, direction.Y, mod.ProjectileType("PixieP"), npc.damage / 3, 0f, Main.myPlayer, player.Center.X, player.Center.Y); // Our timer has finished, do something here:
																																																	   // Main.PlaySound, Dust.NewDust, Projectile.NewProjectile, etc. Up to you.
					timer = 0;
				}
			}
		}
		public override void FindFrame(int frameHeight) 
		{ 
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -5.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}