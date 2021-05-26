using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class SilverRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("3% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.SilverGreaves && body.type == ItemID.SilverChainmail
				&& legs.type == ItemID.TungstenGreaves && body.type == ItemID.TungstenChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedDamage += 0.03f;
			player.setBonus = "3% increased ranged damage\n" +
					"+ 15% silver/tungsten pickaxe speed";
			player.GetModPlayer<TerraStoryPlayer>().silverPickaxe = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 20);
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