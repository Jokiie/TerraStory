using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Placeable
{
	public class OrangeLudiWall : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Orange Ludibrium wall");
		}

		public override void SetDefaults() {
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 7;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createWall = WallType<Walls.OrangeLudiWall>();
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<OrangeLudiBlock>());
			recipe.SetResult(this, 4);
			recipe.AddRecipe();
		}
	}
}