using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items
{
	public class RockOfEvolution : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("This strange rock have certainly an use.");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = ItemRarityID.Cyan;
			item.material = true;
		}
	}
}
