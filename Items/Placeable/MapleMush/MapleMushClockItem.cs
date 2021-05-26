using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushClockItem : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Maple mushroom Clock");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 22;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 500;
			item.createTile = TileType<Tiles.Furnitures.MapleMush.MapleMushClock>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddIngredient(ItemID.IronBar, 3);
			recipe.AddIngredient(ItemType<MapleMushroom>(), 10);
			recipe.AddTile(TileID.Sawmill);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Glass, 6);
			recipe.AddIngredient(ItemID.LeadBar, 3);
			recipe.AddIngredient(ItemType<MapleMushroom>(), 10);
			recipe.AddTile(TileID.Sawmill);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}