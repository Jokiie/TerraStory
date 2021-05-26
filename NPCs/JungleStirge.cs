using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class JungleStirge : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jungle Stirge");
			Main.npcFrameCount[npc.type] = 2;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 14;
			npc.damage = 22;
			npc.defense = 5;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.70f;
			npc.npcSlots = 0.50f;
			npc.value = Item.buyPrice(0, 0, 0, 80);
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/StirgeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/StirgeDie");
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneSnow
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneBeach
			&& player.ZoneJungle
			&& !player.ZoneHoly
			&& !player.ZoneUndergroundDesert
			&& !player.ZoneDesert ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -1.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .01f)
				Item.NewItem(npc.getRect(), ItemID.DepthMeter, 1);
			if (Main.rand.NextFloat() < .04f)
				Item.NewItem(npc.getRect(), ItemID.ChainKnife, 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}