using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerSilverHood : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("3% increased minion damage");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.SilverGreaves && body.type == ItemID.SilverChainmail
				&& legs.type == ItemID.TungstenGreaves && body.type == ItemID.TungstenChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.minionDamage += 0.03f;
			player.setBonus = "3% increased minion damage\n" +
		    "+ 15% silver/tungsten pickaxe speed";
			player.GetModPlayer<TerraStoryPlayer>().silverPickaxe = true;
		}
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}