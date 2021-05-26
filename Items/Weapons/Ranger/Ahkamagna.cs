using Terraria;
using Terraria.ID;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
	public class Ahkamagna : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.scale = 1f;
			item.width = 12;
			item.height = 28;
			item.damage = 18;
			item.knockBack = 0f;
			item.useTime = 28;
			item.useAnimation = 28;
			item.shootSpeed = 6.6f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder;
			item.useAmmo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Amber, 8);
			recipe.AddIngredient(ItemID.AntlionMandible, 8);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
