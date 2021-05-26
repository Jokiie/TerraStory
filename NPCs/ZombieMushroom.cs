using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class ZombieMushroom : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Zombie Mushroom"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 60;  // hitbox
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			npc.damage = 6;
			npc.defense = 5;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.40f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MushroomHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MushroomDie");
			npc.value = Item.buyPrice(0, 0, 5, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			Item.NewItem(npc.getRect(), ItemID.VileMushroom, Main.rand.Next(1,2));
			Item.NewItem(npc.getRect(), ItemID.ViciousMushroom, Main.rand.Next(1, 2));
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneSnow
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneBeach
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& player.ZoneOverworldHeight ? 2.09f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
		npc.frameCounter -= -3.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}
	}
}