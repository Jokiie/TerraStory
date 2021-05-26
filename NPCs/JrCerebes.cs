using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class JrCerebes : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Jr.Cerebes");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 70;
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 42;
			npc.defense = 8;
			npc.lifeMax = 140;
			npc.knockBackResist = 0.20f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/JrCerebesHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/JrCerebesDie");
			npc.value = Item.buyPrice(0, 0, 1, 50);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.lavaImmune = true;
			npc.netAlways = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			Item.NewItem(npc.getRect(), ItemID.Hellstone, Main.rand.Next(1, 3));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return spawnInfo.player.ZoneUnderworldHeight ? 1f : 0f;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JrCerebesBody"), 0.60f);
			}
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}