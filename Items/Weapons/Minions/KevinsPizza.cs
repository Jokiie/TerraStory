using TerraStory.Projectiles.Minions.KevinTheCutePig;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Buffs;

namespace TerraStory.Items.Weapons.Minions
{
	public class KevinsPizza : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kevin's pizza");
			Tooltip.SetDefault("Summons Kevin, the cute pig to fight for you!");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.knockBack = 2f;
			item.mana = 5;
			item.width = 26;
			item.height = 26;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 0, 10, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<KevinTheCutePigBuff>();
			item.shoot = ProjectileType<KevinTheCutePig>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.AddBuff(item.buffType, 2);
			position = Main.MouseScreen;
			return true;
		}
	}
}