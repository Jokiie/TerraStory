using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor.Warrior
{
	[AutoloadEquip(EquipType.Head)]
	public class WarriorNecroHelmet : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("5% increased melee damage");
		}

		public override void SetDefaults() 
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Green;
			item.defense = 10;
		}
		public override void UpdateEquip(Player player)
		{
			player.meleeDamage += 0.05f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.NecroGreaves && body.type == ItemID.NecroBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.meleeCrit += 15;
			player.meleeSpeed += 10f;
			player.setBonus = "15% increased critical strike chance \n" +
				"10% increased melee speed";
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void AddRecipes() 
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 60);
			recipe.AddIngredient(ItemID.Cobweb, 45);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}