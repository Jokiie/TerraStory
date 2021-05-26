using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class CorruptedMorsBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Corrupted Mors");
			Description.SetDefault("The corrupted Mors will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.CorruptedMors.CorruptedMors>()] > 0) {
				modPlayer.CorruptedMors = true;
			}
			if (!modPlayer.CorruptedMors)
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