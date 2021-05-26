using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.GFX;
using TerraStory.Tiles;

namespace TerraStory.NPCs
{
	public class LudibriumMimic : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ludibrium Mimic");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 56;
			npc.height = 48;
			npc.scale = 0.80f;
			npc.damage = 80;
			npc.defense = 35;
			npc.lifeMax = 550;
			npc.HitSound = SoundID.NPCHit4;
			npc.DeathSound = SoundID.NPCDeath6;
			npc.value = 50000f;
			npc.knockBackResist = .30f;
			npc.aiStyle = 25;
			aiType = NPCID.Mimic;
		}
		int frame;
		int timer;
		public override void AI()
		{
			if (npc.velocity != Vector2.Zero)
			{
				timer++;
				if (timer >= 12)
				{
					frame++;
					timer = 0;
				}
				if (frame > 4)
				{
					frame = 2;
				}
			}
			else
			{
				frame = 0;
				
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			int d1 = 236;
			for (int k = 0; k < 5; k++)
			{
				{
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
					Dust.NewDust(npc.position, npc.width, npc.height, d1, 2.5f * hitDirection, -2.5f, 0, default(Color), 0.7f);
				}
			}
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, 99);
				Gore.NewGore(npc.position, npc.velocity, 99);
				Gore.NewGore(npc.position, npc.velocity, 99);
			}
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frame.Y = frameHeight * frame;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = (int)Main.tile[x, y].type;
			return (tile == ModContent.TileType<OrangeLudiBlock>()
				|| tile == ModContent.TileType<YellowToyBlockTile>() 
				|| tile == ModContent.TileType<CyanToyBlock>()
				|| tile == ModContent.TileType<DeepBlueToyBlock>()
				|| tile == ModContent.TileType<RedToyBlock>()
				|| tile == ModContent.TileType<LudiTreeTile>())
				&& Main.hardMode 
				&& spawnInfo.spawnTileY > Main.rockLayer ? 0.20f : 0f;
		}

		public override void NPCLoot()
		{
			string[] lootTable = { "BundleOfMesos" };
			int loot = Main.rand.Next(lootTable.Length);
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType(lootTable[loot]));
		}
        public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
        {
			MasksHelper.DrawMimicGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/GlowMasks/LudibriumMimic_Glow"));
		}
    }
}