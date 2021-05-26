using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class MasterSoulTeddy : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Master Soul Teddy"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 6;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 122;  // hitbox
			npc.height = 135;
			npc.scale = 0.80f;
			npc.aiStyle = 3;
			npc.damage = 25;
			npc.defense = 10;
			npc.lifeMax = 80;
			npc.knockBackResist = 0.40f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MasterSoulTeddyHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MasterSoulTeddyDie");
			npc.value = Item.buyPrice(0, 0, 2, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& !Main.hardMode
			&& player.ZoneRockLayerHeight
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
		npc.frameCounter -= -5.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>(), 5 - 10);
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>(), 5 - 10);
		}
	}
}