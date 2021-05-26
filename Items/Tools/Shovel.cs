using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Tools
{
	public class Shovel : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Who ever thought of digging with a shovel ?Oh, wait...");
		}

		public override void SetDefaults()
		{
			item.damage = 6;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 13;
			item.useAnimation = 20;
			item.pick = 45;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 2;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 10);
			recipe.AddRecipeGroup("IronBar", 8);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}