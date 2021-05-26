using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Weapons.Cannoneer;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Armor.Cannoneer
{
	[AutoloadEquip(EquipType.Head)]
	public class PurpleCastLinen : CannonDamageItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Purple Cast Linen");
			Tooltip.SetDefault("Set with the 'Black Royal Baron' overall.\n" +
				"Increase cannon damage by 6%");
		}

		public override void SafeSetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.rare = ItemRarityID.Orange;
			item.value = Item.sellPrice(gold: 1, silver: 50);
			item.defense = 6;
		}
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
		{
			drawAltHair = true;
		}

		public override void UpdateEquip(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonDamageAdd += 0.06f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BlackRoyalBaron>();
		}

		public override void UpdateArmorSet(Player player)
		{
			CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
			modPlayer.cannonCrit += 15;
			modPlayer.cannonKnockback += 0.15f;
			player.setBonus = "Increase cannon critical rate chance \n" +
				"and cannon knockback by 15%.";
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 15);
			recipe.AddIngredient(ModContent.ItemType<BoneThread>(), 2);
			recipe.AddTile(TileID.Loom);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}