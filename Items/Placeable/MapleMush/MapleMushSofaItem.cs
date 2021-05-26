using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushSofaItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 42;
			item.height = 16;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.value = 2000;
			item.createTile = mod.TileType("MapleMushSofa");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom sofa");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 5);
			recipe.AddIngredient(ItemID.Silk, 2);
			recipe.AddTile(TileID.Sawmill);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}