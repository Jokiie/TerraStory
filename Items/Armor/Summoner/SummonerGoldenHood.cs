using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerGoldenHood : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("5% increased minion damage");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.White;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.GoldGreaves && body.type == ItemID.GoldChainmail
				&& legs.type == ItemID.PlatinumGreaves && body.type == ItemID.PlatinumChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.minionDamage += 0.05f;
			player.GetModPlayer<TerraStoryPlayer>().copperPickaxe = true;
			player.setBonus = "5% increased summon damage\n" +
				"+ 5% gold/platinum pickaxe speed";
		}
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumBar, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}