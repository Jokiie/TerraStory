using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Dusts;

namespace TerraStory.Buffs
{
	public class Electrified : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Electrified");
			Main.buffNoTimeDisplay[Type] = false;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.lifeRegen -= 35;

			Dust.NewDust(npc.position, npc.width, npc.height, 226);
		}
	}
}