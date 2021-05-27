using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles;

namespace TerraStory.Items.Weapons.Mage
{
	public class CrimsonStaff : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Staff");
			Tooltip.SetDefault("Shoots a crimson ball on your ennemies.");
		}
		public override void SetDefaults()
		{

			item.width = 40;
			item.height = 40;
			item.damage = 20;
			item.mana = 6;
			item.knockBack = 1f;
			item.shootSpeed = 8f;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item43;
			item.noMelee = true;
			item.magic = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
			item.shoot = ModContent.ProjectileType<CrimsonItem>();
		}
	}
}