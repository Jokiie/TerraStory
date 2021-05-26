using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Placeable;

namespace TerraStory.Items.Ect
{
	public class CottonBall : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A cotton fabric.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 10);
			item.rare = ItemRarityID.White;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonPlantItem>(), 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TeddysCotton>(), 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<TeddysCotton>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CottonPlantItem>(), 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LeattyFurball>(), 2);
			recipe.AddIngredient(ModContent.ItemType<CottonPlantItem>(), 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LeattyFurball>(), 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LeattyFurball>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CottonPlantItem>(), 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LeattyFurball>(), 2);
			recipe.AddIngredient(ModContent.ItemType<TeddysCotton>(), 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LeattyFurball>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TeddysCotton>(), 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
