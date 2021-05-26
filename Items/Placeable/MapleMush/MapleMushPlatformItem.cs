using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TerraStory.Tiles.Furnitures.MapleMush;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushPlatformItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom platform");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 10;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<MapleMushPlatform>();
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleMushroom>());
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
}