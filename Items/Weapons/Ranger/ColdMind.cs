using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class ColdMind : ModItem
	{
		public override void SetStaticDefaults()
		{

		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 20;
			item.damage = 12;
			item.knockBack = 0f;
			item.useTime = 16;
			item.useAnimation = 16;
			item.shootSpeed = 6.6f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 5);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item11;
			item.shoot = ProjectileID.PurificationPowder;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}