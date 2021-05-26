using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
	public class Flame : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flame Throwing Star");
			Tooltip.SetDefault("Throwing Stars. \n" +
				"Set your ennemies in fire for 3 seconds!");
		}

		public override void SetDefaults()
		{
			item.damage = 25;
			item.knockBack = 3f;
			item.shootSpeed = 8f;
			item.width = 20;
			item.height = 20;
			item.useTime = 12;
			item.useAnimation = 12;
			item.maxStack = 999;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<FlameP>();
			item.value = Item.sellPrice(silver: 25);
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
			recipe.AddIngredient(ItemID.HellstoneBar, 5);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}