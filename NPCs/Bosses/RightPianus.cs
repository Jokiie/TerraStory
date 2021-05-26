using System;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Bosses
{


	public class RightPianus : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Pianus");
			Main.npcFrameCount[npc.type] = 36;
		}

		public override void SetDefaults()
		{

			npc.width = 416;
			npc.height = 388;
			npc.aiStyle = -1;
			npc.npcSlots = 3f;
			npc.noTileCollide = false;
			npc.scale = 0.99f;
			npc.lifeMax = 1500;
			npc.damage = 40;
			npc.defense = 10;
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(gold: 1);
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/MushmomHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/MushmomDie");
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RedWitch");
		}

		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.8f);
		}

		public override void NPCLoot()
		{

		}

		public override void BossLoot(ref string name, ref int potionType)
		{

		}

		public override void FindFrame(int frameHeight)
		{
			// This makes the sprite flip horizontally in conjunction with the npc.direction.
			npc.spriteDirection = npc.direction;
			// Determines the animation speed . positive value ex: 0.5f = higher speed
			npc.frameCounter -= -35.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}