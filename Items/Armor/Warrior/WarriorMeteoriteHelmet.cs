using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorMeteoriteHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("4% increased melee damage");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Blue;
			item.defense = 10;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.MeteorLeggings && body.type == ItemID.MeteorSuit;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeSpeed += 0.10f;
			player.setBonus = "10% increased melee speed";
		}
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MeteoriteBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}