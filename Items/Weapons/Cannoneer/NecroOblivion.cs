using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Cannoneer;
using TerraStory.Items.Weapons.Cannoneer.BombsAmmo;
using Terraria;
using TerraStory.Buffs;
using TerraStory;
using Microsoft.Xna.Framework;
using static Terraria.ModLoader.ModContent;
using TerraStory.Projectiles.Movement;

namespace TerraStory.Items.Weapons.Cannoneer
{
	public class NecroOblivion : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("" +
				"Shoots 2 others bombs in the same time! \n" +
				"Uses cannoneer bombs.");
		}
		public override void SafeSetDefaults()
		{
			item.damage = 24;
			item.crit = 4;
			item.width = 60;
			item.height = 30;
			item.useTime = 35;
			item.useAnimation = 35;
			item.shootSpeed = 7f;
			item.knockBack = 2f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(0, 2, 55, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Cannon2");
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = ProjectileID.Bomb;
			item.useAmmo = ItemID.Bomb;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Projectile.NewProjectile(position.X, position.Y, speedX * 3, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			Projectile.NewProjectile(position.X, position.Y, speedX * 2, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			player.velocity.X = 0 - speedX / 6;
			int ing = Gore.NewGore(player.Center, player.velocity * 4, 826);
			Main.gore[ing].timeLeft = Main.rand.Next(30, 90);
			int ing1 = Gore.NewGore(player.Center, player.velocity * 4, 827);
			Main.gore[ing1].timeLeft = Main.rand.Next(30, 90);
			int ing2 = Gore.NewGore(player.Center, player.velocity * 4, 825);
			Main.gore[ing2].timeLeft = Main.rand.Next(30, 90);

			return true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 20);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}