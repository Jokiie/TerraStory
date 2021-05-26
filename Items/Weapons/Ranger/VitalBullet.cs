using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class VitalBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Each bullet hit up to 4 ennemies.");
		}

		public override void SetDefaults()
		{
			item.scale = 0.5f;
			item.width = 20;
			item.height = 20;
			item.damage = 13;
			item.knockBack = 3f;
			item.shootSpeed = 6.6f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 70);
			item.rare = ItemRarityID.Blue;
			item.shoot= mod.ProjectileType("VitalBullet");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}