using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class IronRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("2% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 6);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.IronGreaves && body.type == ItemID.IronChainmail
				&& legs.type == ItemID.LeadGreaves && body.type == ItemID.LeadChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedDamage += 0.02f;
			player.GetModPlayer<TerraStoryPlayer>().ironPickaxe = true;
			player.setBonus = "2% increased ranged damage \n" +
				"+ 10% iron/lead pickaxe speed";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}