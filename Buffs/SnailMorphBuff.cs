using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class SnailMorphBuff: ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("SnailMorph");
			Description.SetDefault("Transformed into a snail!");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(MountType<Mounts.SnailMorph>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
