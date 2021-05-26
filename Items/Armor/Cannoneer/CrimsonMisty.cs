using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Weapons.Cannoneer;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class CrimsonMisty : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Crimson Misty");
			Tooltip.SetDefault("Set with the 'Crimson Barbay' overall. \n" +
				"Increase cannon damage by 3%.");
		}

		public override void SafeSetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Blue;
			item.value = Item.sellPrice(silver: 35);
			item.defense = 6;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateEquip(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.03f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return  body.type == ModContent.ItemType<CrimsonBarbay>();
		}

		public override void UpdateArmorSet(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonCrit += 7;
			player.setBonus = "increase cannon critical rate chance by 7%.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 2);
			recipe.AddIngredient(ItemID.TissueSample, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}