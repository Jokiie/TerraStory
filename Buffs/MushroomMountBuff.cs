using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class MushroomMountBuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mushroom suit");
			Description.SetDefault("Spawn a mushroom suit that allow\n" +
                "you to jump faster.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.mount.SetMount(MountType<Mounts.MushroomMount>(), player);
			player.buffTime[buffIndex] = 10;
		}
	}
}
