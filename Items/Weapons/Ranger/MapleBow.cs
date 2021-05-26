using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Items.Weapons.Ranger;
using TerraStory.Projectiles.Ranger;
using static IL.Terraria.GameContent.Tile_Entities.TETrainingDummy;

namespace TerraStory.Items.Weapons.Ranger
{
    public class MapleBow : ModItem
	{
		public override void SetStaticDefaults() 
		{
			Tooltip.SetDefault("Turns your arrows in maple arrows\n" +
				"If your right click, you push away the ennemies too close from you");
		}

		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 44;
			item.scale = 0.85f;
			item.damage = 35;
			item.knockBack = 0.5f;
			item.shootSpeed = 6f;
			item.useTime = 23;
			item.useAnimation = 25;
			item.shoot = ProjectileID.WoodenArrowFriendly;
			item.useAmmo = AmmoID.Arrow;
			item.useStyle = ItemUseStyleID.HoldingOut;
			item.value = Item.sellPrice(silver: 10);
			item.rare = ItemRarityID.LightRed;
			item.autoReuse = false;
			item.ranged = true;
			item.noMelee = true;
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useTime = 60;
				item.useAnimation = 40;
				item.damage = 30;
				item.knockBack = 2f;
				item.useStyle = ItemUseStyleID.SwingThrow;
				item.UseSound = mod.GetLegacySoundSlot(SoundType.Item, "Sounds/Item/Bow1");
				item.useAmmo = AmmoID.None;
				item.shoot = ProjectileID.None;
				item.melee = true;
				item.ranged = false;
				item.autoReuse = false;
				item.noMelee = false;
				
			}
			else
			{
				SetDefaults();
			}
			return base.CanUseItem(player);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			if (type == 1)
			{
				type = mod.ProjectileType("MapleArrow");
			}
			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			//Projectile.NewProjectile(position.X, position.Y, speedX + (Main.rand.Next(200) / 100), speedY + (Main.rand.Next(200) / 100), type, damage, knockBack, player.whoAmI, 0f, 0f);
			return false;

		}
		public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
		{

			if (player.altFunctionUse == 2 || target.lifeMax < 5 )
			{
				
				target.velocity.X += 30f * player.direction;
				//target.velocity.Y += 30f;
				target.AddBuff(32, 60, true);
			}
		}
	}
}