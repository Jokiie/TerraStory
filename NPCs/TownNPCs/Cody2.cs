using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.TownNPCs
{
	public class Cody2 : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Incouscious person");
			NPCID.Sets.TownCritter[npc.type] = true;
		}

		public override void SetDefaults()
		{

			npc.friendly = true;
			npc.townNPC = true;
			npc.scale = 0.70f;
			npc.dontTakeDamage = true;
			npc.width = 32;
			npc.height = 48;
			npc.aiStyle = 0;
			npc.damage = 0;
			npc.defense = 25;
			npc.lifeMax = 10000;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0f;
			npc.rarity = 1;
		}
		public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			return false;
		}
		public override string GetChat()
		{
			npc.Transform(NPCType<Cody>());
			npc.dontTakeDamage = false;
			return "Ugh ... Thanks for waking me up! Something crazy happened to me! I had taken the Henesys cab to go to a wedding, but I lost consciousness..When I woke up I was here..But what is this world? Everything stings me! I believe I am allergic to almost everything here. Since I wanted to make a house to protect myself, I planted a ton of special Henesys mushrooms! But I am too tired to pick them. If you could help me, and build me this house while I rest, I promise you that I will build you tons of really useful items with the mushrooms!";
		}
		public override void AI()
		{
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				npc.homeless = false;
				npc.homeTileX = -1;
				npc.homeTileY = -1;
				npc.netUpdate = true;
			}

			if (npc.wet)
			{
				npc.life = 250;
			}
			foreach (var player in Main.player)
			{
				if (!player.active) continue;
				if (player.talkNPC == npc.whoAmI)
				{
					Rescue();
					return;
				}
			}
		}
		public void Rescue()
		{
			npc.Transform(NPCType<Cody>());
			npc.dontTakeDamage = false;
			World.rescuedCody2 = true;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			if (NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
			{
				if (!World.rescuedCody2 && !NPC.AnyNPCs(ModContent.NPCType<Cody>()) && !NPC.AnyNPCs(ModContent.NPCType<Cody2>()))
				{
					return SpawnCondition.BoundCaveNPC.Chance * 1f;
				}
			}
			return 0f;
		}
	}
}
