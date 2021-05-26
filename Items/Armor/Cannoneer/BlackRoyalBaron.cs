using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Body)]
	internal class BlackRoyalBaron : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Black Royal Baron");
			Tooltip.SetDefault("Set with the 'Purple Cast Linen' bandana. \n" +
				"Increase cannon damage by 6%.");
		}

		public override void SafeSetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(gold: 1);
			item.defense = 8;
		}

		public override void UpdateEquip(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.06f;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			// The equipSlot is added in ExampleMod.cs --> Load hook
			equipSlot = mod.GetEquipSlot("BlackRoyalBaron_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = false;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 25);
			recipe.AddIngredient(ModContent.ItemType<BoneThread>(), 5);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}