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
	public class Infernalizer : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Your bombs set the ennemies in fire! \n" +
				"Uses cannoneer bombs.");
		}
		public override void SafeSetDefaults()
		{
			item.damage = 30;
			item.crit = 4;
			item.width = 60;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.shootSpeed = 8f;
			item.knockBack = 3f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(0, 3, 55, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Cannon2");
			item.noMelee = true;
			item.autoReuse = true;
			item.shoot = ProjectileID.Bomb;
			item.useAmmo = ItemID.Bomb;
		}
        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
			target.AddBuff(BuffID.OnFire, 120, true);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.velocity.X = 0 - speedX / 6;
			player.velocity.Y = 0 - speedY / 6;
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
			recipe.AddIngredient(ItemID.HellstoneBar, 15);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}