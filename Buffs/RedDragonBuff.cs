using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class RedDragonBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Red Dragon");
			Description.SetDefault("The Red Dragon loot for you (150f range)!");
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			modPlayer.RedDragon = true;
			//player.truffle = true;
			bool RedDragonNotSpawned = true;

			if (player.ownedProjectileCounts[ModContent.ProjectileType<RedDragon>()] > 0)
			{
				RedDragonNotSpawned = false;
			}
			if (RedDragonNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2) * 2, player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<RedDragon>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}