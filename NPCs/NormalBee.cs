using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Boss;

namespace TerraStory.NPCs
{
	public class NormalBee : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee");
			Main.npcFrameCount[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.width = 24;
			npc.height = 26;
			npc.scale = 1f;
			npc.aiStyle = 14;
			npc.damage = 20;
			npc.defense = 5;
			npc.lifeMax = 125;
			npc.knockBackResist = 0.40f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/HornetHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/HornetDie");
			npc.value = Item.buyPrice(0, 0, 3, 0);
			npc.npcSlots = 1f;
			npc.netAlways = true;
			npc.noGravity = true;
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			Player player = spawnInfo.player;
			return Main.hardMode
			&& !player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
			&& player.ZoneDirtLayerHeight
			&& player.ZoneRockLayerHeight
			&& player.ZoneJungle ? 1f : 0f;
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
			if (Main.rand.NextFloat() < .01f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<BeeCorpse>(), 1);
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ModContent.ItemType<Items.MapleLeaf>());
		}
	}
}