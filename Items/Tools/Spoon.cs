using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Tools
{
	public class Spoon : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Who ever thought of mining with a spoon ?");
		}

		public override void SetDefaults() {
			item.damage = 4;
			item.melee = true;
			item.width = 32;
			item.height = 32;
			item.useTime = 22;
			item.useAnimation = 14;
			item.pick = 35;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
		}

		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}