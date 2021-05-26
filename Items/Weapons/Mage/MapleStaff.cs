using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Projectiles.Magic;
using Microsoft.Xna.Framework;

namespace TerraStory.Items.Weapons.Mage
{
	public class MapleStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots enchanted maple leaves");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.scale = 1.2f;
			item.damage = 40;
			item.mana = 10;
			item.shootSpeed = 10f;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 1f;
			item.value = Item.sellPrice(silver: 50) ;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item20;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
			item.shoot = ProjectileType<MapleLeavesP>();
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//this defines how many projectiles to shot
			float numberProjectiles = 8;
			float rotation = MathHelper.ToRadians(45);
			// this defines the distance of the projectiles from the player when its created
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				// This defines the projectile rotation and speed. .4f == projectile speed
				Vector2 pertubedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .6f;
				Projectile.NewProjectile(position.X, position.Y, pertubedSpeed.X, pertubedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
	}
}