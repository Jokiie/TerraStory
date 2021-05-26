using TerraStory.Projectiles.Minions.OrangeMushroom;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons.Minions
{
    public class OrangeMushroomBuff : ModBuff
	{
	    public override void SetDefaults() 
		{
		    DisplayName.SetDefault("Orange Mushroom");
		    Description.SetDefault("The Orange Mushroom will fight for you");
		    Main.buffNoSave[Type] = true;
		    Main.buffNoTimeDisplay[Type] = true;
	    }

	    public override void Update(Player player, ref int buffIndex) {
		    if (player.ownedProjectileCounts[ProjectileType<OrangeMushroom>()] > 0) {
			    player.buffTime[buffIndex] = 18000;
		    }
		    else {
		     	player.DelBuff(buffIndex);
			    buffIndex--;
			}
		}
	}
	
    public class OrangeMushroomStaff : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Orange Mushroom Staff");
			Tooltip.SetDefault("Summons an Orange Mushroom to fight for you");
			ItemID.Sets.GamepadWholeScreenUseRange[item.type] = true; // This lets the player target anywhere on the whole screen while using a controller.
			ItemID.Sets.LockOnIgnoresCollision[item.type] = true;
		}

		public override void SetDefaults() {
			item.damage = 9;
			item.knockBack = 2f;
			item.mana = 10;
			item.width = 26;
			item.height = 26;
			item.useTime = 28;
			item.useAnimation = 28;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 2, 0, 0);
			item.rare = ItemRarityID.Blue;
			item.UseSound = SoundID.Item44;
			item.noMelee = true;
			item.summon = true;
			item.buffType = BuffType<OrangeMushroomBuff>();
			item.shoot = ProjectileType<OrangeMushroom>();
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack) {
			player.AddBuff(item.buffType, 2);
			position = Main.MouseWorld;
			return true;
		}
	}
}