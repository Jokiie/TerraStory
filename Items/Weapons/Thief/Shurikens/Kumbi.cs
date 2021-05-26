using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class Kumbi: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}

		public override void SetDefaults()
		{
			item.damage = 17;
			item.knockBack = 1.5f;
			item.shootSpeed = 7f;
			item.width = 20;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<KumbiP>();
			item.value = Item.sellPrice(copper: 75);
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
			recipe.AddIngredient(ItemID.MeteoriteBar, 1);
			recipe.AddIngredient(ItemID.BlackDye);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}