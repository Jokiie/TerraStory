using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorSilverHelmet : ModItem
	{
		public override void SetStaticDefaults() {
			Tooltip.SetDefault("Increase melee damage by 3% ."
				+ "\nSet bonus : Increase melee speed by 3%");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.White;
			item.defense = 3;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.SilverGreaves && body.type == ItemID.SilverChainmail
				&& legs.type == ItemID.TungstenGreaves && body.type == ItemID.TungstenChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeSpeed += 0.2f;
		}
	}
}