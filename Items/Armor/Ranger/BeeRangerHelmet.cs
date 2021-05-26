using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class BeeRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased ranged damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 4;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.BeeGreaves && body.type == ItemID.BeeBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedCrit += 10;
			player.ammoCost80 = true;
			player.setBonus = "10% increased ranged critical chance \n" +
				"20% chance to not consume ammos";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeWax, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}