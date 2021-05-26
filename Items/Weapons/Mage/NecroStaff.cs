using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Projectiles.Magic;

namespace TerraStory.Items.Weapons.Mage
{
	public class NecroStaff : ModItem
	{
		public bool ownerHitCheck;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Necro Staff");
			Tooltip.SetDefault("Summon at your cursor a tons of piercing bones. \n" +
				"Mana usage changes through the day, 1.5x at noon \n" +
				" and - 0.5x at midnight.");
		}
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.damage = 25;
			item.mana = 8;
			item.knockBack = 2f;
			item.shootSpeed = 10f;
			item.useTime = 15;
			item.useAnimation = 15;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 50);
			item.rare = ItemRarityID.Orange;
			item.UseSound = SoundID.Item9;
			item.magic = true;
			item.noMelee = true;
			item.channel = true;
			Item.staff[item.type] = true;
			item.shoot = ModContent.ProjectileType<BoneP>();
		}

		// This item's mana usage changes through the day, peaking at 1.5x mana usage at noon, and 0.5x mana usage at midnight.
		// Thanks to chikenbones for the help in the calculations
		public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
		{
			double currentTime = Main.time;
			// The time at which it changes from day to night and vice versa.
			double maxTime = Main.dayTime ? Main.dayLength : Main.nightLength;
			// More mana during day, less at night
			int direction = Main.dayTime ? 1 : -1;
			// Sine goes from 0 to 1 to 0 over a period of pi, so we match that to the length of the day/night.
			float timeMult = (float)Math.Sin(currentTime / maxTime * Math.PI);
			// Then we multiply by direction so it goes between 1 and -1 through the entire day, then multiply by 0.5 and add 1 to make it go between 1.5 and 0.5.
			timeMult = 1 + timeMult * direction * 0.5f;
			// Last, we multiply the current mana cost multiplier of the item by our multiplier.
			mult *= timeMult;
		}
		
	}
}