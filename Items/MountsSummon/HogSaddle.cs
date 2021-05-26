using TerraStory.Mounts;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.MountsSummon
{
	public class HogSaddle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("A hog from maple world.");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 30;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = 30000;
			item.rare = ItemRarityID.Green;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Summon");
			item.noMelee = true;
			item.mountType = MountType<Hog>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}