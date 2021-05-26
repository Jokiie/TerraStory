using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
	public class CrimsonMagicArrows : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Crimson Magic Arrow");
			Tooltip.SetDefault("Ooooh! It's red!");
		}

		public override void SetDefaults() 
		{
			item.width = 7;
			item.height = 38;
			item.scale = 0.80f;
			item.damage = 13;
			item.knockBack = 3f;
			item.shootSpeed = 6.6f;
			item.maxStack = 999;
			item.value = Item.buyPrice(copper: 50);
			item.rare = ItemRarityID.Green;
			item.shoot= mod.ProjectileType("CrimsonMagicArrows");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddIngredient(ItemID.FallenStar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}