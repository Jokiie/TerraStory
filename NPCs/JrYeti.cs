using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class JrYeti : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Jr.Yeti");
			Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 10;
			npc.defense = 1;
			npc.lifeMax = 50;
			npc.knockBackResist = 1f;
			npc.npcSlots = 0.50f;
			npc.value = Item.buyPrice(0, 0, 0, 55);
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/JrYetiHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/JrYetiDie");
			
		}
		
	    public override void NPCLoot() 
		{
				Item.NewItem(npc.getRect(), ItemType<Items.Ect.LeattyFurball>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Thief.Shurikens.Mokbi>(), Main.rand.Next(10, 30));
		}
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			return spawnInfo.player.ZoneSnow ? 1f : 0f;
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