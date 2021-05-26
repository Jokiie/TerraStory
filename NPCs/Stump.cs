using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
    public class Stump : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stump");			
		    Main.npcFrameCount[npc.type] = 4;			
		}

		public override void SetDefaults()
		{
			npc.width = 40;
			npc.height = 40;
			npc.damage = 6;
			npc.defense = 5;
			npc.lifeMax = 70;
			npc.scale = 0.50f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/StumpHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/StumpDie");
			npc.knockBackResist = 0.40f;
			npc.aiStyle = 3;
			npc.value = Item.buyPrice(0, 0, 1, 0);
			npc.npcSlots = 0.50f;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return Main.dayTime
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& !player.ZoneSnow
			&& !player.ZoneCrimson
			&& !player.ZoneCorrupt
			&& !player.ZoneBeach
			&& !player.ZoneJungle
			&& !player.ZoneHoly
			&& player.ZoneOverworldHeight ? 1f : 0f;

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
			Item.NewItem(npc.getRect(), ItemID.Wood, Main.rand.Next(1, 10));
			Item.NewItem(npc.getRect(), ItemType<Items.Ect.StumpLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .15f)
			Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Mage.WoodenWand>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<WoodenTop>(), Main.rand.Next(10, 30));
		}
	}
}