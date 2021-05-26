using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class SharpMagicArrows : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sharp Magic Arrow");
			Tooltip.SetDefault(" It's super magic!");
		}

		public override void SetDefaults()
		{
			item.scale = 0.90f;
			item.width = 45;
			item.height = 20;
			item.damage = 15;
			item.knockBack = 3f;
			item.shootSpeed = 9f;
			item.maxStack = 999;
			item.value = Item.sellPrice(copper: 70);
			item.rare = ItemRarityID.Orange;
			item.shoot= mod.ProjectileType("SharpMagicArrows");
			item.ranged = true;
			item.consumable = true;
			item.ammo = AmmoID.Arrow;              
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<LunarPixieMoonPiece>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ManaInfusedThread>(), 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}