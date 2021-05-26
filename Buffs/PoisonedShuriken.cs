using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Dusts;
using TerraStory.Enums;
using TerraStory.NPCs;

namespace TerraStory.Buffs
{
    public class PoisonedShuriken : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Poisoned shurikens.");
            Description.SetDefault("You lose life and you feel sick");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerraStoryPlayer>().PoisonedShuriken = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, ModContent.DustType<PurpleDust>());
            Main.dust[num1].scale = 0.8f;
            Main.dust[num1].velocity *= 3f;
            Main.dust[num1].noGravity = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<TSNPC>().PoisonedShuriken = true;
            int num2 = Dust.NewDust(npc.position, npc.width, npc.height, ModContent.DustType<PurpleDust>());
            Main.dust[num2].scale = 0.8f;
            Main.dust[num2].velocity *= 3f;
            Main.dust[num2].noGravity = false;
        }
    }
}