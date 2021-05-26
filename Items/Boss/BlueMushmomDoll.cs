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
	
	public class BlueMushmomDoll : ModItem
    {
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blue Mushmom Doll");
			Tooltip.SetDefault("This strange doll look like it come from another world");
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
		   return !NPC.AnyNPCs(mod.NPCType("BlueMushmom"))&& player.ZoneGlowshroom;
			
		}
		
		 public override bool UseItem(Player player)
		{
			// Item sound when used
			Main.PlaySound(SoundID.Roar, player.position);
			if(Main.netMode != NetmodeID.MultiplayerClient)
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("BlueMushmom"));
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.GlowingMushroom, 15);
			recipe.AddIngredient(ItemID.FallenStar , 1);
			recipe.AddIngredient(ModContent.ItemType<MapleLeaf>(), 10);
			recipe.AddTile(TileID.DemonAltar);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}