using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Magic;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.NinjaClaw
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class NinjaClaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Ninja Claw");
			Tooltip.SetDefault("Turns your shurikens in dark throwing stars \n" +
				"which shatter into 2 smaller shurikens.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 8;
			item.knockBack = 1f;
			item.shootSpeed = 6.6f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 50, 0);
			item.rare = ItemRarityID.Orange;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Tglove2");
			item.shoot = ProjectileID.Shuriken;
			item.useAmmo = ItemID.Shuriken;
			item.handOnSlot = 11;
			item.autoReuse = false;
			item.noUseGraphic = true;
			item.noMelee = false;
			item.thrown = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileType<SubiP>() || type == ProjectileType<WolbiP>() || type == ProjectileType<MokbiP>() || type == ProjectileType<TobiP>() || type == ProjectileType<KumbiP>() || type == ProjectileType<IlbiP>() || type == ProjectileType<BalancedFuryP>() || type == ProjectileType<HwabiP>())
			{
				type = ProjectileType<NinjaShuriken>();
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			//Projectile.NewProjectile(position.X, position.Y, speedX + (Main.rand.Next(200) / 100), speedY + (Main.rand.Next(200) / 100), type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;

		}
	}
}