using Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;
using Microsoft.Xna.Framework;

namespace TerraStory.Items.Boss
{
	
	public class MossySnailShell : ModItem
    {
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mossy Snail shell");
			Tooltip.SetDefault("This shell belongs to Mano ...\n" +
	       "is he hiding in it?");
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
		   return !NPC.AnyNPCs(mod.NPCType("Mano")) && Main.dayTime;
			
		}
		
		 public override bool UseItem(Player player)
		{
			// Item sound when used
			int target = Main.player[Main.myPlayer].whoAmI;
			Main.PlaySound(SoundID.Roar, player.position);
			Main.NewText("I hid when I saw you..Now I have to fight!", Color.Red);
			if (Main.netMode != NetmodeID.MultiplayerClient)
			{
				int boss = mod.NPCType("Mano");
				int x = (int)(player.position.X + (player.width - 1));
				int y = (int)(player.position.Y + (player.height - 500));
				NPC.NewNPC(x, y, boss, 0, 0, 0, 0, 0,target);				
				
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ModContent.ItemType<RedSnailShell>(), 5);
			recipe.AddIngredient(ModContent.ItemType<BlueSnailShell>(), 5);
			recipe.AddIngredient(ModContent.ItemType<GreenSnailShell>(), 5);
			recipe.AddIngredient(ItemID.FallenStar , 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}