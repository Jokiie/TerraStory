using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class MapleGun : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoot maple bullets that explose on hit\n" +
				"in mini maple leaves");
		}

		public override void SetDefaults()
		{
			item.scale = 1f;
			item.damage = 30;
			item.ranged = true;
			item.noMelee = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 23;
			item.useAnimation = 23;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 0.4f;
			item.value = 10000;
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item11;
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 6f;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}