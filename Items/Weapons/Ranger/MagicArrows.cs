using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Weapons.Ranger
{
    public class MagicArrows : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Magic Arrow");
			Tooltip.SetDefault(" It's magic!");
		}

		public override void SetDefaults()
		{
			item.width = 7;
			item.height = 38;
			item.scale = 0.90f;
			item.damage = 12;
			item.knockBack = 2f;
			item.shootSpeed = 8f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 30);
			item.rare = ItemRarityID.Orange;
			item.shoot= mod.ProjectileType("MagicArrows");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LusterPixieSunPiece>(), 1);
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}

