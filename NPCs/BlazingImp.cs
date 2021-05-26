using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs
{
	public class BlazingImp : ModNPC
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blazing Imp");
			Main.npcFrameCount[npc.type] = 6;
		}

		public override void SetDefaults()
		{
			npc.width = 60;
			npc.height = 60;
			npc.scale = 0.60f;
			npc.aiStyle = 14;
			npc.damage = 80;
			npc.defense = 50;
			npc.lifeMax = 450;
			npc.knockBackResist = 0.25f;
			npc.npcSlots = 1f;
			npc.value = Item.buyPrice(0, 0, 15, 50);
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/ImpHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/ImpDie");
			npc.buffImmune[BuffID.OnFire] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.buffImmune[BuffID.CursedInferno] = true;
			npc.buffImmune[BuffID.ShadowFlame] = true;
			npc.lavaImmune = true;
			npc.netAlways = true;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			Item.NewItem(npc.getRect(), ItemID.Hellstone, Main.rand.Next(1, 3));
		}

		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			return Main.hardMode
			&& spawnInfo.player.ZoneUnderworldHeight ? 2.09f : 0f;
		}
		public override void DrawEffects(ref Color drawColor)
		{
			for (int l = 0; l < 2; l++)
			{
				int num20 = Dust.NewDust(new Vector2(npc.position.X - npc.velocity.X * 2f, npc.position.Y - 2f - npc.velocity.Y * 2f), npc.width, npc.height, 6, 0f, 0f, 90, default(Color), 2f);
				Main.dust[num20].noGravity = true;
				Main.dust[num20].noLight = true;
				Main.dust[num20].velocity.X -= npc.velocity.X * 0.5f;
				Main.dust[num20].velocity.Y -= npc.velocity.Y * 0.5f;
			}
		}
		public override void FindFrame(int frameHeight)
		{
			npc.spriteDirection = npc.direction;
			npc.frameCounter -= -5.9f;
			npc.frameCounter %= Main.npcFrameCount[npc.type];
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
	}
}