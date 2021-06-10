using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Placeable;

namespace TerraStory.Items.Ect
{
	public class LinenBall : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A linen fabric.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 5);
			item.rare = ItemRarityID.Blue;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Hay, 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this, 3);
			recipe.AddRecipe();

		}
	}
}