using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushLanternItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 16;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.value = 2000;
			item.createTile = mod.TileType("MapleMushLantern");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom lantern");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 6);
			recipe.AddIngredient(ItemID.Torch, 1);
			recipe.SetResult(this);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
		}
	}
}