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

namespace TerraStory.NPCs.TownNPCs
{
    // [AutoloadHead] and npc.townNPC are extremely important and absolutely both necessary for any Town NPC to work at all.
    [AutoloadHead]
	public class CygnusKnight : ModNPC
	{
		public override string Texture => "TerraStory/NPCs/TownNPCs/CygnusKnight";


		public override bool Autoload(ref string name)
		{
			name = "Cygnus Knight";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults()
		{
			// DisplayName automatically assigned from .lang files, but the commented line below is the normal approach.
			// DisplayName.SetDefault("Example Person");
			Main.npcFrameCount[npc.type] = 24;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 5;
			NPCID.Sets.DangerDetectRange[npc.type] = 30;
			NPCID.Sets.AttackType[npc.type] = 3; // this defines the npc damage type, 0 (Throwing),1 (Shooting), 2 (magic), 3 (melee)
			NPCID.Sets.AttackTime[npc.type] = 30;
			NPCID.Sets.AttackAverageChance[npc.type] = 30;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults()
		{
			npc.townNPC = true;
			npc.scale = 0.50f;
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
			animationType = NPCID.TaxCollector;

		}
		public Player player;
		public override bool CanTownNPCSpawn(int numTownNPCs, int money)
		{
			return World.downedMano;
		}


		public override string TownNPCName()
		{
			switch (WorldGen.genRand.Next(4))
			{
				case 0:
					return "Keven";
				case 1:
					return "Zakiel";
				case 2:
					return "Kaeiv";
				case 3:
					return "Zekal";
				default:
					return "Eikha";
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
			button = "Buy supplies";

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
					return "I don't know why the monsters from MapleWorld came here.";
				case 1:
					return "Cygnus sent me to sell you some supplies from MapleWorld.This will help you fight against monsters you never saw before.";
				case 2:
					return "Our heroes from MapleWorld are busy fighting the Black Mage.I'm sorry,you will need to handle it yourself.";
				default:
					return "I wonder what the Hero Jokie think about me..";
			}
		}
		public override void SetupShop(Chest shop, ref int nextSlot)
		{
			if (World.downedMano)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Subi>());
				nextSlot++;
			}
			if (NPC.downedSlimeKing)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Wolbi>());
				nextSlot++;
			}
			if (NPC.downedBoss2)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Mokbi>());
				nextSlot++;
			}
			if (NPC.downedQueenBee)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Kumbi>());
				nextSlot++;
			}
			if (NPC.downedBoss3)
			{
				shop.item[nextSlot].SetDefaults(ItemType<Tobi>());
				nextSlot++;
			}
			if (!Main.dayTime || NPC.downedBoss3)
			{
				shop.item[nextSlot].SetDefaults(ItemType<IllegalClawParts>());
				nextSlot++;
			}
			if (Main.hardMode)
            {
				shop.item[nextSlot].SetDefaults(ItemType<Flame>());
				nextSlot++;
            }
			if (Main.hardMode)
			{
				shop.item[nextSlot].SetDefaults(ItemType<MapleBox>());
				shop.item[nextSlot].shopCustomPrice = 500;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			shop.item[nextSlot].SetDefaults(ItemType<SurprisePetBox>());
			shop.item[nextSlot].shopCustomPrice = 400;
			shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ItemType<WhiteCrusaderChainMail>());
			shop.item[nextSlot].shopCustomPrice = 10;
			shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID; // omit this line if shopCustomPrice should be in regular coins. 
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ItemType<JoustingHelmet>());
			shop.item[nextSlot].shopCustomPrice = 10;
			shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
			nextSlot++;

			shop.item[nextSlot].SetDefaults(ItemType<Ring>());
			shop.item[nextSlot].shopCustomPrice = 50;
			shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
			nextSlot++;

			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))
			{
				shop.item[nextSlot].SetDefaults(ItemType<GoldenRing>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<GoldenRing>()))
			{
				shop.item[nextSlot].SetDefaults(ItemType<MapleRing>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<MapleRing>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<MapleGoldenRing>());
				shop.item[nextSlot].shopCustomPrice = 150;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<SwordsEarrings>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<ShieldEarrings>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<StarEarrings>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<FeathersEarrings>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
			if (Main.LocalPlayer.HasItem(ItemType<Ring>()))

			{
				shop.item[nextSlot].SetDefaults(ItemType<WeightedEarrings>());
				shop.item[nextSlot].shopCustomPrice = 100;
				shop.item[nextSlot].shopSpecialCurrency = TerraStory.CustomCurrencyID;
				nextSlot++;
			}
		}
		public override void NPCLoot()
		{
			if (Main.rand.NextFloat() < .20f) // 20% chance
				Item.NewItem(npc.getRect(), ItemType<Items.Weapons.Warrior.CygnusKnightSpear>());
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
	    public override void HitEffect(int hitDirection, double damage)
	    {
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/CygnusKnightHead"), 1f);
			}
		}
	}
}
