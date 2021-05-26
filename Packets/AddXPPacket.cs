using System.Collections.Generic;
using System.IO;
using TerraStory.Content.Players;
using TerraStory.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Packets
{
    public static class AddXPPacket
    {
        public static void Read(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                PlayerCharacter character = Main.LocalPlayer.GetModPlayer<PlayerCharacter>();
                character.AddXp((int)reader.ReadInt32());
            }
        }

        public static bool Write(int scaled, int target)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = TerraStory.Mod.GetPacket();
                packet.Write((byte)Message.AddXp);
                packet.Write(scaled);
                packet.Write(target);
                packet.Send();
                return true;
            }

            return false;
        }
    }
}