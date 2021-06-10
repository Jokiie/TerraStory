using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class BlinkrootThread : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Thread made from the fibers in the blinkroot");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 30);
			item.rare = ItemRarityID.Green;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Blinkroot, 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this, 3);
			recipe.AddRecipe();

		}
	}
}

