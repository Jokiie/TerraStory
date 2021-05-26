using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonRangerHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased ranged damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Blue;
			item.defense = 2;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.CrimsonGreaves && body.type == ItemID.CrimsonScalemail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.AddBuff(2, 2, true);
			player.ammoCost80 = true;
			player.setBonus = "Greatly increase life regeneration\n" +
				"20% less chance to consumme ammo";
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CrimtaneBar, 15);
			recipe.AddIngredient(ItemID.TissueSample, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}