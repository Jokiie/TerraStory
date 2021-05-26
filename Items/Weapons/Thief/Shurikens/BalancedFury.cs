using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.ShurikensProj;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class BalancedFury : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throwing Stars.");
		}
		public override void SetDefaults()
		{
			item.damage = 50;
			item.knockBack = 2.3f;
			item.shootSpeed = 20f;
			item.width = 20;
			item.height = 20;
			item.useTime = 11;
			item.useAnimation = 12;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<BalancedFuryP>();
			item.value = Item.sellPrice(copper: 5);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
	}
}