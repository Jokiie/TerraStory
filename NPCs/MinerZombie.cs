using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class MinerZombie : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Miner Zombie");
			Main.npcFrameCount[npc.type] = 3;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 60;
			npc.height = 90;
			npc.scale = 0.55f;
			npc.aiStyle = 3;
			aiType = NPCID.Zombie;
			npc.damage = 25;
			npc.defense = 9;
			npc.lifeMax = 70;
			npc.knockBackResist = 0.35f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MinerZombieHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MinerZombieDie");
			npc.value = Item.buyPrice(0, 0, 2, 50);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;			
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneHoly
			&& !player.ZoneJungle
			&& !player.ZoneUndergroundDesert
			&& spawnInfo.player.ZoneRockLayerHeight ? 0.20f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -2.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.ZombieGoldTooth>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .05f)
			Item.NewItem(npc.getRect(), ItemID.MiningHelmet, 1);
			if (Main.rand.NextFloat() < .024f)
			Item.NewItem(npc.getRect(), ItemID.MiningShirt, 1);
			if (Main.rand.NextFloat() < .024f)
			Item.NewItem(npc.getRect(), ItemID.MiningPants, 1);
			if (Main.rand.NextFloat() < .04f)
			Item.NewItem(npc.getRect(), ItemID.Hook, 1);
			if (Main.rand.NextFloat() < .04f)
			Item.NewItem(npc.getRect(), ItemID.Bomb, Main.rand.Next(0, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}