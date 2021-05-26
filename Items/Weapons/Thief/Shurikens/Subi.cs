using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
	public class Subi : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.knockBack = 2f;
			item.shootSpeed = 6f;
			item.width = 20;
			item.height = 20;
			item.useTime = 25;
			item.useAnimation = 25;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ProjectileType<SubiP>();
			item.value = Item.sellPrice(copper: 2);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
        public override void OnConsumeItem(Player player)
        {
			player.AddBuff(BuffType<TooSharp>(), 50);
        }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinBar, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}