using Terraria;
using Terraria.ID;
using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Ranger
{
	public class Bow : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("First bow is always special.");
		}

		public override void SetDefaults()
		{
			item.width = 12;
			item.height = 28;
			item.damage = 4;
			item.knockBack = 0f;
			item.useTime = 30;
			item.useAnimation = 30;
			item.shootSpeed = 6.6f;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(copper: 5);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item5;
			item.ranged = true;
			item.noMelee = true;
			item.autoReuse = false;
			item.shoot = ProjectileID.PurificationPowder;
			item.useAmmo = AmmoID.Arrow;
		}
	}
}
