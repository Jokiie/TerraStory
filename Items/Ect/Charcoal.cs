using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class Charcoal : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Used to make gunpowder for cannonner's bombs.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 1, 0);
			item.rare = ItemRarityID.Blue;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddTile(TileID.Fireplace);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddTile(TileID.Furnaces);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddTile(TileID.Campfire);
			recipe.SetResult(this, 5);
			recipe.AddRecipe();
		}
	}
}
