using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Ect;
using Terraria;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Body)]
	internal class GreenKnuckleVest : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Green Knucklevest");
			Tooltip.SetDefault("Set with the 'Burgler' hat of any colors.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 15);
			item.defense = 2;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			// The equipSlot is added in ExampleMod.cs --> Load hook
			equipSlot = mod.GetEquipSlot("GreenKnuckleVest_Legs", EquipType.Legs);
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
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}