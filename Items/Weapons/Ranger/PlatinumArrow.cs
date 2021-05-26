using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
	public class PlatinumArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.width = 7;
			item.height = 38;
			item.scale = 0.90f;
			item.damage = 11;
			item.knockBack = 3f;
			item.shootSpeed = 5f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 25);
			item.rare = ItemRarityID.White;
			item.shoot = mod.ProjectileType("PlatinumArrowP");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 1);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();
		}
	}
}