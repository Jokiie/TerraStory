using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Accessories
{
    public class CannonOil : CannonDamageItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cannon Oil");
			Tooltip.SetDefault("Improve the perfomance of cannons! \n" +
				"10% increased cannon damage and critical rate chance.");
		}
		public override void SafeSetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.10f;
			modPlayer.cannonCrit += 10;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<SquishyLiquid>(), 5);
			recipe.AddIngredient(ModContent.ItemType<YellowSquishyLiquid>(), 5);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemID.BottledWater);
			recipe.AddTile(TileID.Bottles);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
