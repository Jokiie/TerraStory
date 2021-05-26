using System.Collections.Generic;
using System.IO;
using TerraStory.Content.Players;
using TerraStory.Enums;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace TerraStory.Packets
{
    public static class SyncStatsPacket
    {
        public static void Read(BinaryReader reader)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                PlayerCharacter character = Main.player[reader.ReadInt32()].GetModPlayer<PlayerCharacter>();
                character.Level = reader.ReadInt32();
                character.BaseStats[PlayerStats.HP] = reader.ReadInt32();
                character.BaseStats[PlayerStats.MP] = reader.ReadInt32();
                character.BaseStats[PlayerStats.STR] = reader.ReadInt32();
                character.BaseStats[PlayerStats.DEX] = reader.ReadInt32();
                character.BaseStats[PlayerStats.INT] = reader.ReadInt32();
                character.BaseStats[PlayerStats.LUK] = reader.ReadInt32();
            }
        }

        public static void Write(int whoAmI, int level, int HP, int MP, int STR, int DEX, int INT,int LUK)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                ModPacket packet = TerraStory.Mod.GetPacket();
                packet.Write((byte)Message.SyncStats);
                packet.Write(whoAmI);
                packet.Write(level);
                packet.Write(HP);
                packet.Write(MP);
                packet.Write(STR);
                packet.Write(DEX);
                packet.Write(INT);
                packet.Write(LUK);
                packet.Send();
            }
        }
    }
}