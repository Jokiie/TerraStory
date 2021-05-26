using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class TickTock : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Tick-Tock"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 11;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 51;  // hitbox
			npc.height = 54;
			npc.scale = 0.90f;
			npc.aiStyle = 3;
			npc.damage = 30;
			npc.defense = 18;
			npc.lifeMax = 130;
			npc.knockBackResist = 0.35f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/TickTockHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/TickTockDie");
			npc.value = Item.buyPrice(0, 0, 6, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneDesert
			&& !player.ZoneSnow
			&& !player.ZoneBeach
			&& spawnInfo.player.ZoneOverworldHeight // Vanilla Biome aka Zone
			&& spawnInfo.player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f; // Mod Biome)
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
		npc.frameCounter -= -10.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			//if (Main.rand.NextFloat() < .50f) // 50% chance
			//	Item.NewItem(npc.getRect(), ItemType<Items.Placeable.PinkToyBlock>(), 1 - 5);
		}
	}
}