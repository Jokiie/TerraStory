using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerNecroHood : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("5% increased minion damage ");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NecroGreaves && body.type == ItemID.NecroBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.maxMinions++;
			player.maxMinions++;
			player.setBonus = "Increase your max minion slots by 2";
		}
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 60);
			recipe.AddIngredient(ItemID.Cobweb, 45);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}