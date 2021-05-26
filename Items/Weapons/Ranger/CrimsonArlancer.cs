using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class CrimsonArlancer : ModItem
	{
		public override void SetStaticDefaults() 
		{

		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 12;
			item.height = 28;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.alpha = 30;
			item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 6.7f;
			item.useAmmo = AmmoID.Arrow;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TendonBow, 1);
			recipe.AddIngredient(ItemID.TissueSample, 8);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 5);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
