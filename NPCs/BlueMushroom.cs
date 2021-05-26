using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class BlueMushroom : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Mushroom");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			npc.damage = 60;
			npc.defense = 15;
			npc.lifeMax = 220;
			npc.knockBackResist = 0.30f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MushroomHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MushroomDie");
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 15, 50);
			npc.netAlways = true;

		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
			Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			Item.NewItem(npc.getRect(), ItemID.GlowingMushroom, Main.rand.Next(1, 2));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.ZoneGlowshroom ? 2.09f : 0;
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