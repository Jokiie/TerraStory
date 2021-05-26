using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class OrangeMushroom : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Orange Mushroom");
			Main.npcFrameCount[npc.type] = 3;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 60;
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			npc.damage = 6;
			npc.defense = 1;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MushroomHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MushroomDie");
			npc.value = Item.buyPrice(0, 0, 5, 50);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			Item.NewItem(npc.getRect(), ItemID.Mushroom, Main.rand.Next(1, 2));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneSnow
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneBeach
			&& !player.ZoneDesert
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