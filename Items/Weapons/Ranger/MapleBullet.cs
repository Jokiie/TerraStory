using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class MapleBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Each arrow hit up to 3 enemies");
		}

		public override void SetDefaults()
		{
			item.damage = 7;
			item.ranged = true;
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.consumable = true;             //You need to set the item consumable so that the ammo would automatically consumed
			item.knockBack = 3f;
			item.value = 10;
			item.rare = ItemRarityID.LightRed;
			item.shoot= mod.ProjectileType("MapleBullet");   //The projectile shoot when your weapon using this ammo
			item.shootSpeed = 8.5f;                  //The speed of the projectile
			item.ammo = AmmoID.Bullet;              //The ammo class this ammo belongs to.
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<Bullet>(), 70);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();
		}
	}
}