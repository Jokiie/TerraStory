using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Summoner
{
	[AutoloadEquip(EquipType.Head)]
	public class SummonerNinjaHood : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("3% increased minion damage");
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
			player.minionDamage += 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NinjaPants && body.type == ItemID.NinjaShirt;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 20f;
			player.maxMinions++;
			player.setBonus = "20% increased movement speed\n" +
				"Increased your max number of minion by 1";
		}
	}
}