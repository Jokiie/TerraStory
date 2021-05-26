
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Warrior
{
	public class CrimsonSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blood fury Desperado");
			Tooltip.SetDefault("Shoots 3 desperado on your foes.");
		}
		public override void SetDefaults()
		{
			item.width = 14;
			item.height = 28;
			item.damage = 20;
			item.knockBack = 5f;
			item.shootSpeed = 6f;
			item.useAnimation = 15;
			item.useTime = 15;
			item.rare = ItemRarityID.Green;
			item.value = Item.sellPrice(0, 0, 30, 0);
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = SoundID.Item1;
			item.shoot = mod.ProjectileType("CrimsonSwordP");
			item.melee = true;
			item.noMelee = true;
			item.autoReuse = true;
			item.noUseGraphic = true;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			for (int i = 0; i < 1; ++i) // Will shoot 3 projectiles.
			{
				Projectile.NewProjectile(position.X, position.Y, speedX + 3, speedY + 3, type, damage, knockBack, Main.myPlayer);
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, Main.myPlayer);
				Projectile.NewProjectile(position.X, position.Y, speedX - 3, speedY - 3, type, damage, knockBack, Main.myPlayer);
			}
			return false;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddIngredient(ItemID.CrimtaneBar, 3);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}