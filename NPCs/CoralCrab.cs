using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class CoralCrab : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Coral Crab");
			Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 40;
			npc.height = 40;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 20;
			npc.defense = 10;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.30f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.value = Item.buyPrice(0, 0, 0, 60);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		
	    public override void NPCLoot() 
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			Item.NewItem(npc.getRect(), ItemID.Coral, Main.rand.Next(1,3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			return SpawnCondition.Ocean.Chance * 2.09f;			
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