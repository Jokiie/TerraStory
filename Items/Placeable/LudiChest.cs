using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Placeable
{
	public class LudiChest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ludibrium Chest");
			Tooltip.SetDefault("");
		}
		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 18;  // 18
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 15;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.rare = ItemRarityID.Blue;
			item.consumable = true;
			item.value = 2000;
			item.createTile = mod.TileType("LudiChestTile");
		}
	}
}