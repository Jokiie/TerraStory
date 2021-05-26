using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{

	public class MapleCaveSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coke slime");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			aiType = NPCID.BlackSlime;
			npc.alpha = 50;
			npc.damage = 16;
			npc.defense = 5;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.45f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SlimeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SlimeDie");
			npc.value = Item.buyPrice(0, 0, 0, 25);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;

		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.BrownDye, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Main.dayTime
			&& !spawnInfo.player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !spawnInfo.player.ZoneCrimson
			&& !spawnInfo.player.ZoneCorrupt
			&& !spawnInfo.player.ZoneHoly
			&& !spawnInfo.player.ZoneJungle
			&& !spawnInfo.player.ZoneSnow
			&& !spawnInfo.player.ZoneDesert
			&& !spawnInfo.player.ZoneDungeon
			&& !spawnInfo.player.ZoneMeteor
			&& !spawnInfo.player.ZoneUndergroundDesert
			&& !spawnInfo.player.ZoneSkyHeight
			&& spawnInfo.player.ZoneDirtLayerHeight
			&& spawnInfo.player.ZoneRockLayerHeight ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}