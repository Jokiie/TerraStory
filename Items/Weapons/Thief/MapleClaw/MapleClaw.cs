using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.MapleClaw
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class MapleClaw : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Maple Claw");
			Tooltip.SetDefault("Transforms your shurikens into maple shurikens \n" +
				"which shatter into smaller shurikens who do some damage.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 30;
			item.knockBack = 3f;
			item.shootSpeed = 8f;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Tglove2");
			item.shoot = ProjectileID.Shuriken;
			item.useAmmo = ItemID.Shuriken;
			item.handOnSlot = 11;
			item.autoReuse = true;
			item.noUseGraphic = true;
			item.noMelee = false;
			item.thrown = true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == ProjectileType<SubiP>() || type == ProjectileType<WolbiP>() || type == ProjectileType<MokbiP>() || type == ProjectileType<TobiP>() || type == ProjectileType<KumbiP>() || type == ProjectileType<IlbiP>() || type == ProjectileType<BalancedFuryP>() || type == ProjectileType<HwabiP>())
			{
				type = ModContent.ProjectileType<MapleShurikenP>();
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			//Projectile.NewProjectile(position.X, position.Y, speedX + (Main.rand.Next(200) / 100), speedY + (Main.rand.Next(200) / 100), type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;

		}
	}
}