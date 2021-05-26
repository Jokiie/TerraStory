using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.GFX;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.NPCs.Ludibrium
{
	// This is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors.
	public class GiaganticSpiritViking : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Giagantic Spirit Viking"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 6;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 212;  // hitbox
			npc.height = 224;
			npc.scale = 0.70f;
			npc.aiStyle = 3;
			npc.damage = 90;
			npc.defense = 40;
			npc.lifeMax = 1000;
			npc.knockBackResist = 0.05f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/GiaganticSpiritVikingHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/GiaganticSpiritVikingDie");
			npc.value = Item.buyPrice(0, 10, 0, 0);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			MasksHelper.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/GlowMasks/GiaganticSpiritViking_Glow"));
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo) 
		{
			Player player = spawnInfo.player;
			return !Main.dayTime
			&& Main.hardMode
			&& player.ZoneRockLayerHeight
			&& player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium ? 2.09f : 0f;
		}
		
		public override void FindFrame(int frameHeight)
		{
		// This makes the sprite flip horizontally in conjunction with the npc.direction.
		npc.spriteDirection = npc.direction; 
		// Determines the animation speed . positive value ex: 0.5f = higher speed
		npc.frameCounter -= -5.9f;
		npc.frameCounter %= Main.npcFrameCount[npc.type];
		int frame = (int)npc.frameCounter;
		npc.frame.Y = frame * frameHeight;
		}

		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>(), 15 - 35);
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>(), 15 - 35);
			//if (Main.rand.NextFloat() < .50f) // 50% chance
			//	Item.NewItem(npc.getRect(), ItemType<Items.Placeable.PinkToyBlock>(), 1 - 5);
		}
	}
}