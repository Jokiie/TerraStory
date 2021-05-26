using Terraria;
using Terraria.ModLoader;
using TerraStory.Dusts;
using TerraStory.NPCs;

namespace TerraStory.Buffs
{
    public class DeepCut : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Deep Cut");
            Description.SetDefault("it's bleed alot !");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = true;

        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerraStoryPlayer>().DeepCut = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 5);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 0.9f;
            Main.dust[num1].noGravity = false;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<TSNPC>().DeepCut = true;
            int num64 = Dust.NewDust(npc.position, npc.width, npc.height, 5);
             Main.dust[num64].velocity.Y += 0.5f;
             Main.dust[num64].velocity *= 0.25f;;
        }
    }
}