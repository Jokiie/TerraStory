using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class JrReaperBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Jr.Reaper");
			Description.SetDefault("The Jr.Reaper will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.JrReaper.JrReaper>()] > 0) {
				modPlayer.JrReaper = true;
			}
			if (!modPlayer.JrReaper)
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