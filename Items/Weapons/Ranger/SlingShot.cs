using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Ranger
{
    public class SlingShot : ModItem
	{
		public override void SetStaticDefaults() 
		{

		}

		public override void SetDefaults() 
		{
			item.damage = 15;
			item.ranged = true;
			item.scale = 0.60f;
			item.width = 31;
			item.height = 32;
			item.useTime = 35;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 2f;
			item.value = Item.sellPrice(0, 0, 5, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/DualBow2");
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 6f;
			item.useAmmo = ItemType<Rock>();
		}
	}
}
