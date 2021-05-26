using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Ranger
{
	[AutoloadEquip(EquipType.Head)]
	public class ShadowRangerHelmet : ModItem
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
			item.defense = 3;
		}
		public override void UpdateEquip(Player player)
		{
			player.rangedDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.ShadowGreaves && body.type == ItemID.ShadowScalemail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.moveSpeed += 0.15f;
			player.ammoCost80 = true;
			player.setBonus = "Increase movement speed by 15% \n" +
	            "20% chance to not consume ammo";
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.DemoniteBar, 15);
			recipe.AddIngredient(ItemID.ShadowScale, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}