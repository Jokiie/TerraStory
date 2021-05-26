using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items.Weapons.Thief.Shurikens;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.SilverGuardian
{
	[AutoloadEquip(EquipType.HandsOn)]
	internal class SilverGuardian : ModItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			DisplayName.SetDefault("Silver Guardian");
			Tooltip.SetDefault("Shoots your shurikens safely.");
		}

		public override void SetDefaults()
		{
			item.width = 10;
			item.height = 10;
			item.damage = 9;
			item.knockBack = 0f;
			item.shootSpeed = 6.6f;
			item.useTime = 27;
			item.useAnimation = 27;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.sellPrice(0, 0, 8, 0);
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
			recipe.AddIngredient(ItemID.SilverBar, 8);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.TungstenBar, 8);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}