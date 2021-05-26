using System;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Placeable;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class Lidium : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lidium");
			Tooltip.SetDefault("A complete Lidium , almost as perfect and powerful than the ones from Maple World.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 24;
			item.maxStack = 99;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.createTile = mod.TileType("Lidium");
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<LidiumOre>(), 4);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
