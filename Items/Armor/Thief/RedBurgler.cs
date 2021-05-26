using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Head)]
	public class RedBurgler : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Burgler");
			Tooltip.SetDefault("Set with the 'KnuckleVest' overall of any colors.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(silver: 95);
			item.defense = 5;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.04f;
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
			recipe.AddIngredient(ItemID.Wood, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}