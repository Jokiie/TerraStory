using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class BrownLeatherOceanHat : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leather Ocean Hat");
			Tooltip.SetDefault("Set with the 'Green Plasteer' overall.\n" +
				"Increase cannon critical rate chance by 6%.");
		}

		public override void SafeSetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(silver: 95);
			item.defense = 5;
		}

		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateEquip(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonCrit += 6;

		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<GreenPlasteer>();
		}

		public override void UpdateArmorSet(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.10f;
			player.setBonus = "Increase cannon damage by 10%.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<CottonBall>(), 10);
			recipe.AddIngredient(ModContent.ItemType<MapleThread>(), 2);
			recipe.AddIngredient(ItemID.Leather, 5);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}