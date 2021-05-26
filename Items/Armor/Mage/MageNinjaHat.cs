using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageNinjaHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("3% increased magic damage.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 20);
			item.rare = ItemRarityID.Blue;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicDamage = 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NinjaPants && body.type == ItemID.NinjaShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increase your movement speed by 20%\n" +
				"Reduce mana cost by 5%";
			player.moveSpeed += 20f;
			player.manaCost -= 0.05f;
		}
	}
}