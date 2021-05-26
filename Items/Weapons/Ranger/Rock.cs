using TerraStory.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Weapons.Ranger;

namespace TerraStory.Items.Weapons.Ranger
{
	public class Rock : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("When shoot by a slingshot, it can piece up to 3 ennemies!");
		}

		public override void SetDefaults()
		{
			item.scale = 0.60f;
			item.damage = 1;
			item.ranged = true;
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 2f;
			item.shootSpeed = 8f;
			item.value = Item.sellPrice(0, 0, 0, 5);
			item.rare = ItemRarityID.White;
			item.shoot = ProjectileType<Projectiles.Rock>();
			item.ammo = item.type; // The first item in an ammo class sets the AmmoID to it's type
		}

		public override void AddRecipes()
		{
			RockRecipe recipe = new RockRecipe(mod);
			recipe.AddIngredient(ItemID.StoneBlock, 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 20);
			recipe.AddRecipe();
		}
        public class RockRecipe : ModRecipe
	    {
		public RockRecipe(Mod mod) : base(mod)
		{
		}

		public override bool RecipeAvailable()
		{
			return Main.LocalPlayer.HasItem(ItemType<SlingShot>());
		}

			public override int ConsumeItem(int type, int numRequired)
			{
				if (type == ItemType<Rock>())
				{
					Main.PlaySound(SoundID.Item, -1, -1, mod.GetSoundSlot(SoundType.Item, "Sounds/Item/BareHands1"));
					return Main.rand.NextBool() ? 0 : 1; //You have half chance to not consume your materials
				}
				return base.ConsumeItem(type, numRequired);
			}
		}
	}
}
