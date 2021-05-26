using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Projectiles.ShurikensProj;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Thief.Shurikens
{
    public class Icicle : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Frostburns your ennemies to death!");
		}
		public override void SetDefaults()
		{
			item.damage = 25;
			item.knockBack = 1f;
			item.shootSpeed = 6.6f;
			item.width = 20;
			item.height = 20;
			item.useTime = 20;
			item.useAnimation = 20;
			item.maxStack = 999;
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item1;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.shoot = ModContent.ProjectileType<IcicleP>();
			item.value = Item.sellPrice(copper: 20);
			item.ammo = ItemID.Shuriken;
			item.thrown = true;
			item.noMelee = true;
			item.consumable = true;
			item.noUseGraphic = true;
		}
		public override void OnConsumeItem(Player player)
		{
			player.AddBuff(BuffType<TooSharp>(), 50);
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 500);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 5);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 500);
			recipe.AddRecipe();
		}
	}
}