using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class MushmomSpore : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 20, 0);
			item.rare = ItemRarityID.Blue;
			item.material = true;
		}
	}
}
