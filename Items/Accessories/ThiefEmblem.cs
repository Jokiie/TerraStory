using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Accessories
{
	public class ThiefEmblem : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
			Tooltip.SetDefault("15% increased thrown dammage.");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.thrownDamage += 0.15f;
		}
	}
}