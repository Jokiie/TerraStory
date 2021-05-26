using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
	public class Gun : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("First gun is always special");
		}

		public override void SetDefaults()
		{
			item.damage = 4;
			item.scale = 0.60f;
			item.ranged = true;
			item.noMelee = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 25;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 1;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item11;
			item.autoReuse = true;
			item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Bullet;
		}
	}
}
