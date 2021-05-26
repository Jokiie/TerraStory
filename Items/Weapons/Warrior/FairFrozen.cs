using TerraStory.Projectiles.Warrior;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
	public class FairFrozen : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("It's so sharp and so cold in the same time...");
		}

		public override void SetDefaults()
		{
			item.width = 32;
			item.height = 32;
			item.scale = 1f;
			item.damage = 22;
			item.useAnimation = 18;
			item.useTime = 24;
			item.shootSpeed = 6f;
			item.knockBack = 3f;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(gold: 1);
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.melee = true;
			item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
			item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
			item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()
			item.shoot = ProjectileType<FairFrozenP>();
		}

		public override bool CanUseItem(Player player)
		{
			// Ensures no more than one spear can be thrown out, use this when using autoReuse
			return player.ownedProjectileCounts[item.shoot] < 1;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.ShadowScale, 10);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddIngredient(ItemID.IceBlock, 50);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}