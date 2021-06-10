using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;
using TerraStory.Tiles;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class DoubleMarine : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Double Marine");
			Tooltip.SetDefault("Set with the 'Beige Carribean' overall.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.White;
			item.value = Item.sellPrice(copper: 10);
			item.defense = 1;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateArmorSet(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.02f;
			player.statDefense += 2;
			player.setBonus = " Increase defense by 2 \n" +
				"increase cannon damage by 2%";
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BeigeCarribean>();
		}


		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 2);
			recipe.AddIngredient(ModContent.ItemType<BlinkrootThread>(), 1);
			recipe.AddTile(ModContent.TileType<IronSewingMachineTile>());
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}