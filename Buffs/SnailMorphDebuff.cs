using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Buffs
{
	public class SnailMorphDebuff: ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Snail morph debuff");
			Description.SetDefault("Transformed into a snail!");
			Main.debuff[Type] = true;
			Main.buffNoSave[Type] = true;
			canBeCleared = false;
			longerExpertDebuff = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<TerraStoryPlayer>().SnailMorphdebuff = true;
			player.mount.SetMount(MountType<Mounts.SnailMorphDB>(), player);
		}
	}
}
