using Microsoft.Xna.Framework;
using TerraStory.Projectiles.Movement;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Buffs;

namespace TerraStory.Items.DoubleJumps
{
	public class WarriorLeap : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Warrior Leap");
			Tooltip.SetDefault("Leap like a warrior!");
		}

		public override void SetDefaults()
		{
			item.width = 44;
			item.height = 48;
			item.useTime = 100;
			item.useAnimation = 100;
			item.useStyle = ItemUseStyleID.HoldingOut;
			Item.staff[item.type] = true;
			item.noMelee = true;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item20;
			item.autoReuse = false;
			item.shoot = ProjectileType<DoubleJump>();
			item.shootSpeed = 12f;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (!player.HasBuff(BuffID.Featherfall))
			{
				player.AddBuff(ModContent.BuffType<LeapBuff>(), 60);
				player.velocity.X = 0 - speedX;
				player.velocity.Y = 0 - speedY;
				int ing = Gore.NewGore(player.Center, player.velocity * 4, 825);
				Main.gore[ing].timeLeft = Main.rand.Next(30, 90);
				int ing1 = Gore.NewGore(player.Center, player.velocity * 4, 826);
				Main.gore[ing1].timeLeft = Main.rand.Next(30, 90);
				int ing2 = Gore.NewGore(player.Center, player.velocity * 4, 827);
				Main.gore[ing2].timeLeft = Main.rand.Next(30, 90);
			}

			return false;
		}

	}
}