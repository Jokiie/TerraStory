using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class CrocoSkin : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 25);
			item.rare = ItemRarityID.Blue;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CrocoSkin>(), 12);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(ItemID.Leather, 3);
			recipe.AddRecipe();
		}
	}
}
