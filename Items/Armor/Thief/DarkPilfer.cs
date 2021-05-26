using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Head)]
	public class DarkPilfer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
			Tooltip.SetDefault("Set with the 'Dark Shadow' overall'.\n" +
				"Increase thrown damage by 6%.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(gold: 1, silver: 50);
			item.defense = 6;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.06f;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkShadow>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.thrownCrit += 15;
			player.thrownVelocity += 0.15f;
			player.thrownCost33 = true;
			player.setBonus = "Increase throwing critical strike chance \n" +
			"& throwing velocity by 15% and \n" +
			"33% less chance to consume throwings stars.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddIngredient(ModContent.ItemType<BoneThread>(), 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}