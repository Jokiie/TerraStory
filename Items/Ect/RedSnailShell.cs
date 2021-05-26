using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Ect
{
	public class RedSnailShell : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.damage = 4;
			item.knockBack = 1f;
			item.shootSpeed = 3.5f;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 1);
			item.rare = ItemRarityID.White;
			item.magic = true;
			item.consumable = true;
			item.material = true;
			item.shoot = ModContent.ProjectileType<Projectiles.RedSnailShellP>();
			item.ammo = ItemID.Grenade;
		}
	}
}
