using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Head)]
	public class GreenBurgler : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Green Burgler");
			Tooltip.SetDefault("Set with the 'KnuckleVest' overall of any colors.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(silver: 95);
			item.defense = 5;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownCrit += 6;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<RedKnuckleVest>() || body.type == ModContent.ItemType<GreenKnuckleVest>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.thrownDamage += 10f;
			player.setBonus = "Increase throwing damage by 10%.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 2);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}