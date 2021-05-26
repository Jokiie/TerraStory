﻿using Terraria.ID;
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
	public class GrandCanon : CannonDamageItem
	{
		// Our ExampleDamageItem abstract class handles all code related to our custom damage class
		public override void SafeSetDefaults()
		{
			item.damage = 11;
			item.crit = 4;
			item.width = 60;
			item.height = 30;
			item.useTime = 37;
			item.useAnimation = 37;
			item.shootSpeed = 6.6f;
			item.knockBack = 1f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(0, 0, 3, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Cannon2");
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = ProjectileID.Bomb;
			item.useAmmo = ItemID.Bomb;
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
	}
}