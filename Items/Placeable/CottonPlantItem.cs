using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Placeable
{
    public class CottonPlantItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cotton plant");
            Tooltip.SetDefault("This plant can be woven with the loom.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 99;
            item.width = 14;
            item.height = 18;
            item.value = Item.sellPrice(copper: 20);
        }
    }
}