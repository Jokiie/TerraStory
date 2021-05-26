using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
    public class NinjaKatana : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A very cheap katana from the ninja in the king slime. \n" +
				"I guess that's why he got defeated");
		}

		public override void SetDefaults()
		{
			item.damage = 10;
			item.melee = true; 
			item.width = 40; 
			item.height = 40;
			item.autoReuse = true;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 10;
			item.useAnimation = 10;
			item.knockBack = 0.3f; 
			item.value = Item.buyPrice(silver: 5);
			item.rare = ItemRarityID.Blue; 
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;

		}
	}
}