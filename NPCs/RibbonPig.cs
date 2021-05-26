using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class RibbonPig : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ribbon Pig");
			Main.npcFrameCount[npc.type] = 3;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 40; 
			npc.height = 40;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 6;
			npc.defense = 5;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.45f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/PigHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/PigDie");
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.npcSlots = 0.50f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneSnow
			&& !player.ZoneCorrupt
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneDesert
			&& !player.ZoneBeach
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

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}