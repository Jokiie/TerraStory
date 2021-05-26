using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Projectiles.Magic;

namespace TerraStory.Items.Weapons.Mage
{
	public class CrystalWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Shoots crystalized bubbles fishes");
		}

		public override void SetDefaults() 
		{
			item.width = 30;
			item.height = 30;
			item.damage = 25;
			item.mana = 5;
			item.shootSpeed = 15f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.knockBack = 5;
			item.value = Item.sellPrice(silver: 4);
			item.rare = ItemRarityID.Green;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
			item.shoot = ProjectileType<Bubbles>();

		}
	}
}