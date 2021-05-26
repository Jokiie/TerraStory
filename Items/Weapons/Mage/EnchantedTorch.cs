using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Magic;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Mage
{
	public class EnchantedTorch : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Control an enchanted torch who light your way.\n" +
				"very low damage,but set ennemies in fire.");
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.damage = 2;
			item.mana = 3;
			item.knockBack = 0f;
			item.shootSpeed = 6f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(copper: 10);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item8;
			item.magic = true;
			item.noMelee = true;
			item.channel = true;
			Item.staff[item.type] = true;
			item.shoot = ProjectileType<EnchantedTorchP>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Torch, 15);
			recipe.AddIngredient(ItemID.FallenStar, 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}