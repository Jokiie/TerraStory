using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Bubbling : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bubbling");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 32;
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 1;
			aiType = NPCID.IceSlime;
			npc.alpha = 50;
			npc.damage = 9;
			npc.defense = 4;
			npc.lifeMax = 35;
			npc.knockBackResist = 0.30f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SlimeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SlimeDie");
			npc.value = Item.buyPrice(0, 0, 0, 50);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Frostburn] = true;
			npc.netAlways = true;

		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneHoly
			&& !player.ZoneJungle
			&& !player.ZoneDesert
			&& player.ZoneOverworldHeight
			&& player.ZoneSnow ? 2.09f : 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
			npc.AddBuff(BuffID.Chilled, 600, true);
        }
    }
}