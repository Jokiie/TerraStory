using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class SoldierHongBuff : ModBuff
	{
		public override void SetDefaults() {
			DisplayName.SetDefault("Soldier Hong");
			Description.SetDefault("Soldier Hong will fight with you,but will die after hitting an ennemy 4 times");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex) {
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ProjectileType<Projectiles.Minions.SoldierHong.SoldierHong>()] > 0) {
				modPlayer.SoldierHong = true;
			}
			if (!modPlayer.SoldierHong)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else 
			{
				player.buffTime[buffIndex] = 1800; //18000 default
			}
		}
	}
}