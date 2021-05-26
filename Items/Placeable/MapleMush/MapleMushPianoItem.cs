using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Placeable.MapleMush
{
	public class MapleMushPianoItem : ModItem
	{
		public override void SetDefaults()
		{

			item.width = 62;
			item.height = 32;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.value = 0;
			item.createTile = mod.TileType("MapleMushPiano");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple mushroom Piano");
			Tooltip.SetDefault("");
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 4);
			recipe.AddIngredient(ModContent.ItemType<MapleMushroom>(), 15);
			recipe.AddIngredient(ItemID.Book, 1);
			recipe.AddTile(TileID.Sawmill);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}