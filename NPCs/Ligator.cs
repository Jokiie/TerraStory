using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class Ligator : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ligator");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 87;
			npc.height = 31;
			npc.scale = 0.80f;
			npc.aiStyle = 3;
			npc.damage = 8;
			npc.defense = 6;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/LigatorHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/LigatorDie");
			npc.value = Item.buyPrice(0, 0, 0, 85);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
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
				return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && player.ZoneJungle ? 1f : 0f;
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
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/LigatorBody"), 0.80f);
			}
		}

		public override void NPCLoot()
		{
			Item.NewItem(npc.getRect(), ItemType<LigatorSkin>(), 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Thief.Shurikens.Subi>(), Main.rand.Next(10, 30));
		}
	}
}