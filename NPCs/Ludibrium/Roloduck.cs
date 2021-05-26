using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class Roloduck : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Rolo duck"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 8;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 104;  // hitbox
			npc.height = 44;
			npc.scale = 0.80f;
			npc.aiStyle = 3;
			npc.damage = 30;
			npc.defense = 14;
			npc.lifeMax = 160;
			npc.knockBackResist = 0.40f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/RoloduckHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/RoloduckDie");
			npc.value = Item.buyPrice(0, 0, 2, 50);
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
			if (Main.rand.NextFloat() < .20f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>(), 3 - 9);
			if (Main.rand.NextFloat() < .20f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>(), 3 - 9);
			//if (Main.rand.NextFloat() < .50f) // 50% chance
			//	Item.NewItem(npc.getRect(), ItemType<Items.Placeable.PinkToyBlock>(), 1 - 5);
		}
	}
}