using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Accessories
{
	public class CannoneerEmblem : CannonDamageItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ninja belt");
			Tooltip.SetDefault("15% increased cannon damage.");
		}
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.15f;
		}
	}
}