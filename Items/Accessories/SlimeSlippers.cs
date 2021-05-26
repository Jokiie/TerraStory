using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Accessories
{
	public class SlimeSlippers : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slime slippers");
			Tooltip.SetDefault("Increase your movement speed by 10% \n" +
				"and your summon damage by 5%.");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 0, 2, 50);
			item.rare = ItemRarityID.Blue;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.moveSpeed += 10f;
			player.minionDamage += 0.5f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Leather, 1);
			recipe.AddIngredient(ModContent.ItemType<Items.Ect.SquishyLiquid>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}
