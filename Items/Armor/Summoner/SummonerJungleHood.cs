using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerJungleHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased minion damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.JunglePants && body.type == ItemID.JungleShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.minionDamage += 0.20f;
			player.maxMinions++;
			player.setBonus = "20% increased minion damage\n" +
				"Increase max minion slot by 1" +
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