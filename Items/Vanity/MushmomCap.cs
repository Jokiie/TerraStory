using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Vanity
{
	[AutoloadEquip(EquipType.Head)]
	public class MushmomCap : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Blue;
			item.defense = 13;
			item.vanity = true;
		}
	}
}