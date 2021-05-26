using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
	public class BundleOfMesos : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Bundle of mesos");
			Tooltip.SetDefault("This is lot of mesos");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(30, 3));
		}
		public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.scale = 0.80f;
			item.maxStack = 999;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.Blue;
        }
	}
}
