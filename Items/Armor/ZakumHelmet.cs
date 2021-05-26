using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Enums;
using TerraStory.Items.Accessories;
using TerraStory.Content.Players;

namespace TerraStory.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class ZakumHelmet: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("+15 STR \n" +
				"+15 DEX \n" +
				"+15 INT \n" +
				"+15 LUK");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(gold: 50);
			item.rare = ItemRarityID.Pink;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.GetModPlayer<PlayerCharacter>().TempStats[PlayerStats.STR] += 15;
			player.GetModPlayer<PlayerCharacter>().TempStats[PlayerStats.DEX] += 15;
			player.GetModPlayer<PlayerCharacter>().TempStats[PlayerStats.INT] += 15;
			player.GetModPlayer<PlayerCharacter>().TempStats[PlayerStats.LUK] += 15;
		}
		/*
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.BeeGreaves && body.type == ItemID.BeeBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.thrownCrit += 10;
			player.thrownVelocity += 0.10f;
			player.thrownCost33 = true;
			player.setBonus = "10% increased throwing critical strike chance \n" +
			"10% increased throwing velocity \n" +
			"33% less chance to consume thrown items";
		}*/
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RangerEmblem, 1);
			recipe.AddIngredient(ItemID.WarriorEmblem, 1);
			recipe.AddIngredient(ItemID.SorcererEmblem, 1);
			recipe.AddIngredient(ItemID.SummonerEmblem, 1);
			recipe.AddIngredient(ModContent.ItemType<ThiefEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CannoneerEmblem>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 100);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}