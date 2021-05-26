using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class Snowball : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Snow ball");
		}

		public override void SetDefaults()
		{
			item.damage = 15;
			item.knockBack = 1f;
			item.shootSpeed = 6f;
			item.width = 20;
			item.height = 20;
			item.useTime = 15;
			item.useAnimation = 15;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<SnowballP>();
			item.value = Item.sellPrice(copper: 5);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SnowBlock, 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}