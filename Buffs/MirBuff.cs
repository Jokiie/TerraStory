using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class MirBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Mir");
			Description.SetDefault("Mir will fight for you");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.Mir.Mir>()] > 0) {
				modPlayer.Mir = true;
			}
			if (!modPlayer.Mir)
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