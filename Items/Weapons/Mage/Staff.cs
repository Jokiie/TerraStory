using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Mage
{
	public class Staff : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("First staff is always special.");
		}

		public override void SetDefaults() 
		{
			item.width = 40;
			item.height = 40;
			item.damage = 12;
			item.mana = 1;
			item.knockBack = 1f;
			item.shootSpeed = 6f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(copper: 5);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item20;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;
			Item.staff[item.type] = true;
			item.shoot = ProjectileType<Brilliance>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup(ItemID.Wood, 10);
			recipe.AddIngredient(ItemID.ManaCrystal);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}