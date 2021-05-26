using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items
{
	public class MapleLeaf : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("A simple maple leaf from Maple world.");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Blue;
			item.material = true;
		}
	}
}
