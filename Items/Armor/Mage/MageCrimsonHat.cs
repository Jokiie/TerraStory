using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageCrimsonHat : ModItem
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
			return legs.type == ItemID.CrimsonGreaves && body.type == ItemID.CrimsonScalemail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.GetModPlayer<TerraStoryPlayer>().manaCost80 = true;
			player.setBonus ="Greatly increase life regeneration \n" +
				"20% chance to not consumme mana";
			player.AddBuff(2, 2, true);
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