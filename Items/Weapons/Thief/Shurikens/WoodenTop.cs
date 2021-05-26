using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
	public class WoodenTop : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Throw a wooden spinning top\n" +
				"Who keeps spinning on the ground until it stops \n" +
				"maximum duration of 10 seconds");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.knockBack = 1.5f;
			item.shootSpeed = 7f;
			item.width = 18;
			item.height = 20;
			item.useTime = 14;
			item.useAnimation = 14;
			item.maxStack = 999;
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<WoodenTopP>();
			item.value = Item.sellPrice(copper: 5);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
        public override void AddRecipes()
		{

		}
	}
}