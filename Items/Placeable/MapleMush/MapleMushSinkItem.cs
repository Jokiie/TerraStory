using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushSinkItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 30;
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
			item.createTile = mod.TileType("MapleMushSink");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom Sink");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 6);
			recipe.AddIngredient(ItemID.WaterBucket);
			recipe.SetResult(this);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
		}
	}
}