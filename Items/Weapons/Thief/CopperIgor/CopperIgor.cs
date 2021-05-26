using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.CopperIgor
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class CopperIgor : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Copper Igor");
			Tooltip.SetDefault("Shoots your shurikens safely.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 6;
			item.knockBack = 0.5f;
			item.shootSpeed = 6.6f;
			item.useTime = 29;
			item.useAnimation = 29;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 0, 70);
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
			recipe.AddIngredient(ItemID.CopperBar, 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TinBar, 10);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 8);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}