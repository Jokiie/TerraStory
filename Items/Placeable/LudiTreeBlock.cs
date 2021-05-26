using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable
{
    public class LudiTreeBlock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Ludibrium wall");
        }
        public override void SetDefaults()
        {
            item.width = 12;
            item.value = 1;
            item.height = 12;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.rare = ItemRarityID.Blue;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.consumable = true;
            item.createTile = mod.TileType("LudiTreeTile");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemType<LudiTreeWall>(), 4);
            recipe.SetResult(this, 1);
            recipe.AddRecipe();
        }
    }
}