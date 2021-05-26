using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class MapleThrowingStar : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}

		public override void SetDefaults()
		{
			item.damage = 25;
			item.knockBack = 1.5f;
			item.shootSpeed = 7f;
			item.width = 20;
			item.height = 20;
			item.useTime = 14;
			item.useAnimation = 14;
			item.maxStack = 999;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ProjectileType<MapleThrowingStarP>();
			item.value = Item.sellPrice(silver: 75);
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