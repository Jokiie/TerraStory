using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Tiles;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class BrownPitzBandana : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Brown Pitz Bandana");
			Tooltip.SetDefault("Set with the 'TurkGally' overall.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 15);
			item.defense = 2;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increase defense by 2. ";
			player.statDefense += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<TurkGally>();
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 5);
			recipe.AddIngredient(ModContent.ItemType<BlinkrootThread>(), 2);
			recipe.AddTile(ModContent.TileType<IronSewingMachineTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}