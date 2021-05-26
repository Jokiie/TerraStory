using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class CoolieZombie : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Coolie Zombie");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 90;
			npc.scale = 0.55f;
			npc.aiStyle = 3;
			aiType = NPCID.Zombie;
			npc.damage = 15;
			npc.defense = 7;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.45f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CoolieZombieHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CoolieZombieDie");
			npc.value = Item.buyPrice(0, 0, 0, 65);
			npc.npcSlots = 1f;
			npc.netAlways = true;

		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .02f)
				Item.NewItem(npc.getRect(), ItemID.Shackle, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneHoly
			&& !player.ZoneJungle
			&& !player.ZoneBeach
			&& !player.ZoneUndergroundDesert
			&& player.ZoneOverworldHeight ? 1f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -2.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}
	}
}