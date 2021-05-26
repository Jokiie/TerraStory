using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorIronHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increase melee damage by 2% ."
				+ "\nSet bonus : Increase melee speed by 2%");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.02f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.IronGreaves && body.type == ItemID.IronChainmail
				&& legs.type == ItemID.LeadGreaves && body.type == ItemID.LeadChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeSpeed += 0.02f;
		}
	}
}