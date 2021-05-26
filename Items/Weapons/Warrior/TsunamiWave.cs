using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
    public class TsunamiWave : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.scale = 1f;
			item.damage = 30;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 23;
			item.useAnimation = 23;
			item.knockBack = 2;
			item.value = Item.buyPrice(silver: 50);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
		}
	}
}