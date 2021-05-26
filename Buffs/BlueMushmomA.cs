using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Dusts;
using TerraStory.Enums;
using TerraStory.NPCs;

namespace TerraStory.Buffs
{
    public class BlueMushmomA : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("");
            Description.SetDefault("");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = false;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerraStoryPlayer>().BlueMushmomJump = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            
        }
    }
}