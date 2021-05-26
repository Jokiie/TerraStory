using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Tiles.Furnitures.MapleMush;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushBookcaseItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 42;
			item.height = 16;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.value = 2000;
			item.createTile = ModContent.TileType<MapleMushBookcase>();
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom bookcase");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 20);
			recipe.AddIngredient(ItemID.Book, 10);
			recipe.SetResult(this);
			recipe.AddTile(TileID.Sawmill);
			recipe.AddRecipe();
		}
	}
}