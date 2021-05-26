using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class KinoBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Kino");
			Description.SetDefault("Kino loot for you (50f range)! ");
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			modPlayer.Kino = true;
			//player.truffle = true;
			bool KinoNotSpawned = true;

			if (player.ownedProjectileCounts[ModContent.ProjectileType<Kino>()] > 0)
			{
				KinoNotSpawned = false;
			}
			if (KinoNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2) * 2, player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Kino>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}