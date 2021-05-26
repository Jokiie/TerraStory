using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable
{
	public class LudiTreeWorkbench : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Ludibrium green Workbench");
		}

		public override void SetDefaults() {
			item.width = 28;
			item.height = 14;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 150;
			item.createTile = TileType<Tiles.LudiTreeWorkbench>();
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.WorkBench);
			recipe.AddIngredient(ItemType<LudiTreeBlock>(), 10);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}