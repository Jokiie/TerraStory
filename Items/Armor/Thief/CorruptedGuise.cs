using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Thief
{
	[AutoloadEquip(EquipType.Head)]
	public class CorruptedGuise : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purple Guise");
			Tooltip.SetDefault("Set with the 'Purple Stealer' overall.");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 35);
			item.defense = 6;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}
		public override void UpdateEquip(Player player)
		{
			player.thrownDamage += 0.03f;
		}
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<PurpleStealer>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.thrownCrit += 7;
			player.setBonus = "Increase throwing critical rate chance by 7%.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 2);
			recipe.AddIngredient(ItemID.ShadowScale, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}