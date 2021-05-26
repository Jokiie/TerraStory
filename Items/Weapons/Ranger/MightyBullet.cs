using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class MightyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Each bullet hit up to 3 ennemies.");
		}

		public override void SetDefaults()
		{
			item.scale = 0.5f;
			item.width = 20;
			item.height = 20;
			item.damage = 11;
			item.knockBack = 3f;
			item.shootSpeed = 6.6f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 40);
			item.rare = ItemRarityID.White;
			item.shoot= mod.ProjectileType("MightyBullet");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddTile(TileID.Bowls);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}