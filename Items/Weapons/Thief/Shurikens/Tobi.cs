using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
	public class Tobi : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.knockBack = 2f;
			item.shootSpeed = 7f;
			item.width = 20;
			item.height = 20;
			item.useTime = 15;
			item.useAnimation = 15;
			item.maxStack = 999;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<TobiP>();
			item.value = Item.sellPrice(silver: 1);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffType<TooSharp>(), 50);
		}
	}
}