using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Tiles.Plants;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable
{
    public class CottonPlantBoxItem: ModItem
    {
        public static bool isCraftable;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cotton plantbox");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 24;
            item.height = 20;
            item.value = 100;
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.consumable = true;
            item.createTile = ModContent.TileType<CottonPlantBoxTile>();
        }

        public override void AddRecipes()
        {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ItemID.MudBlock, 2);
                recipe.AddIngredient(ItemID.RichMahogany, 2);
                recipe.AddIngredient(ModContent.ItemType<CottonPlantSeedItem>(), 2);
                recipe.AddTile(TileID.WorkBenches);
                recipe.SetResult(this);
                recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.DirtBlock, 2);
            recipe.AddIngredient(ItemID.Wood, 2);
            recipe.AddIngredient(ModContent.ItemType<CottonPlantSeedItem>(), 2);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}