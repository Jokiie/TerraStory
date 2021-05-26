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
	public class GhostPirate : ModNPC
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Ghost Pirate"); // Automatic from .lang files
			Main.npcFrameCount[npc.type] = 6;
        }
		
		public override void SetDefaults() 
		{
			npc.width = 170;  // hitbox
			npc.height = 173;
			npc.scale = 0.70f;
			npc.aiStyle = 3;
			npc.damage = 80;
			npc.defense = 30;
			npc.lifeMax = 750;
			npc.knockBackResist = 0.10f;
			npc.value = 1f;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/GhostPirateHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/GhostPirateDie");
			npc.value = Item.buyPrice(0, 7, 0, 0);
			npc.buffImmune[BuffID.Poisoned] = true;
			npc.buffImmune[BuffID.Confused] = false;
			npc.netAlways = true;
			
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			MasksHelper.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/GlowMasks/GhostPirate_Glow"));
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
				Item.NewItem(npc.getRect(), ItemType<Items.BundleOfMesos>());
			if (Main.rand.NextFloat() < .50f) // 50% chance
				Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
			//if (Main.rand.NextFloat() < .50f) // 50% chance
			//	Item.NewItem(npc.getRect(), ItemType<Items.Placeable.PinkToyBlock>(), 1 - 5);
		}
	}
}