using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorBeeHelmet : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("4% increased melee critical chance");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 13;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.BeeGreaves && body.type == ItemID.BeeBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeCrit += 10;
			player.meleeSpeed += 0.15f;
			player.setBonus = "10% increased melee critical strike chance\n" +
				"15% increased melee speed ";
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