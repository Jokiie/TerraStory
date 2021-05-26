using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Bain : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bain");
			Main.npcFrameCount[npc.type] = 8;
		}

		public override void SetDefaults()
		{
			npc.width = 70; 
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 75;
			npc.defense = 45;
			npc.lifeMax = 400;
			npc.knockBackResist = 0.30f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BainHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BainDie");
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 12, 50);
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.ShadowFlame] = true;
			npc.lavaImmune = true;
			npc.netAlways = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			    Item.NewItem(npc.getRect(), ItemID.Hellstone, Main.rand.Next(1, 3));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Main.hardMode
			&& spawnInfo.player.ZoneUnderworldHeight ? 2.09f : 0f; // Mod Biome)
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -7.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}
	}
}