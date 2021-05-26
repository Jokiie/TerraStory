using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class FennecFoxBuff : ModBuff
	{
		public override void SetDefaults()
		{
			Main.buffNoTimeDisplay[Type] = true;
			DisplayName.SetDefault("Fennec Fox");
			Description.SetDefault("The Fennec Fox loot for you (100f range)!");
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			modPlayer.FennecFox = true;
			//player.truffle = true;
			bool FennecFoxNotSpawned = true;

			if (player.ownedProjectileCounts[ModContent.ProjectileType<FennecFox>()] > 0)
			{
				FennecFoxNotSpawned = false;
			}
			if (FennecFoxNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + (float)(player.width / 2) * 2, player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<FennecFox>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}