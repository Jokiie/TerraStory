using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class InfiniteThrowingKnife: ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing knife. \n" +
				"Inflict a very deep cut that's last 6 seconds!");
		}
		public override void SetDefaults()
		{
			item.damage = 35;
			item.knockBack = 3f;
			item.shootSpeed = 13f;
			item.width = 20;
			item.height = 20;
			item.useTime = 12;
			item.useAnimation = 12;
			item.maxStack = 999;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ProjectileType<InfiniteThrowingKnifeP>();
			item.value = Item.buyPrice(gold: 1);
			item.value = Item.sellPrice(silver: 50);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}

		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffType<TooSharp>(), 360 );
		}
	}
}