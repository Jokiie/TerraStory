using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class MommaBoyCactus : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Momma Boy Cactus");
			Main.npcFrameCount[npc.type] = 6;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 40;
			npc.height = 40;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 15;
			npc.defense = 8;
			npc.lifeMax = 60;
			npc.knockBackResist = 0.50f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MommaBoyCactusHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MommaBoyCactusDie");
			npc.value = Item.buyPrice(0, 0, 1, 50);
			npc.npcSlots = 0.50f;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			Item.NewItem(npc.getRect(), ItemID.Cactus, Main.rand.Next(1, 3));
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
			&& !player.ZoneUndergroundDesert
			&& player.ZoneOverworldHeight
			&& player.ZoneDesert ? 1f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -5.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;

		}
	}
}