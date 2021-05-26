using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageIronHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("2% increased magic critical strike chance");
		}
		public override void SetDefaults()
		{
			item.scale = 1.5f;
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 6);
			item.rare = ItemRarityID.White;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicCrit += 2;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.IronGreaves && body.type == ItemID.IronChainmail
				&& legs.type == ItemID.LeadGreaves && body.type == ItemID.LeadChainmail;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.magicDamage += 0.02f;
			player.GetModPlayer<TerraStoryPlayer>().ironPickaxe = true;
			player.setBonus = "2% increased magic damage \n" +
				"+ 10% iron/lead pickaxe speed";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IronBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.LeadBar, 20);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}