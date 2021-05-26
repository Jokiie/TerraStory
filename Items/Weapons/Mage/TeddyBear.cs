using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Mage
{
	public class TeddyBear : ModItem
	{
		public override void SetStaticDefaults() 
		{
            Tooltip.SetDefault("Call the spirits of fallen teddies bears.");
		}

		public override void SetDefaults() 
		{
			item.scale = 0.90f;
			item.width = 60;
			item.height = 65;
			item.damage = 60;
			item.mana = 25;
			item.knockBack = 2f;
			item.shootSpeed = 10f;
			item.useTime = 10;
			item.useAnimation = 10;
			item.useStyle = ItemUseStyleID.HoldingUp;
			item.value = Item.sellPrice(gold: 5);
			item.rare = ItemRarityID.LightRed;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Spell1");
			item.magic = true;
			item.noMelee = true;
			item.autoReuse = true;
			Item.staff[item.type] = true;
			item.shoot = ProjectileType<TeddySpirit>();
		}
	}
}