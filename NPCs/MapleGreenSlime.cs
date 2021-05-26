using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class MapleGreenSlime : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Green Slime");
			Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 1; 
			aiType = NPCID.GreenSlime;
			npc.alpha = 50;
			npc.damage = 7;
			npc.defense = 0;
			npc.lifeMax = 16;
			npc.knockBackResist = 0f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SlimeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SlimeDie");
			npc.value = Item.buyPrice(0, 0, 0, 4);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(1, 3));
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.SquishyLiquid>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.GreenDye, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < 0.02f)
			    Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Minions.MapleGreenSlimeStaff>());
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
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneDesert
			&& !player.ZoneSnow
			&& !player.ZoneBeach
			&& spawnInfo.player.ZoneOverworldHeight ? 1f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -3.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}
	}
}