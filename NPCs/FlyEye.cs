using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class FlyEye : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flyeye");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 14;
			npc.damage = 18;
			npc.defense = 2;
			npc.lifeMax = 60;
			npc.knockBackResist = 0.20f;
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 0, 75);
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/StirgeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/StirgeDie");
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !Main.dayTime
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneBeach
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneUndergroundDesert
			&& !player.ZoneDesert
			&& player.ZoneOverworldHeight ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -1.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .33f)
				Item.NewItem(npc.getRect(), ItemID.Lens, 1);
			if (Main.rand.NextFloat() < .01f)
				Item.NewItem(npc.getRect(), ItemID.BlackLens, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}