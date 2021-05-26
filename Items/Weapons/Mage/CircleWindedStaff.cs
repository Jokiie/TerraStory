using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Projectiles.Magic;

namespace TerraStory.Items.Weapons.Mage
{
	public class CircleWindedStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 20;
			item.mana = 5;
			item.knockBack = 1f;
			item.shootSpeed = 8f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item20;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;
			Item.staff[item.type] = true;
			item.shoot = ModContent.ProjectileType<CircleWindedProj>();
		}
	}
}