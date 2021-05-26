using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
	public class Hwabi : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}

		public override void SetDefaults()
		{
			item.damage = 30;
			item.knockBack = 2.3f;
			item.shootSpeed = 10f;
			item.scale = 2f;
			item.width = 20;
			item.height = 20;
			item.useTime = 11;
			item.useAnimation = 12;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<HwabiP>();
			item.value = Item.sellPrice(gold: 5);
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