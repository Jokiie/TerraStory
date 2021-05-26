using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Maple
{
	[AutoloadEquip(EquipType.Head)]
	public class BrownBambooHat : ModItem
	{
		public override void SetStaticDefaults() {
		}

		public override void SetDefaults() {
			item.width = 18;
			item.height = 18;
			item.value = 10000;
			item.rare = ItemRarityID.Green;
			item.defense = 2;
		}
	}
}