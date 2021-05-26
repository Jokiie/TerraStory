using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageNecroHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("5% increased damage.");
		}
		public override void SetDefaults()
		{
			item.scale = 1.5f;
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Green;
			item.defense = 3;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NecroGreaves && body.type == ItemID.NecroBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.GetModPlayer<TerraStoryPlayer>().necroStaff = true;
			player.setBonus = "Increase your magical crit chance by 15% \n" +
				"And the NecroStaff cost less mana at day and night.";
			player.magicCrit += 15;
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 40);
			recipe.AddIngredient(ItemID.Cobweb, 40);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}