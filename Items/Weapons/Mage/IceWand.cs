using TerraStory.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Projectiles.Magic;

namespace TerraStory.Items.Weapons.Mage
{
	public class IceWand : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Control a ice shards who shatters in mini ice shards \n" +
				"and inflict the frostburn debuff.");
		}

		public override void SetDefaults()
		{
			item.width = 40;
			item.height = 40;
			item.damage = 18;
			item.mana = 5;
			item.knockBack = 1f;
			item.shootSpeed = 8f;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 30);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item9;
			item.magic = true;
			item.noMelee = true;
			item.channel = true;
			Item.staff[item.type] = true;
			item.shoot = ModContent.ProjectileType<IceWandP>();
		}
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.IceBlock, 30);
			recipe.AddIngredient(ItemID.Sapphire, 3);
			recipe.AddIngredient(ItemID.ManaCrystal, 1);
			recipe.AddIngredient(ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}