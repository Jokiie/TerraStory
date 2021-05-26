using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class JrNecki : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Jr Necki");
			Main.npcFrameCount[npc.type] = 3;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 30;
			npc.height = 30;
			npc.scale = 0.80f;
			npc.aiStyle = 3;
			npc.damage = 8;
			npc.defense = 6;
			npc.lifeMax = 40;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/JrNeckiHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/JrNeckiDie");
			npc.value = Item.buyPrice(0, 0, 0, 85);
			npc.npcSlots = 0.50f;
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
		npc.frameCounter -= -2.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/JrNeckiBody"), 0.80f);
			}
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Thief.Shurikens.Subi>(), Main.rand.Next(10, 30));
		}
	}
}