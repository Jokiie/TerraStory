using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class SnailBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Snail");
			Description.SetDefault("The Snail loot for you (50f range)! ");
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			modPlayer.Snail = true;
			//player.truffle = true;
			bool SnailNotSpawned = true;

			if (player.ownedProjectileCounts[ModContent.ProjectileType<Snail>()] > 0)
			{
				SnailNotSpawned = false;
			}
			if (SnailNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2) * 2, player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Snail>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}