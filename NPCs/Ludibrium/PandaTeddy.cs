using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class PandaTeddy : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Panda Teddy"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 8;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 46;  // hitbox
			npc.height = 50;
			npc.scale = 0.65f;
			npc.aiStyle = 3;
			npc.damage = 35;
			npc.defense = 20;
			npc.lifeMax = 180;
			npc.knockBackResist = 0.35f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/PandaTeddyHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/PandaTeddyDie");
			npc.value = Item.buyPrice(0, 0, 6, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& Main.hardMode
			&& !player.ZoneTowerNebula
			&& !player.ZoneTowerSolar
			&& !player.ZoneTowerStardust
			&& !player.ZoneTowerVortex
			&& player.ZoneOverworldHeight
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
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