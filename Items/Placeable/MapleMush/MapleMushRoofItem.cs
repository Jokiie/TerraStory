using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushRoofItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom roof");
			ItemID.Sets.ExtractinatorMode[item.type] = item.type;
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 12;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.createTile = TileType<Tiles.Furnitures.MapleMush.MapleMushRoof>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleMushroom>(), 1);
			recipe.SetResult(this, 2);
			recipe.AddRecipe();
		}
	}
}