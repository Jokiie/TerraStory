using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class HellStirge : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Hell Stirge");
			Main.npcFrameCount[npc.type] = 2;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 32; 
			npc.height = 32;
			npc.scale = 0.60f;
			npc.aiStyle = 14;
			aiType = 60;
			npc.damage = 40;
			npc.defense = 8;
			npc.lifeMax = 50;
			npc.knockBackResist = 0.20f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/HellStirgeHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/HellStirgeDie");
			npc.value = Item.buyPrice(0, 0, 1, 50);
			npc.npcSlots = 0.75f;
			npc.buffImmune[BuffID.OnFire] = true;
			npc.lavaImmune = true;
			npc.netAlways = true;
			
		}

        public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			return SpawnCondition.Underworld.Chance * 1f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		npc.spriteDirection = npc.direction; 
		npc.frameCounter -= -1.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void DrawEffects(ref Color drawColor)
		{
			for (int l = 0; l < 2; l++)
			{
				int num20 = Dust.NewDust(new Vector2(npc.position.X - npc.velocity.X * 2f, npc.position.Y - 2f - npc.velocity.Y * 2f), npc.width, npc.height, 6, 0f, 0f, 175, default(Color), 2f);
				Main.dust[num20].noGravity = true;
				Main.dust[num20].noLight = true;
				Main.dust[num20].velocity.X -= npc.velocity.X * 0.5f;
				Main.dust[num20].velocity.Y -= npc.velocity.Y * 0.5f;
			}
		}

        public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			Item.NewItem(npc.getRect(), ItemID.Hellstone, Main.rand.Next(1, 3));
			if (Main.rand.NextFloat() < .20f)
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}
	}
}