using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Magic;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Mage
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class MagicClaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Magic Claw");
			Tooltip.SetDefault("Summons a magic shuriken \n" +
				"which shatter into smaller shurikens when hitting an ennemy.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 24;
			item.knockBack = 3f;
			item.shootSpeed = 6.5f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 1, 5, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Tglove2");
			item.shoot = ModContent.ProjectileType<MagicShuriken>();
			item.handOnSlot = 11;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.noMelee = true;
			item.magic = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			type = ModContent.ProjectileType<MagicShuriken>();
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			//Projectile.NewProjectile(position.X, position.Y, speedX + (Main.rand.Next(200) / 100), speedY + (Main.rand.Next(200) / 100), type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 1);
			recipe.AddIngredient(ItemID.Silk, 10);
			recipe.AddIngredient(ModContent.ItemType<ManaInfusedThread>(), 3);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}