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
	public class DeadlyGrenade : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A deadly grenade that contains many, many other small grenades filled with \n" +
				"condensed cannon powder. No need to know how to aim to kill your target!");
		}

		public override void SafeSetDefaults()
		{
			item.scale = 0.60f;
			item.damage = 20;
			item.crit = 4;
			item.width = 31;
			item.height = 31;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.shootSpeed = 12f;
			item.value = Item.sellPrice(0, 0, 25, 0);
			item.rare = ItemRarityID.Orange;
			item.shoot = ProjectileType<DeadlyGrenadeProj>();
			item.ammo = ItemID.Bomb;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<HellPowder>(), 20);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddIngredient(ItemID.HellstoneBar, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 100);
			recipe.AddRecipe();
		}
	}
}