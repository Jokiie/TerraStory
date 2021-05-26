using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Ect;
using Terraria;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Body)]
	internal class BrownSneak : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brown Sneak");
			Tooltip.SetDefault("Set with the 'Brown Tiberian' hat.");
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
			// The equipSlot is added in ExampleMod.cs --> Load hook
			equipSlot = mod.GetEquipSlot("BrownSneak_Legs", EquipType.Legs);
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
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
