using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Minions.Lai;


namespace TerraStory.Items.Weapons.Minions
{
	public class MapleScepter : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Maple scepter");
			Tooltip.SetDefault("Summons lai, the precious Beast tamer's companion!\n" +
				"Count as a sentry. but don't think you can tame him yet!\n" +
				"he will move and target what he want!");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true;
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 28;
			item.value = Item.buyPrice(gold: 10);
			item.rare = ItemRarityID.LightRed;
			item.mana = 100;
			item.damage = 20;
			item.knockBack = 1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 30;
			item.useAnimation = 30;
			item.summon = true;
			item.noMelee = true;
			item.shoot = ModContent.ProjectileType<Lai>();
			item.UseSound = SoundID.Item44;
		}

		public override bool CanUseItem(Player player)
		{
			player.FindSentryRestingSpot(item.shoot, out int worldX, out int worldY, out _);
			worldX /= 16;
			worldY /= 16;
			worldY--;
			return !WorldGen.SolidTile(worldX, worldY);
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.FindSentryRestingSpot(type, out int worldX, out int worldY, out int pushYUp);
			Projectile.NewProjectile(worldX, worldY - pushYUp, speedX, speedY, type, damage, knockBack, player.whoAmI);
			player.UpdateMaxTurrets();
			return false;
		}
	}
}