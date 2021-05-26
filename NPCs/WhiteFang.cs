using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class WhiteFang : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("White Fang");
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.65f;
			npc.aiStyle = 3;
			npc.damage = 20;
			npc.defense = 10;
			npc.lifeMax = 60;
			npc.knockBackResist = 0.60f;
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 1, 0);
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/WhiteFangHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/WhiteFangDie");

		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.WhiteFangTail>());
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

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/WhiteFangBody"), 0.65f);
			}
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