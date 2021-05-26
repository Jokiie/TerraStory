using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class CrimsonMetusBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Crimson Metus");
			Description.SetDefault("The crimson Metus will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.CrimsonMetus.CrimsonMetus>()] > 0) {
				modPlayer.CrimsonMetus = true;
			}
			if (!modPlayer.CrimsonMetus)
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