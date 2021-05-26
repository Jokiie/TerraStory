using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;

namespace TerraStory.Buffs
{
    public class SlowDebuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Slow");
            Description.SetDefault("Movement speed is reduced");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerraStoryPlayer>().Slow = true;
            player.moveSpeed -= 0.99f;
        }
    }
}