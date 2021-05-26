using System;
using TerraStory.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using TerraStory.Items.Ect;

namespace TerraStory.Items.Weapons
{
    public class BeginnerSword : ModItem
    {
        public override void SetStaticDefaults()
        {
			DisplayName.SetDefault("The Shooting Shell");
            Tooltip.SetDefault("Shoots snail shell and use them as ammo.");
        }
		public override void SetDefaults()
		{
			item.damage = 4;
			item.scale = 1f;
			item.magic = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 25;
			item.useAnimation = 25;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.knockBack = 1f;
			item.value = Item.sellPrice(0, 0, 0, 10);
			item.rare = ItemRarityID.White;
			item.UseSound = SoundID.Item1;
			item.autoReuse = false;
			item.useAmmo = ItemID.Grenade;
			item.shoot = ProjectileID.PurificationPowder;
			item.shootSpeed = 6f;
		}
	}
}