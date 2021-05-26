using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerraStory.Items.Armor.Maple;
using TerraStory.Content.UI;
using TerraStory.Content.GFX;
using TerraStory.Items;
using TerraStory.Items.Accessories;
using static Terraria.ModLoader.ModContent;
using static TerraStory.Items.Accessories.Ring;
using static TerraStory.Items.Accessories.SlimeEarrings;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Items.Ect;
using TerraStory.Items.Boxes;
using TerraStory.Tiles;
using TerraStory.Tiles.Furnitures.MapleMush;
using TerraStory.Items.Placeable.MapleMush;
using TerraStory.Walls;

namespace TerraStory.NPCs.TownNPCs
{
    // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
	public class Cody : ModNPC
	{
		public override string Texture => "TerraStory/NPCs/TownNPCs/Cody";


		public override bool Autoload(ref string name)
		{
			name = "Cody";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			// DisplayName.SetDefault("Example Person");
			Main.npcFrameCount[npc.type] = 22;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 5;
			NPCID.Sets.DangerDetectRange[npc.type] = 10;
			NPCID.Sets.AttackType[npc.type] = 3; // this defines the npc damage type, 0 (Throwing),1 (Shooting), 2 (magic), 3 (melee)
			NPCID.Sets.AttackTime[npc.type] = 30;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.scale = 0.70f;
			npc.friendly = true;
			npc.width = 40;
			npc.height = 60;
			npc.aiStyle = 7;
			npc.damage = 10;
			npc.defense = 15;
			npc.lifeMax = 250;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = 369;

		}
		public Player player;
		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return World.rescuedCody2;
		}
		
		public override bool CheckConditions(int left, int right, int top, int bottom)
		{
			int score = 0;
			for (int x = left; x <= right; x++)
			{
				for (int y = top; y <= bottom; y++)
				{
					int type = Main.tile[x, y].type;
					if (type == TileType<MapleMushTile>() || type == TileType<MapleMushChair>() || type == TileType<MapleMushWorkbench>() || type == TileType<MapleMushBed>() || type == TileType<MapleMushOpenDoor>() || type == TileType<MapleMushClosedDoor>() || type == TileType<MapleMushLamp>())
					{
						score++;
					}
					if (Main.tile[x, y].wall == WallType<MapleMushWallTile>())
					{
						score++;
					}
				}
			}
			return score >= (right - left) * (bottom - top) / 2;
		}
		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
				default:
					return "Cody";
			}
		}/*
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			Player player = Main.player[Main.myPlayer];
			var questData = player.GetModPlayer<QuestData>();

			if (questData.CurrentQuest == -1 && !questData.Completed)
			{
				if (GFX.QuestIcon2 == null) return;
				Vector2 origin = new Vector2(GFX.QuestIcon2.Width / 2, GFX.QuestIcon2.Height / 2);
				float y = 50.0f;
				Vector2 position = npc.Center - Main.screenPosition - new Vector2(0.0f, y);
				spriteBatch.Draw(GFX.QuestIcon2, position, null, Color.White, 0, origin, npc.scale * 1.5f, SpriteEffects.None, 0.0f);
			}
			if (questData.CurrentQuest >= 0 && questData.CurrentQuest != -1 && questData.GetCurrentQuest() is ItemQuest && player.CountItem((questData.GetCurrentQuest() as ItemQuest).ItemType, (questData.GetCurrentQuest() as ItemQuest).ItemAmount) >= (questData.GetCurrentQuest() as ItemQuest).ItemAmount)
			{
				int leftToRemove = (questData.GetCurrentQuest() as ItemQuest).ItemAmount;
				foreach (Item item in player.inventory)
				{
					if (item.type == (questData.GetCurrentQuest() as ItemQuest).ItemType)
					{
						if (GFX.QuestIcon3 == null) return;
						Vector2 origin = new Vector2(GFX.QuestIcon3.Width / 2 , GFX.QuestIcon3.Height / 2);
						float y = 50.0f;
						Vector2 position = npc.Center - Main.screenPosition - new Vector2(0.0f, y);
						spriteBatch.Draw(GFX.QuestIcon3, position, null, Color.White, 0, origin, npc.scale * 1.5f, SpriteEffects.None, 0.0f);
					}
				}
			}
			if (questData.CurrentQuest >= 0 && questData.CurrentQuest != -1 && questData.GetCurrentQuest() is HuntQuest && questData.QuestKills >= (questData.GetCurrentQuest() as HuntQuest).TargetCount)
			{
				if (GFX.QuestIcon3 == null) return;
				Vector2 origin = new Vector2(GFX.QuestIcon3.Width / 2, GFX.QuestIcon3.Height / 2);
				float y = 50.0f;
				Vector2 position = npc.Center - Main.screenPosition - new Vector2(0.0f, y);
				spriteBatch.Draw(GFX.QuestIcon3, position, null, Color.White, 0, origin, npc.scale, SpriteEffects.None, 0.0f);
			}
		}*/
		public override void SetChatButtons(ref string button, ref string button2)
		{
			button = "Buy";

		}

		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			Main.npcChatCornerItem = 0;
			if (firstButton)
			{
				shop = true;
			}
		}
		public override string GetChat()
		{
			switch (Main.rand.Next(4))
			{
				case 0:
					return "Bring me some lidium ore and I will craft you lots of nice equipments!";
				case 1:
					return "Im so exhausted after planting all these mushroom from Maple world!";
				case 2:
					return "If you craft more mushrooms furnitures for me, I'll craft you lot of nice things!";
				default:
					return "If you craft more mushrooms furnitures for me, I'll craft you lot of nice things!";
			}
		}
		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			if (World.downedBlueMushmom)
			{
				shop.item[nextSlot].SetDefaults(ItemType<MapleLeaf>());
				nextSlot++;
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
			    Item.NewItem(npc.getRect(), ItemType<Items.MapleLeaf>());
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback)
		{
			damage = 20;
			knockback = 0.4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
		{
			cooldown = 1;
			randExtraCooldown = 3;
		}

		public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
		{
			scale = 0.5f;
			item = Main.itemTexture[mod.ItemType("CygnusKnightSpear")];
			itemSize = 60;
		}

		public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
		{
			itemWidth = 40;
			itemHeight = 40;
		}
		/*
	    public override void HitEffect(int hitDirection, double damage)
	    {
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CygnusKnightHead"), 1f);
			}
		}*/
	}
}
