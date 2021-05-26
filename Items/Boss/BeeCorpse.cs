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

	public class BeeCorpse : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee corpse");
			Tooltip.SetDefault("You just killed a true queen bee child, maybe you should hide that little body so you don't wake her up!");
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
			return !NPC.AnyNPCs(mod.NPCType("TrueQueenBee")) && player.ZoneJungle;

		}

		public override bool UseItem(Player player)
		{
			// Item sound when used
			Main.PlaySound(SoundID.Roar, player.position);
			if (Main.netMode != NetmodeID.MultiplayerClient )
			{
				NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("TrueQueenBee"));
			}
			return true;

		}
	}
}