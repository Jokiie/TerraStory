using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using TerraStory.Content.Players;
using TerraStory.Enums;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageBeeHat : ModItem
	{

		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("4% increased magic damage");
		}

		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver : 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 2;

		}
		public override void UpdateEquip(Player player)
		{
		   player.magicDamage += 0.04f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.BeeGreaves && body.type == ItemID.BeeBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.GetModPlayer<TerraStoryPlayer>().beeGun = true;
			player.magicCrit = 10;
			player.setBonus = "10% increased magic critical chance\n" +
				"the BeeGun cost 0 mana.";
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeeWax, 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}