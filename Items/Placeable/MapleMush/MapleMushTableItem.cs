using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushTableItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 48;
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
			item.createTile = mod.TileType("MapleMushTable");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom Table");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}