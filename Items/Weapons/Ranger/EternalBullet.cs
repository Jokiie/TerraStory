using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class EternalBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Each bullet hit up to 10 ennemies.");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 3f;
			item.value = 10;
			item.rare = ItemRarityID.Cyan;
			item.shoot= mod.ProjectileType("EternalBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 11f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<ShinyBullet>(), 100);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}