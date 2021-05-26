using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Tools
{
	public class MapleHavorHammer : ModItem
	{
		public override void SetStaticDefaults()
		{
			//Tooltip.SetDefault("This certainly come from Maple World.");
		}

		public override void SetDefaults()
		{
			item.damage = 22;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 14;
			item.useAnimation = 27;
			item.hammer = 70;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 8;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<GoldenMole>(), 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 25);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
