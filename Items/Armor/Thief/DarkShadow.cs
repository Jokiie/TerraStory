using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Body)]
	internal class DarkShadow : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dark Shadow overall");
			Tooltip.SetDefault("Set with the 'Dark pilfer' bandana. \n" +
				"Increase thrown damage by 6%.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(gold: 1);
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.06f;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			// The equipSlot is added in ExampleMod.cs --> Load hook
			equipSlot = mod.GetEquipSlot("DarkShadow_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 25);
			recipe.AddIngredient(ModContent.ItemType<BoneThread>(), 5);
			recipe.AddIngredient(ItemID.SilverBar, 3);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}