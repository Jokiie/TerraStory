using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Accessories
{
	public class AvengerQuiver : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Avenger Quiver");
			Tooltip.SetDefault("Not moving increase your \n" +
				"ranged damage and critical chance rate by 8%");
		}
		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 30;
			item.value = Item.buyPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.Green;
			item.accessory = true;
		}
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.velocity.X == 0 || player.velocity.Y == 10)
			{
				player.GetModPlayer<TerraStoryPlayer>().BoneQuiverBuff = true;
			}
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 20);
			recipe.AddIngredient(ItemID.BlackDye, 1);
			recipe.AddIngredient(ModContent.ItemType<BoneThread>(), 50);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();

		}
	}
}