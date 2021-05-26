using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class NinjaRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("3% increased ranged damage");
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.White;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NinjaPants && body.type == ItemID.NinjaShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 20f;
			player.ammoCost80 = true;
			player.setBonus = "20% increased movement speed\n" +
				"20% chance to not consume ammo";
		}
	}
}