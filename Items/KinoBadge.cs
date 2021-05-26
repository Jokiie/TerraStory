using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using TerraStory.Buffs;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace TerraStory.Items
{
	public class KinoBadge : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Kino Badge");
			Tooltip.SetDefault("Summons Kino!");
		}

		public override void SetDefaults()
		{
			item.damage = 0;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.width = 16;
			item.height = 30;
			item.UseSound = SoundID.Item2;
			item.useAnimation = 20;
			item.useTime = 20;
			item.rare = ItemRarityID.Orange;
			item.noMelee = true;
			item.value = Item.buyPrice(0, 3, 0, 0);
			item.buffType = ModContent.BuffType<KinoBuff>();
			item.shoot = ModContent.ProjectileType<Kino>();
		}
	}
}