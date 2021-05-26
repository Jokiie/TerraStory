using TerraStory.Projectiles.Pets;
using TerraStory.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items
{
    public class GreenDragonSummon : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Green Dragon");
			Tooltip.SetDefault("Summons a Green Dragon!");
		}

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.Orange;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 3, 0, 0);
			item.buffType = ModContent.BuffType<GreenDragonBuff>();
			item.shoot = ModContent.ProjectileType<GreenDragon>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<BabyDragonSummon>(), 1);
			recipe.AddIngredient(ItemType<RockOfEvolution>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}