using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Tiles;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Body)]
	public class TurkGally: ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brown Turk Gally");
			Tooltip.SetDefault("Set with the 'Brown Pitz Bandana'.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 15);
			item.defense = 3;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("TurkGally_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 8);
			recipe.AddIngredient(ModContent.ItemType<BlinkrootThread>(), 3);
			recipe.AddTile(ModContent.TileType<IronSewingMachineTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
