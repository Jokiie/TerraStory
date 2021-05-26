using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class Bullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("First bullet is always special.");
		}

		public override void SetDefaults()
		{
			item.width = 8;
			item.height = 8;
			item.scale = 0.5f;
			item.damage = 5;
			item.knockBack = 2f;
			item.shootSpeed = 4f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 10);
			item.rare = ItemRarityID.White;
			item.shoot= mod.ProjectileType("Bullet");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
