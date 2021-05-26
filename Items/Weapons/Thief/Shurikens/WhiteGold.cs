using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class WhiteGold : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("WhiteGold Throwing Stars");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.knockBack = 1.5f;
			item.shootSpeed = 7f;
			item.width = 18;
			item.height = 20;
			item.useTime = 14;
			item.useAnimation = 14;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<WhiteGoldP>();
			item.value = Item.sellPrice(copper: 5);
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