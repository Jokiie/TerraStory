using TerraStory.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using TerraStory.Buffs;
using TerraStory.Projectiles.Cannoneer;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Weapons.Cannoneer.BombsAmmo
{
	public class NoviceBomb : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Compressed gas bomb");
			Tooltip.SetDefault("A cheap bomb for a novice cannoneer!");
		}

		public override void SafeSetDefaults()
		{
			item.scale = 0.60f;
			item.damage = 4;
			item.crit = 4;
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.shootSpeed = 12f;
			item.value = Item.sellPrice(0, 0, 1, 0);
			item.rare = ItemRarityID.White;
			item.shoot = ProjectileType<NoviceBombProj>();
			item.ammo = ItemID.Bomb;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemID.TinBar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemID.CopperBar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}