using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Garnier
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class Garnier : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Garnier");
			Tooltip.SetDefault("Shoots your shurikens with more power" +
				"\n since you don't hurt yourself while throwing it.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 4;
			item.knockBack = 0f;
			item.shootSpeed = 6.6f;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 0, 5);
			item.rare = ItemRarityID.White;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Tglove2");
			item.shoot = ProjectileID.Shuriken;
			item.useAmmo = ItemID.Shuriken;
			item.handOnSlot = 11;
			item.autoReuse = false;
			item.noUseGraphic = true;
			item.noMelee = false;
			item.thrown = true;
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddIngredient(ItemID.CopperOre, 5);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Silk, 5);
			recipe.AddIngredient(ItemID.TinOre, 5);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
