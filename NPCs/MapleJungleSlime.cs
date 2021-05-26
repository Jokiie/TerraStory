using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class MapleJungleSlime : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("MapleStory's Jungle Slime"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 4;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 40;
			npc.height = 40;
			npc.scale = 0.70f;
			npc.aiStyle = 1;
			aiType = NPCID.JungleSlime;
			npc.alpha = 50;
			npc.damage = 18;
			npc.defense = 6;
			npc.lifeMax = 60;
			npc.knockBackResist = 0f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/SlimeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/SlimeDie");
			npc.value = Item.buyPrice(0, 0, 5, 0);
			npc.npcSlots = 1f;
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.netAlways = true;
		}
		
	    public override void NPCLoot() 
		{
			Item.NewItem(npc.getRect(), ItemID.Gel, Main.rand.Next(2, 4));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.PinkDye, 0 - 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemID.LimeDye, Main.rand.Next(0, 1));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < 0.05f)
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Ranger.Ryden>());
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
				int[] TileArray2 = { TileID.Mud, TileID.JungleGrass};
				return TileArray2.Contains(Main.tile[spawnInfo.spawnTileX, spawnInfo.spawnTileY].type) && player.ZoneJungle? 1f : 0f;
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
	}
}