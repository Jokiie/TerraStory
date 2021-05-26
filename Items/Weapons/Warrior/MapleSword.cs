using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Warrior
{
    public class MapleSword : ModItem
	{
		public override void SetStaticDefaults() 
		{
			//Tooltip.SetDefault("");
		}

		public override void SetDefaults()
		{
			item.damage = 40;
			item.scale = 2f;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 10;
			item.useAnimation = 20;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1f;
			item.value = Item.sellPrice(gold: 10);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SwordK");
			item.autoReuse = true;
		}
	}
}