using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Body)]
	internal class CrimsonBarbay : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Barbay");
			Tooltip.SetDefault("Set with the 'Red Misty' bandana.\n" +
				"Increase cannon damage by 4%.");
		}

		public override void SafeSetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 50);
			item.defense = 7;
		}
		public override void UpdateEquip(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.04f;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes)
		{
			robes = true;
			equipSlot = mod.GetEquipSlot("CrimsonBarbay_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms)
		{
			drawHands = true;
			drawArms = true;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 2);
			recipe.AddIngredient(ItemID.TissueSample, 8);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}