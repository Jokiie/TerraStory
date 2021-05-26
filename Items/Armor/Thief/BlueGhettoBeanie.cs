using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Items.Ect;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Head)]
	public class BlueGhettoBeanie : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
			Tooltip.SetDefault("Set with the 'Blue cloth' overall.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(copper: 5);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BlueCloth>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.statDefense += 1;
			player.setBonus = "Increase defense by 1. ";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 1);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
