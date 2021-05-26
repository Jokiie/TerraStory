using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class KevinTheCutePigBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Kevin, The Cute Pig");
			Description.SetDefault("Kevin will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.KevinTheCutePig.KevinTheCutePig>()] > 0) {
				modPlayer.KevinTheCutePig = true;
			}
			if (!modPlayer.KevinTheCutePig)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else 
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}