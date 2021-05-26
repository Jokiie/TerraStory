using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class Bloctopus : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloctopus"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 10;
		}

		public override void SetDefaults()
		{
			npc.width = 47;  // hitbox
			npc.height = 54;
			npc.scale = 0.80f;
			npc.aiStyle = 3;
			npc.damage = 30;
			npc.defense = 15;
			npc.lifeMax = 150;
			npc.knockBackResist = 0.30f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/BloctopusHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/BloctopusDie");
			npc.value = Item.buyPrice(0, 0, 2, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;

		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& Main.hardMode
			&& spawnInfo.player.ZoneDirtLayerHeight
			&& !player.ZoneTowerNebula
			&& !player.ZoneTowerSolar
			&& !player.ZoneTowerStardust
			&& !player.ZoneTowerVortex
			&& spawnInfo.player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			// Determines the animation speed . positive value ex: 0.5f = higher speed
			npc.frameCounter -= -9.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}