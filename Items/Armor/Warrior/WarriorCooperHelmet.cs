using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorCooperHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increase melee damage by 1% ."
				+ "\nSet bonus : Increase melee speed by 1% .");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.01f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.CopperGreaves && body.type == ItemID.CopperChainmail
			    && legs.type == ItemID.TinGreaves && body.type == ItemID.TinChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeSpeed += 0.01f;
		}
	}
}