using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.Graphics.Shaders;

namespace TerraStory.Items.Armor.Mage
{
	[AutoloadEquip(EquipType.Head)]
	public class MageMoltenHat : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("7% increased magic damage.");
		}
		public override void SetDefaults()
		{
			item.scale = 1.5f;
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(silver: 90);
			item.rare = ItemRarityID.Orange;
			item.defense = 0;
		}
		public override void UpdateEquip(Player player)
		{
			player.magicDamage += 0.07f;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return legs.type == ItemID.MoltenGreaves && body.type == ItemID.MoltenBreastplate;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "25% chance to not consumme mana \n" +
				"Increase your max mana by 40\n" +
					"+ 40% molten pickaxe speed";
			player.GetModPlayer<TerraStoryPlayer>().moltenPickaxe = true;
			player.GetModPlayer<TerraStoryPlayer>().manaCost75 = true;
			player.statManaMax2 += 40;
			{
				for (int l = 0; l < 2; l++)
				{
					int num20 = Dust.NewDust(new Vector2(player.position.X - player.velocity.X * 2f, player.position.Y - 2f - player.velocity.Y * 2f), player.width, player.height, 6, 0f, 0f, 100, default(Color), 2f);
					Main.dust[num20].noGravity = true;
					Main.dust[num20].noLight = true;
					Main.dust[num20].velocity.X -= player.velocity.X * 0.5f;
					Main.dust[num20].velocity.Y -= player.velocity.Y * 0.5f;
					Main.dust[num20].shader = GameShaders.Armor.GetSecondaryShader(player.ArmorSetDye(), player);
					if (player.velocity.X == 0 && player.velocity.Y == 0)
                    {
						Main.dust[num20].active = false;
                    }
				}
			}
		}

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.HellstoneBar, 10);
			recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}