using System;
using TerraStory.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
	public class GreenMapleSword : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots green maple leaves!");
		}

		public override void SetDefaults()
		{
			item.damage = 42;
			item.scale = 1f;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 25;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = Item.sellPrice(0, 5, 0, 10);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			item.shoot = ProjectileType<GreenMapleLeaf>();
			item.shootSpeed = 6f;
		}
		// even spread projectiles
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			//this defines how many projectiles to shot
			float numberProjectiles = 6;
			float rotation = MathHelper.ToRadians(45);
			// this defines the distance of the projectiles from the player when its created
			position += Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
			for (int i = 0; i < numberProjectiles; i++)
			{
				// This defines the projectile rotation and speed. .4f == projectile speed
				Vector2 pertubedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .4f;
				Projectile.NewProjectile(position.X, position.Y, pertubedSpeed.X, pertubedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}
    }
}