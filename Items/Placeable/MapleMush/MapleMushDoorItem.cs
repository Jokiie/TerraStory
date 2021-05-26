using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushDoorItem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom door");
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			item.width = 14;
			item.height = 28;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.consumable = true;
			item.value = 150;
			item.createTile = TileType<Tiles.Furnitures.MapleMush.MapleMushClosedDoor>();
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleMushroom>(), 6);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}