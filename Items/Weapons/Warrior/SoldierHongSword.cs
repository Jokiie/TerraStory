using TerraStory.Projectiles.Minions.SoldierHong;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Warrior
{
    public class SoldierHongSword : ModItem
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("SoldierHongSword");
			Tooltip.SetDefault("Summons a toy soldier each time you attack to fight with you until \n" +
				"he hit 4 times or until 5 seconds.\n" +
				" Damage can be increased by melee and summons accessories.");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.damage = 16;
			item.knockBack = 2f;
			item.mana = 0;
			item.useTime = 28;
			item.useAnimation = 28;
			item.value = Item.buyPrice(0, 0, 50, 0);
			item.rare = ItemRarityID.Orange;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/SwordS");
			item.melee = true;
			item.summon = true;
			//item.buffType = BuffType<SoldierHongBuff>();
			item.shoot = ProjectileType<SoldierHong>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
		//	player.AddBuff(item.buffType, 2);
			position = Main.MouseScreen;
			return true;
		}
	}
}