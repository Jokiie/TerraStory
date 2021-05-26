using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
	public class IronArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.width = 7;
			item.height = 38;
			item.scale = 0.90f;
			item.damage = 8;
			item.knockBack = 2f;
			item.shootSpeed = 3.5f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 20);
			item.rare = ItemRarityID.White;
			item.shoot = mod.ProjectileType("IronArrowP");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 1);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 70);
			recipe.AddRecipe();
		}
	}
}