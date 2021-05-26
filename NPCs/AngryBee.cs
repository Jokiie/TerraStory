using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Boss;

namespace TerraStory.NPCs
{
    public class AngryBee : ModNPC
    {

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angry bee");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 26;
			npc.scale = 1f;
			npc.aiStyle = 14;
			npc.damage = 50;
			npc.defense = 5;
			npc.lifeMax = 100;
			npc.knockBackResist = 0.40f;
			npc.noGravity = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/HornetHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/HornetDie");
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 10, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.hardMode
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& player.ZoneDirtLayerHeight
			&& player.ZoneRockLayerHeight
			&& player.ZoneJungle ? 2.09f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .01f) // 1% chance
				Item.NewItem(npc.getRect(), ModContent.ItemType<BeeCorpse>(), 1);
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MapleLeaf>());
		}
	}
}