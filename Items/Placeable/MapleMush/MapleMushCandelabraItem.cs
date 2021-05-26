
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushCandelabraItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 22;
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
			item.createTile = mod.TileType("MapleMushCandelabra");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom Candelabra");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 5);
			recipe.AddIngredient(ItemID.Torch, 3);
			recipe.SetResult(this);
			recipe.AddTile(TileID.WorkBenches);
			recipe.AddRecipe();
		}
	}
}