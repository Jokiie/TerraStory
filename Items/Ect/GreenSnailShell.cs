using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Ect
{
	public class GreenSnailShell : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Can be thrown with the beginner sword.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.damage = 2;
			item.knockBack = 0f;
			item.shootSpeed = 3.5f;
			item.maxStack = 999;
			item.value = Item.buyPrice(0, 0, 0, 1);
			item.rare = ItemRarityID.White;
			item.magic = true;
			item.consumable = true;
			item.material = true;
			item.shoot = ProjectileType<Projectiles.GreenSnailShellP>();
			item.ammo = ItemID.Grenade;
		}
	}
}