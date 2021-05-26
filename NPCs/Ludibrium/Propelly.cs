using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{

	public class Propelly : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Propelly");
			Main.npcFrameCount[npc.type] = 8;
		}

		public override void SetDefaults()
		{
			npc.width = 58;
			npc.height = 48;
			npc.scale = 0.90f;

			npc.aiStyle = 14; 
			npc.damage = 35;
			npc.defense = 8;
			npc.lifeMax = 100;
			npc.knockBackResist = 0.25f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/PropellyHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/PropellyDie");
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& Main.hardMode
			&& !player.ZoneTowerNebula
			&& !player.ZoneTowerSolar
			&& !player.ZoneTowerStardust
			&& !player.ZoneTowerVortex
			&& player.ZoneOverworldHeight
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;

		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			// Determines the animation speed . positive value ex: 0.5f = higher speed
			npc.frameCounter -= -7.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>(), 3 - 8);
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>(), 3 - 8);
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.Placeable.PinkToyBlock>(), 1 - 5);
	    }
	}
}