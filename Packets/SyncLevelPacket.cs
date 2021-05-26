﻿
using System.Collections.Generic;
using System.IO;
using TerraStory.Content.Players;
using TerraStory.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace TerraStory.Packets
{
    public static class SyncLevelPacket
    {
        public static void Read(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
                Main.player[reader.ReadInt32()].GetModPlayer<PlayerCharacter>().Level = reader.ReadInt32();
        }

        public static void Write(int whoAmI, int level, bool force = false)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = TerraStory.Mod.GetPacket();
                packet.Write((byte)Message.SyncLevel);
                packet.Write(whoAmI);
                packet.Write(level);
                packet.Send();
            }
            else if (force)
            {
                ModPacket packet = TerraStory.Mod.GetPacket();
                packet.Write((byte)Message.SyncLevel);
                packet.Write(whoAmI);
                packet.Write(level);
                packet.Send();
            }
        }
    }
}