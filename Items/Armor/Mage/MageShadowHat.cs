using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;

namespace TerraStory.Items.Armor.Mage
{
    [AutoloadEquip(EquipType.Head)]
	public class MageShadowHat : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased magic damage.");
		}
		public override void SetDefaults()
		{
			item.scale = 1.5f;
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 75);
			item.rare = ItemRarityID.Blue;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ItemID.ShadowScalemail && legs.type == ItemID.ShadowGreaves;
		}
		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Increase movement speed by 15% \n" +
				"20% chance to not consume mana";
			player.GetModPlayer<TerraStoryPlayer>().manaCost80 = true;
			player.moveSpeed += 0.15f;
			player.maxRunSpeed += 1;
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