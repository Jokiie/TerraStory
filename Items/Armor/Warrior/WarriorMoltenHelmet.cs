using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorMoltenHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increase melee damage by 5% ."
				+ "\nSet bonus : Increase melee damage by 12% and melee crit and speed by 5%.");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Orange;
			item.defense = 8;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.MoltenGreaves && body.type == ItemID.MoltenBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeDamage += 0.12f;
			player.meleeCrit += 5;
			player.meleeSpeed += 0.05f;
		}
		public override void AddRecipes() {
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}