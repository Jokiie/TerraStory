using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Content.SFX;
using TerraStory.Enums;

namespace TerraStory.Buffs
{
    public class TooSharp : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Too Sharp!");
            Description.SetDefault("The Shurikens are too sharp! You must use gloves to use them painlessly.");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            longerExpertDebuff = true;
            

        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<TerraStoryPlayer>().TooSharp = true;
            int num1 = Dust.NewDust(player.position, player.width, player.height, 5);
            Main.dust[num1].scale = 0.9f;
            Main.dust[num1].velocity *= 0.5f;
            Main.dust[num1].noGravity = false;
        }
    }
}