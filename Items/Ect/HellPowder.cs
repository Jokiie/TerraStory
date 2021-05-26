using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class HellPowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This powder smell like misery, madness and death.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 15, 0);
			item.rare = ItemRarityID.Orange;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Charcoal>(), 5);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 5);
			recipe.AddIngredient(ItemID.Hellstone, 5);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 20);
			recipe.AddRecipe();
		}
	}
}
