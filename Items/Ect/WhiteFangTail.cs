using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class WhiteFangTail : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It's so fluffy!");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 25);
			item.rare = ItemRarityID.White;
			item.material = true;
		}
	}
}
