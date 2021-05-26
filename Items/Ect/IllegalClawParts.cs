using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class IllegalClawParts : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Obtained from Cygnus Knight, during the night and after squeletron \n" +
				"has been defeated.");
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 35, 0, 0);
			item.rare = ItemRarityID.Cyan;
			item.material = true;
		}
	}
}
