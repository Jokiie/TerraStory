using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class CopperRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("1% increased ranged critical strike chance");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 1, copper: 50);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 1;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.CopperGreaves && body.type == ItemID.CopperChainmail
				&& legs.type == ItemID.TinGreaves && body.type == ItemID.TinChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedDamage += 0.01f;
			player.setBonus = " 1% increased ranged damage\n" +
					"+ 5% copper/tin pickaxe speed";
			player.GetModPlayer<TerraStoryPlayer>().copperPickaxe = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperBar, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinBar, 15);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}