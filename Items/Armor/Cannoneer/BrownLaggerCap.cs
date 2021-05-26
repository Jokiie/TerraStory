using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Weapons.Cannoneer;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class BrownLaggerCap : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lagger Cap");
			Tooltip.SetDefault("Set with the 'Cotton Lagger' overall.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 5);
			item.defense = 1;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateArmorSet(Player player)
		{

			player.statDefense += 1;
			player.setBonus = "Increase defense by 1. ";

		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BrownCottonLagger>();
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