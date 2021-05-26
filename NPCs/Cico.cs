using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Cico : Fish
	{
		public Cico()
		{
			speed = 1f;
			speedY = 1f;
			acceleration = 0.05f;
			accelerationY = 0.05f;
			idleSpeed = 0.5f;
			bounces = false;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cico");

			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.scale = 0.60f;
			npc.width = 25;
			npc.height = 30;
			npc.lifeMax = 100;
			npc.damage = 25;
			npc.defense = 2;
			npc.knockBackResist = 0.30f;
			npc.aiStyle = -1;
			npc.noGravity = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CicoHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CicoDie");
			npc.value = Item.buyPrice(0, 0, 70, 0);
			npc.npcSlots = 1f;
			npc.netAlways = true;

		}

		public override void FindFrame(int frameHeight)
		{
			npc.frame.Y = 0;
			npc.rotation = 0f;
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -5.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CicoBody"), 0.60f);
			}
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return SpawnCondition.Ocean.Chance * 2.09f;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .05f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Warrior.TsunamiWave>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Thief.Shurikens.Mokbi>(), Main.rand.Next(10, 30));

		}
	}
}