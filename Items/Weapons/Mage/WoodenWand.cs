using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Mage
{
	public class WoodenWand : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Shoots stump's leaves.");
			Item.staff[item.type] = true; //this makes the useStyle animate as a staff instead of as a gun
		}

		public override void SetDefaults() 
		{
			item.width = 35;
			item.height = 34;
			item.scale = 0.80f;
			item.damage = 11;
			item.knockBack = 0f;
			item.mana = 3;
			item.shootSpeed = 6.6f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.value = Item.sellPrice(copper: 5);
			item.rare = ItemRarityID.White;
			Item.staff[item.type] = true;
			item.magic = true;
			item.autoReuse = false;
			item.shoot = ProjectileType<StumpLeaf>();

		}
	}
}