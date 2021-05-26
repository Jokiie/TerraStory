using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class ShinyBullet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Each bullet hit up to 5 ennemies. \n" +
				"Set your ennnemies in fire!");
		}

		public override void SetDefaults()
		{
			item.scale = 0.5f;
			item.width = 20;
			item.height = 20;
			item.damage = 18;
			item.knockBack = 4f;
			item.shootSpeed = 8f;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver : 20);
			item.rare = ItemRarityID.LightRed;
			item.shoot= mod.ProjectileType("ShinyBullet");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddIngredient(ItemType<GunPowder>(), 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}