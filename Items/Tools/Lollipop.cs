using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Tools
{
	public class Lollipop : ModItem
	{
		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault(" Lolli-Axe");
		}

		public override void SetDefaults() 
		{
			item.damage = 4;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 29;
			item.useAnimation = 22;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 6;
			item.value = 10000;
            item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
            item.axe = 8; // How much axe power the weapon has, note that the axe power displayed in-game is this value multiplied by 5
			item.autoReuse = true;
		}

		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("Wood", 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 3);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}