using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class BoneThread : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A Thread made from the fibers in the bones.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 20, 0);
			item.rare = ItemRarityID.Green;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 1);
			recipe.AddIngredient(ItemID.Bone, 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}

