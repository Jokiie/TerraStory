using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class GoldenRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increase ranged crit chance by 5% ."
				+ "\nSet bonus : Increase ranged damage by 5 % .");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedCrit += 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.GoldGreaves && body.type == ItemID.GoldChainmail
				&& legs.type == ItemID.PlatinumGreaves && body.type == ItemID.PlatinumChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.rangedDamage += 0.05f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GoldBar, 25);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}