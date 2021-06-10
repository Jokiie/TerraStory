using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Boss
{
	
	public class PapaPixieHat : ModItem
    {
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Papa Pixie's hat");
			Tooltip.SetDefault("This strange hat look like it from a poweful creature.");
		}
		
		public override void SetDefaults()
		{
		   item.width = 26;
		   item.height = 26;
		   item.maxStack = 20;
		   item.rare = ItemRarityID.LightRed;
		   item.useAnimation = 45;
		   item.useTime = 45;
		   item.useStyle = ItemUseStyleID.HoldingUp;
		   item.consumable = true;
		}
		
		 public override bool CanUseItem(Player player)
		{
			// we make sure that the boss doesn't already exist
		   return !NPC.AnyNPCs(mod.NPCType("PapaPixie")) && !Main.dayTime;
			
		}
		
		 public override bool UseItem(Player player)
		{
			// Item sound when used
			Main.PlaySound(SoundID.Roar, player.position);
			if(Main.netMode != NetmodeID.MultiplayerClient || !Main.dayTime)
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("PapaPixie"));
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FallenStar, 10);
			recipe.AddIngredient(ModContent.ItemType<Items.Ect.LunarPixieMoonPiece>(), 3);
			recipe.AddIngredient(ModContent.ItemType<Items.Ect.LusterPixieSunPiece>(), 3);
			recipe.AddIngredient(ModContent.ItemType<Items.Ect.StarPiece>(), 3);
			recipe.AddTile(TileID.Hellforge);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}