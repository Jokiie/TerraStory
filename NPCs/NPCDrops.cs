using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Items;
using TerraStory.Items.Accessories;

namespace TerraStory.NPCs
{
    public class NpcDrops : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.hardMode)
            {
                if (Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    if (Main.rand.Next(9) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MapleLeaf>(), Main.rand.Next(0, 1));
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MapleLeaf>(), Main.rand.Next(0, 1));
                        }
                    }
                }
            }
            else 
            {
                if (Main.player[Main.myPlayer].ZoneCorrupt)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MapleLeaf>(), 1);
                    }
                }

            }
            if (npc.type == NPCID.KingSlime && !Main.expertMode)
            {
                if (Main.rand.Next(4) == 0)
                {
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IronArrow"), Main.rand.Next(10, 100));
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Wolbi"), Main.rand.Next(10, 100));
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MightyBullet"), Main.rand.Next(10, 100));
                    }
                    if (npc.type == NPCID.KingSlime || !Main.expertMode)
                    {
                        switch (Main.rand.Next(5))
                        {
                            case 0:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MageNinjaHat"), 1);
                                break;
                            case 1:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NinjaRangerHelmet"), 1);
                                break;
                            case 2:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SummonerNinjaHood"), 1);
                                break;
                            case 3:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WarriorNinjaHelmet"), 1);
                                break;
                            case 4:
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("NinjaClaw"), 1);
                                break;
                        }
                    }
                }
                if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
                {
                    switch (Main.rand.Next(2))
                    {
                        case 0:

                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<CannoneerEmblem>(), 1);
                            break;
                        case 1:
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<ThiefEmblem>(), 1);
                            break;
                    }
                }
                // this is an example of how to make items drop custom npcs
                if (npc.type == mod.NPCType("GreenSlime"))
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<MapleLeaf>(), 1);
                        }
                    }
                }
            }
        }
    }
}