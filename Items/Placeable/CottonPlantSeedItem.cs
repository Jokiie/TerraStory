using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Tiles.Plants;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable
{
    public class CottonPlantSeedItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cotton plant seeds");
            Tooltip.SetDefault("Cotton plants grow in a tropical climate. \n" +
                "But they can survive in a temperate climate if it doesn't freeze.");
        }

        public override void SetDefaults()
        {
            item.autoReuse = true;
            item.useTurn = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 10;
            item.maxStack = 99;
            item.consumable = true;
            item.width = 12;
            item.height = 14;
            item.value = 80;
            item.createTile = ModContent.TileType<CottonPlantTile>();
        }
    }
}