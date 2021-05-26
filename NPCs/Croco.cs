using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Croco : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Croco");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 87;
			npc.height = 31;
			npc.scale = 0.60f;
			npc.aiStyle = 3;
			npc.damage = 85;
			npc.defense = 28;
			npc.lifeMax = 320;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/CrocoHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/CrocoDie");
			npc.value = Item.buyPrice(0, 0, 5, 50);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Confused] = true;
			npc.netAlways = true;

		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			if (player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
			{
				return 0f;
			}
			if (!(player.ZoneTowerSolar || player.ZoneTowerVortex || player.ZoneTowerNebula || player.ZoneTowerStardust || player.ZoneBeach) && ((!Main.pumpkinMoon && !Main.snowMoon) || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && (SpawnCondition.GoblinArmy.Chance == 0))
			{
				int[] TileArray2 = { TileID.Mud, TileID.JungleGrass };
				return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && player.ZoneJungle && Main.hardMode? 2.09f : 0f;
			}
			return 0f;
		}

		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -3.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CrocoBody"), 0.60f);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.CrocoSkin>(), 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Thief.Shurikens.Subi>(), Main.rand.Next(10, 30));
		}
	}
}