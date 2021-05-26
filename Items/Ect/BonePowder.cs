using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class BonePowder : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This powder is full of calcium, \n" +
				"but no one will recommend to consumme it.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.rare = ItemRarityID.Orange;
			item.material = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 5);
			recipe.AddIngredient(ItemID.Bone, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 20);
			recipe.AddRecipe();
		}
	}
}
