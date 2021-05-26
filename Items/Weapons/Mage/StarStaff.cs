using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Mage
{
	public class StarStaff : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Star Staff");
			Tooltip.SetDefault("Shoots stars!On kill, it return to you to replenish some mana.");
		}
		public override void SetDefaults()
		{

			item.width = 40;
			item.height = 40;
			item.damage = 30;
			item.mana = 15;
			item.knockBack = 8;
			item.shootSpeed = 15f;
			item.useTime = 18;
			item.useAnimation = 18;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(0, 2, 9, 0);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = SoundID.Item43;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;
			Item.staff[item.type] = true;
			item.shoot = mod.ProjectileType("Star");
		}
	}
}