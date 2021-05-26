using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class BrownTeddy : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("BrownTeddy"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 8;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 51;  // hitbox
			npc.height = 50;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 15;
			npc.defense = 5;
			npc.lifeMax = 80;
			npc.knockBackResist = 0.45f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BrownTeddyHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BrownTeddyDie");
			npc.value = Item.buyPrice(0, 0, 2, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.ZoneTowerNebula
			&& !player.ZoneTowerSolar
			&& !player.ZoneTowerStardust
			&& !player.ZoneTowerVortex
			&& player.ZoneOverworldHeight
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -7.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.TeddysCotton>());
			if (Main.rand.NextFloat() < .20f) // 20% chance et -
				Item.NewItem(npc.getRect(), ItemType<Items.Placeable.CottonPlantSeedItem>(), 1 - 3);
			if (Main.rand.NextFloat() < .20f) // 20% chance et -
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f) // 20% chance et -
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}