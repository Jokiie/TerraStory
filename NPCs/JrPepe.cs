using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class JrPepe : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jr.Pepe");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.90f;
			npc.aiStyle = 3;
			npc.damage = 10;
			npc.defense = 1;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/JrPepeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/JrPepeDie");
			npc.npcSlots = 0.75f;
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;

		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.ZoneSnow ? 1f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -5.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}