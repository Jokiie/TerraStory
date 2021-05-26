using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Warrior
{
	public class RedValentineRose : ModItem
	{
		public override void SetStaticDefaults() 
		{
			// DisplayName.SetDefault("Red Valentine Rose"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			Tooltip.SetDefault("You should give it to the one you love.");
		}

		public override void SetDefaults() 
		{
			item.damage = 30;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}
	}
}