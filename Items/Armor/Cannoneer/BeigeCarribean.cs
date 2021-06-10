using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Ect;
using Terraria;
using TerraStory.Tiles;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Body)]
	internal class BeigeCarribean : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beige Carribean");
			Tooltip.SetDefault("Set with the 'Double Marine' cap.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 10);
			item.defense = 2;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("BeigeCarribean_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 4);
			recipe.AddIngredient(ModContent.ItemType<BlinkrootThread>(), 2);
			recipe.AddTile(ModContent.TileType<IronSewingMachineTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}