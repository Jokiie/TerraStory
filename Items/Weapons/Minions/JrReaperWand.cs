using TerraStory.Projectiles.Minions.JrReaper;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Minions
{
	public class JrReaperWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Summons a JrReaper that shoot skulls on your ennemies!");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}
		public override void SetDefaults() 
		{
			item.damage = 16;
			item.summon = true;
			item.knockBack = 2f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;
			item.noMelee = true;
			item.shoot = ProjectileType<JrReaper>();
			item.buffType = BuffType<Buffs.JrReaperBuff>();
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
	    }
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 100);
			recipe.AddIngredient(ItemID.Shadewood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();

			recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 100);
			recipe.AddIngredient(ItemID.Ebonwood, 10);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}