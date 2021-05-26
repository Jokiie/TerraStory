using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class LeapBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Leap Buff");
			//Description.SetDefault("");
			Main.pvpBuff[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.noFallDmg = true;
		}
	}
}