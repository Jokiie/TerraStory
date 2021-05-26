using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorNinjaHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("3% increased melee damage");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 20);
			item.rare = ItemRarityID.Blue;
			item.defense = 4;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage = 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NinjaPants && body.type == ItemID.NinjaShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 20f;
			player.meleeSpeed += 0.05f;
			player.setBonus = "20% increase movement speed \n" +
				"5% increased melee speed";
		}
	}
}