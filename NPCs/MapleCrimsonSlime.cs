using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class MapleCrimsonSlime : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Crimson Slime");
			Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 60;
			npc.height = 40;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			aiType = NPCID.LittleCrimslime;
			npc.alpha = 50;
			npc.damage = 22;
			npc.defense = 6;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SlimeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SlimeDie");
			npc.value = Item.buyPrice(0, 0, 4, 0);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.RedSquishyLiquid>());
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(2, 4));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.RedDye, 1 - 3);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			return SpawnCondition.Crimson.Chance * 1f;
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