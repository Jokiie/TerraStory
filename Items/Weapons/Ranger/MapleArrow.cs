using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
    public class MapleArrow : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.damage = 20;
			item.ranged = true;
			item.width = 7;
			item.height = 38;
			item.scale = 0.80f;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 3f;
			item.value = 15;
			item.rare = ItemRarityID.LightRed;
			item.shoot = mod.ProjectileType("MapleArrow");
			item.shootSpeed = 6.1f;
			item.ammo = AmmoID.Arrow;
		}
	}
}

