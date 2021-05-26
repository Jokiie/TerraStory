using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class Ryden : ModItem
	{
		public override void SetStaticDefaults() 
		{

		}

		public override void SetDefaults()
		{
			item.damage = 12;
			item.ranged = true;
			item.width = 12;
			item.height = 28;
			item.useTime = 26;
			item.useAnimation = 26;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 4;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item5;
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder; //idk why but all the guns in the vanilla source have this
			item.shootSpeed = 7f;
			item.useAmmo = AmmoID.Arrow;
		}
	}
}
