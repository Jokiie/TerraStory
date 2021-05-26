using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerShadowHood : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("4% increased minion damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Blue;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.ShadowGreaves && body.type == ItemID.ShadowScalemail;
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void UpdateArmorSet(Player player)
		{
			player.maxMinions++;
			player.moveSpeed += 0.15f;
			player.setBonus = "Increase movement speed by 15% \n" +
	             "Increase max minion slot by 1";

		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 15);
			recipe.AddIngredient(ItemID.ShadowScale, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}