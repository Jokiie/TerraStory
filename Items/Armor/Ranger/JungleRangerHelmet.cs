using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class JungleRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 7;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 4;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.JunglePants && body.type == ItemID.JungleShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedCrit += 10;
			player.GetModPlayer<TerraStoryPlayer>().ZoneJungle = true;
			player.setBonus = "10% increased ranged critical strike chance\n" +
				"You can see more clearly in" +
				"the dark when in the jungle";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.JungleSpores, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}