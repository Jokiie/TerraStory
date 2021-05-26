using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Items.Armor.Maple
{
	[AutoloadEquip(EquipType.Body)]
	internal class WhiteCrusaderChainMail : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("White Crusader Chainmail");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 14;
			item.rare = ItemRarityID.Green;
			item.defense = 8;
		}

		public override void SetMatch(bool male, ref int equipSlot, ref bool robes) {
			robes = true;
			// The equipSlot is added in ExampleMod.cs --> Load hook
			equipSlot = mod.GetEquipSlot("WhiteCrusaderChainMail_Legs", EquipType.Legs);
		}

		public override void DrawHands(ref bool drawHands, ref bool drawArms) {
			drawHands = true;
		}
	}
}
