using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class HogMount : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Hog");
			Description.SetDefault("Spawn a hog orignally from maple world.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(MountType<Mounts.Hog>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
