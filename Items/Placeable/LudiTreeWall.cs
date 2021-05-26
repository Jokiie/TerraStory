using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Placeable
{
	public class LudiTreeWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ludibrium green wall");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = WallType<Walls.LudiTreeWall>();
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<LudiTreeBlock>());
			recipe.SetResult(this, 4);
			recipe.AddRecipe();
		}
	}
}