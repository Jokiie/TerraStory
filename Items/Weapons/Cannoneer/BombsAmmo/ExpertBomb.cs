using TerraStory.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using TerraStory.Buffs;
using TerraStory.Projectiles.Cannoneer;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Weapons.Cannoneer.BombsAmmo
{
	public class ExpertBomb : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("This bomb explose on impact, damaging ennemies and \n" +
				"releasing 3 explosives fragments!");
		}

		public override void SafeSetDefaults()
		{
			item.scale = 1f;
			item.damage = 15;
			item.crit = 4;
			item.width = 18;
			item.height = 17;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.shootSpeed = 12f;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.rare = ItemRarityID.White;
			item.shoot = ProjectileType<ExpertBombProj>();
			item.ammo = ItemID.Bomb;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 30);
			recipe.AddIngredient(ItemID.GoldBar, 2);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<GunPowder>(), 30);
			recipe.AddIngredient(ItemID.PlatinumBar, 2);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}
