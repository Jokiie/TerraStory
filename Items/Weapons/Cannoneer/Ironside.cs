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
	public class Ironside : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Uses cannoneer bombs.");
		}
		public override void SafeSetDefaults()
		{
			item.damage = 14;
			item.crit = 4;
			item.width = 60;
			item.height = 30;
			item.useTime = 38;
			item.useAnimation = 38;
			item.shootSpeed = 6.6f;
			item.knockBack = 0.5f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(0, 0, 2, 80);
			item.rare = ItemRarityID.White;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Cannon2");
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = ProjectileID.Bomb;
			item.useAmmo = ItemID.Bomb;
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.velocity.X = 0 - speedX / 8;
			player.velocity.Y = 0 - speedY / 8;
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
			recipe.AddIngredient(ItemID.LeadBar, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

		}
	}
}