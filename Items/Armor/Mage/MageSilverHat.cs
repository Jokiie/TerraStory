using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageSilverHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Increase your magic critical chance by 3%.");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 15);
			item.rare = ItemRarityID.White;
			item.defense = 1;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 3;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.SilverGreaves && body.type == ItemID.SilverChainmail
				&& legs.type == ItemID.TungstenGreaves && body.type == ItemID.TungstenChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "3% increased magic damage\n" +
					"+ 15% silver/tungsten pickaxe speed";
			player.GetModPlayer<TerraStoryPlayer>().silverPickaxe = true;
			player.magicDamage += 0.03f;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SilverBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}