using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using static Terraria.ModLoader.ModContent;
using static TerraStory.Content.Players.ContentSamples;

namespace TerraStory.Items.Weapons.Warrior
{
    public class Sword : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("First sword is always special.");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.scale = 1.5f;
			item.damage = 12;
			item.knockBack = 2; // The force of knockback of the weapon. Maximum is 20
			item.useTime = 23;
			item.useAnimation = 23;
			item.value = Item.buyPrice(copper: 5);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.melee = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinOre, 5);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperOre, 5);
			recipe.AddIngredient(ItemID.Wood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}