using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Siren : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Siren");
			Main.npcFrameCount[npc.type] = 6;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 40; 
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 14; 
			npc.damage = 13;
			npc.defense = 2;
			npc.lifeMax = 16;
			npc.knockBackResist = 0.50f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SirenHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SirenDie");
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;

        }
		
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			if (player.ZoneSkyHeight)
				return 1f;
			else
				return 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -5.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .50f)
			Item.NewItem(npc.getRect(), ItemID.Feather, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}