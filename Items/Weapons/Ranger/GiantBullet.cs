using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class GiantBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("The biggest bullet in Maple World. \n" +
				"Each bullet can hit up to ... 100 ennemies! \n" +
				"You really don't want to be hit by that.");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.ranged = true;
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 10f;
			item.value = 10;
			item.rare = ItemRarityID.Cyan;
			item.shoot= mod.ProjectileType("GiantBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 15f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<EternalBullet>(), 100);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.AdamantiteForge);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}