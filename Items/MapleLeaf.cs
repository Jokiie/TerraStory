using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items
{
	public class MapleLeaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A precious maple leaf. \n" +
				"Can be used to craft various items from MapleWorld");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Orange;
			item.material = true;
		}
	}
}
