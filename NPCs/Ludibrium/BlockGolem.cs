using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class BlockGolem : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Block Golem"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 12;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 212;  // hitbox
			npc.height = 224;
			npc.scale = 0.90f;
			npc.aiStyle = 3;
			npc.damage = 40;
			npc.defense = 20;
			npc.lifeMax = 400;
			npc.knockBackResist = 0.15f;
			npc.value = 2f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BlockGolemHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BlockGolemDie");
			npc.value = Item.buyPrice(0, 0, 8, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& Main.hardMode
			&& player.ZoneDirtLayerHeight
			&& !player.ZoneTowerNebula
			&& !player.ZoneTowerSolar
			&& !player.ZoneTowerStardust
			&& !player.ZoneTowerVortex
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 0.50f : 0f; // Mod Biome)
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
		npc.frameCounter -= -11.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>(), 1 - 15);
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>(), 1 - 15);
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.Placeable.OrangeLudiBlock>(), 1 - 5);
		}
	}
}