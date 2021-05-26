using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
    public class LionHeart : ModItem
	{
		public override void SetStaticDefaults()
		{
		}

		public override void SetDefaults()
		{
			item.scale = 1.5f;
			item.damage = 20;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.useTime = 23;
			item.useAnimation = 23;
			item.knockBack = 2;
			item.value = Item.buyPrice(silver: 5);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 4);
			recipe.AddIngredient(ItemID.SilverBar, 4);
			recipe.AddRecipeGroup(ItemID.Wood, 4);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 4);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}