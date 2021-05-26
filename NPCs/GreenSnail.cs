using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{

	public class GreenSnail : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Green Snail");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 37;
			npc.height = 28;
			npc.aiStyle = 67;
			aiType = 360;
			npc.CloneDefaults(360);
			npc.damage = 3;
			npc.defense = 0;
			npc.lifeMax = 30;
			npc.knockBackResist = 0.30f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SnailHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SnailDie");
			npc.value = Item.buyPrice(0, 0, 0, 5);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Slow] = true;
			npc.noGravity = true;
			npc.netAlways = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneDesert
			&& !player.ZoneSnow
			&& !player.ZoneBeach
			&& spawnInfo.player.ZoneOverworldHeight ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter -= -5.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.GreenSnailShell>(), 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}