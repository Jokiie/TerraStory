using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class BlueSnailShell : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults() 
		{
			item.width = 20;
			item.height = 20;
			item.damage = 3;
			item.knockBack = 0.5f;
			item.shootSpeed = 3.5f;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 1);
			item.rare = ItemRarityID.White;
			item.magic = true;
			item.consumable = true;
			item.material = true;
			item.shoot = ModContent.ProjectileType<Projectiles.BlueSnailShellP>();
			item.ammo = ItemID.Grenade;
		}
	}
}
