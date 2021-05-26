using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Mage
{
	public class BigMushroom : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Big mushroom");
			Tooltip.SetDefault("Release mushroom powder that chases your enemies\n" +
				"When you shake it");
		}
		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 20;
			item.mana = 3;
			item.knockBack = 0f;
			item.useTime = 18;
			item.useAnimation = 18;
			item.shootSpeed = 6.6f;
			item.shoot = mod.ProjectileType("BigMushroomP");
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item43;
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = false;
			Item.staff[item.type] = true;
		}
	}
}