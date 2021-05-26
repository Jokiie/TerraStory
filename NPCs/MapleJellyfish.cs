using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class MapleJellyFish : Fish
	{
		public MapleJellyFish()
		{
			speed = 1f;
			speedY = 1f;
			acceleration = 0.05f;
			accelerationY = 0.05f;
			idleSpeed = 0.5f;
			bounces = false;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple Jellyfish");
			
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;
			npc.scale = 0.60f;
			npc.lifeMax = 50;
			npc.damage = 25;
			npc.defense = 6;
			npc.knockBackResist = 0.30f;
			npc.aiStyle = -1;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CoolJellyFishHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CoolJellyFishDie");
			npc.value = Item.buyPrice(0, 0, 0, 60);
			npc.npcSlots = 1f;
			npc.noGravity = true;
			npc.netAlways = true;
		}
			
		public override void FindFrame(int frameHeight) 
		{
			npc.frame.Y = 0;
			npc.rotation = 0f;
		    npc.spriteDirection = npc.direction;
            npc.frameCounter -= -5.9f;
		    npc.frameCounter %= Main.npcFrameCount[npc.type];
		    int frame = (int)npc.frameCounter;
		    npc.frame.Y = frame * frameHeight;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			return SpawnCondition.Ocean.Chance * 1f;
		}
		
	    public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(1, 5));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .05f)
			Item.NewItem(npc.getRect(), ItemID.JellyfishNecklace, 1);
			if (Main.rand.NextFloat() < .15f)
				Item.NewItem(npc.getRect(), ItemID.BlackDye, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}