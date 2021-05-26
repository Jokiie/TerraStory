using System;
using TerraStory.Content.Players;
using TerraStory.Packets;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerraStory.NPCs.Bosses;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TerraStory.NPCs
{
    public class TSNPC : GlobalNPC
    {
        public string name = "";

        public bool TooSharp = false;

        public bool DeepCut = false;

        public bool PoisonedShuriken = false;

        public int whoAmI;

        public bool active;

        public static int LastKilledMob = -1;

        public bool Initialized { get; set; }

        public override bool InstancePerEntity => true;

        public bool stairFall = false;

        public static float gravity = 0.3f;

        public override void ResetEffects(NPC npc)
        {
            TooSharp = false;
            DeepCut = false;
            PoisonedShuriken = false;
            stairFall = false;
        }


        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            if (npc.GetGlobalNPC<TSNPC>().TooSharp)
            {
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 4;
                }
            }
            if (npc.GetGlobalNPC<TSNPC>().PoisonedShuriken)
            {
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 4;
                }
            }
            if (npc.GetGlobalNPC<TSNPC>().DeepCut)
            {
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 4;
                }
            }
        }
        public static int GetLevel(NPC npc)
        {
            return Math.Min (Main.expertMode ? (npc.lifeMax + npc.defense + npc.damage) / 100 : (npc.lifeMax + npc.defense + npc.damage) / 10, npc.boss ? (npc.lifeMax + npc.defense + npc.damage) / 500 : (npc.lifeMax + npc.defense + npc.damage) / 250);
        }

        public static string ReturnMouseOverNPCText(NPC npc)
        {
            _ = npc.GetGlobalNPC<TSNPC>();

            string Text = npc.FullName + " Lv:" + GetLevel(npc) + "\n";
            object obj = Text;
            Text = string.Concat(obj, "HP:", npc.life, "/", npc.lifeMax, "\n");
            obj = Text;
            return string.Concat(obj, "Damage: ", npc.damage, "%) Defense: ", npc.defense);
        }

        public override void NPCLoot(NPC npc)
        {

            TSNPC tsNpc = npc.GetGlobalNPC<TSNPC>();

            if (npc.lifeMax < 10) return;
            if (npc.friendly) return;
            if (npc.townNPC) return;

            int level = GetLevel(npc);
            //GetLevel(npc.netID);

            Player player = Array.Find(Main.player, p => p.active);
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                player = Main.LocalPlayer;
            }
            else if (Main.player[npc.target].active)
            {
                player = Main.player[npc.target];
            }
            else
            {
                PlayerCharacter c = player.GetModPlayer<PlayerCharacter>();
                foreach (Player p in Main.player)
                    if (p != null)
                        if (p.active)
                            if (p.GetModPlayer<PlayerCharacter>() != null)
                                if (p.GetModPlayer<PlayerCharacter>().Level > c.Level)
                                    player = p;
            }

            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();
            int life = npc.type == NPCID.SolarCrawltipedeTail || npc.type == NPCID.SolarCrawltipedeBody || npc.type == NPCID.SolarCrawltipedeHead
            ? npc.lifeMax / 8
            : npc.lifeMax;
            // int defFactor = npc.lifeMax / 5 ;
            int NpcDef = npc.lifeMax / 5; //npc.defense < 0 ? 1 : npc.defense * life / (character.Level + 10); //
            int scaled = Main.expertMode ? (int)(NpcDef * 0.5) : NpcDef;


            if (!AddXPPacket.Write(scaled, npc.target))
                character.AddXp(scaled);

            if (level < Math.Min(character.Level - 17, 70)) return;

            /*float dps = Math.Min((float)(Math.Pow(1.04, Math.Min(130, character.Level)) * 9f), (float)(Math.Pow(1.023, level) * 15) + 14);
            int assumedDef = !Main.hardMode ? 5 : character.Level / 3;*/
        }

        public int GetPlayerLevel()
        {
            try
            {
                Player player = Main.netMode == NetmodeID.Server ? Main.player[0] : Main.player[Main.myPlayer];
                return player.GetModPlayer<PlayerCharacter>().Level;
            }
            catch (Exception)
            {
                return 20;
            }

        }

        public override void SetDefaults(NPC npc)
        {
            if (Main.netMode == NetmodeID.SinglePlayer && !TerraStory.PlayerEnteredWorld)
                return;
            if (npc.boss || npc.townNPC || npc.friendly || Main.netMode == NetmodeID.MultiplayerClient)
                return;


            int playerLevel = Main.netMode == NetmodeID.SinglePlayer ? GetPlayerLevel() : 20;

            int npcLevel = GetLevel(npc);

            npc.damage = (int)Math.Round(npc.damage * (npcLevel / 30f + 0.4f + playerLevel * 0.05f));

            npc.lifeMax = (int)Math.Round(npc.lifeMax * (npcLevel / 30f + 0.4f + playerLevel * 0.050f));

            npc.life = npc.lifeMax;

            npc.defense = (int)Math.Round(npc.defense * (npcLevel / 160f + 1f));

            if (Main.expertMode)
            {
                //npc.lifeMax = (int)(npc.lifeMax * 1.3);
                npc.life = (int)(npc.life * 1.3);
                npc.damage = (int)(npc.damage * 2);
            }
        }

        public bool Active = false;

        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {

                if (player.GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    spawnRate = (int)(spawnRate * 2f);
                    maxSpawns = (int)(maxSpawns * 2f);

                }
                else
                {
                    spawnRate = (int)(spawnRate * 0.75f);
                    maxSpawns = (int)(spawnRate * 0.75f);
                }
            }
        }
        /*
        public override void EditSpawnRange(Player player, ref int spawnRangeX, ref int spawnRangeY, ref int safeRangeX, ref int safeRangeY)
        {
            spawnRangeX = spawnRangeX / 2;
            spawnRangeY = spawnRangeY / 2;
            safeRangeX = safeRangeX / 2;
            safeRangeY = safeRangeY / 2;
        }*/

        public override void FindFrame(NPC npc, int frameHeight)
        {
            if (npc.type == NPCID.BlueSlime && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime && npc.type == NPCID.BlackSlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.60f;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.IceSlime)
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.65f;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.LavaSlime)
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.80f;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.SandSlime)
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.65f;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.MotherSlime && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 1f;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.BabySlime)
            {
                npc.scale = 0.60f / 2;
                Main.npcFrameCount[npc.type] = 4;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.BigCrimslime && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 1f;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.Crimslime && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.80f;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.LittleCrimslime)
            {
                npc.scale = 0.70f / 2;
                Main.npcFrameCount[npc.type] = 4;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.CorruptSlime && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.95f;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.Slimer && (!(npc.type == NPCID.Pinky && npc.type == NPCID.Slimeling && npc.type == NPCID.LittleCrimslime && npc.type == NPCID.BabySlime)))
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.80f;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.Slimeling)
            {
                npc.scale = 0.70f / 2;
                Main.npcFrameCount[npc.type] = 4;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.Pinky)
            {
                npc.scale = 20f;
                Main.npcFrameCount[npc.type] = 4;
                npc.spriteDirection = npc.direction;
            }
            else if (npc.type == NPCID.BlackSlime)
            {
                Main.npcFrameCount[npc.type] = 4;
                npc.scale = 0.80f;
                npc.spriteDirection = npc.direction;
            }
            if (npc.type == NPCID.JungleBat)
            {
                npc.scale = 0.60f;
            }
        }
        public override void AI(NPC npc)
        {
            int frameHeight = npc.frame.Y * Main.npcFrameCount[npc.type]; // 42

            if (npc.type == NPCID.BlueSlime &&
                npc.type == NPCID.IceSlime &&
                npc.type == NPCID.SandSlime &&
                npc.type == NPCID.MotherSlime &&
                npc.type == NPCID.LavaSlime &&
                npc.type == NPCID.Crimslime &&
                npc.type == NPCID.CorruptSlime)
            {
                npc.spriteDirection = npc.direction;
                npc.frameCounter -= -3.9f;
                npc.frameCounter %= Main.npcFrameCount[npc.type];
                int frame = (int)npc.frameCounter;
                npc.frame.Y = frame * frameHeight;
            }
                if (npc.type == ModContent.NPCType<GreenSnail>() || npc.type == ModContent.NPCType<RedSnail>() || npc.type == ModContent.NPCType<BlueSnail>())
            {
                if (npc.ai[3] != 0f)
                {
                    npc.scale = npc.ai[3];
                    int num16 = (int)(40f * npc.scale);
                    int num17 = (int)(40f * npc.scale);
                    if (num16 != npc.width)
                    {
                        npc.position.X = npc.position.X + (float)(npc.width / 2) - (float)num16 - 2f;
                        npc.width = num16;
                    }
                    if (num17 != npc.height)
                    {
                        npc.position.Y = npc.position.Y + (float)npc.height - (float)num17;
                        npc.height = num17;
                    }
                }
                if (npc.ai[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    npc.ai[3] = (float)Main.rand.Next(80, 111) * 0.01f;
                    npc.netUpdate = true;
                }

                float num18 = 0.3f;
                if (npc.ai[0] == 0f)
                {
                    npc.TargetClosest();
                    npc.directionY = 1;
                    npc.ai[0] = 1f;
                    if (npc.direction > 0)
                    {
                        npc.spriteDirection = 1;
                    }
                }
                bool PeutSeCollerSurUnMur = false;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    if (npc.ai[2] == 0f && Main.rand.Next(7200) == 0)
                    {
                        npc.ai[2] = 2f;
                        npc.netUpdate = true;
                    }
                    if (!npc.collideX && !npc.collideY)
                    {
                        npc.localAI[3] += 1f;
                        if (npc.localAI[3] > 5f)
                        {
                            npc.ai[2] = 2f;
                            npc.netUpdate = true;
                        }
                    }
                    else
                    {
                        npc.localAI[3] = 0f;
                    }
                }
                if (npc.ai[2] > 0f)
                {
                    npc.ai[1] = 0f;
                    npc.ai[0] = 1f;
                    npc.directionY = 1;
                    if (npc.velocity.Y > num18)
                    {
                        npc.rotation += (float)npc.direction * 0.1f;
                    }
                    else
                    {
                        npc.rotation = 0f;
                    }
                    npc.spriteDirection = npc.direction;
                    npc.velocity.X = num18 * (float)npc.direction;
                    npc.noGravity = false;
                    int num19 = (int)(npc.Center.X + (float)(npc.width / 2 * -npc.direction)) / 16;
                    int num20 = (int)(npc.position.Y + (float)npc.height + 8f) / 16;
                    if (Main.tile[num19, num20] != null && !Main.tile[num19, num20].topSlope() && npc.collideY)
                    {
                        npc.ai[2] -= 1f;
                    }
                    num20 = (int)(npc.position.Y + (float)npc.height - 4f) / 16;
                    num19 = (int)(npc.Center.X + (float)(npc.width / 2 * npc.direction)) / 16;
                    if (Main.tile[num19, num20] != null && Main.tile[num19, num20].bottomSlope())
                    {
                        npc.direction *= -1;
                    }
                    if (npc.collideX && npc.velocity.Y == 0f)
                    {
                        PeutSeCollerSurUnMur = true;
                        npc.ai[2] = 0f;
                        npc.directionY = -1;
                        npc.ai[1] = 1f;
                    }
                    if (npc.velocity.Y == 0f)
                    {
                        if (npc.localAI[1] == npc.position.X)
                        {
                            npc.localAI[2] += 1f;
                            if (npc.localAI[2] > 10f)
                            {
                                npc.direction = 1;
                                npc.velocity.X = (float)npc.direction * num18;
                                npc.localAI[2] = 0f;
                            }
                        }
                        else
                        {
                            npc.localAI[2] = 0f;
                            npc.localAI[1] = npc.position.X;
                        }
                    }
                }
                if (npc.ai[2] != 0f)
                {
                    return;
                }
                npc.noGravity = true;
                if (npc.ai[1] == 0f)
                {
                    if (npc.collideY)
                    {
                        npc.ai[0] = 2f;
                    }
                    if (!npc.collideY && npc.ai[0] == 2f)
                    {
                        npc.direction = -npc.direction; // ne pas changer
                        npc.ai[1] = 1f;
                        npc.ai[0] = 1f;
                    }
                    if (npc.collideX)
                    {
                        npc.directionY = -npc.directionY; // ne pas changer
                        npc.ai[1] = 1f;
                    }
                }
                else
                {
                    if (npc.collideX)
                    {
                        npc.ai[0] = 2f;
                    }
                    if (!npc.collideX && npc.ai[0] == 2f)
                    {
                        npc.directionY = -npc.directionY; //ne pas changer
                        npc.ai[1] = 0f;
                        npc.ai[0] = 1f;
                    }
                    if (npc.collideY)
                    {
                        npc.direction = -npc.direction; //ne pas changer
                        npc.ai[1] = 0f;
                    }
                }
                if (!PeutSeCollerSurUnMur)
                {
                    float num21 = npc.rotation;
                    if (npc.directionY < 0)
                    {
                        if (npc.direction < 0)
                        {
                            if (npc.collideX)
                            {
                                npc.rotation = 1.57f;
                                npc.spriteDirection = -1; // ->
                            }
                            else if (npc.collideY)
                            {
                                npc.rotation = 3.14f;
                                npc.spriteDirection = 1; //<-
                            }
                        }
                        else if (npc.collideY)
                        {
                            npc.rotation = 3.14f;
                            npc.spriteDirection = -1; // ->
                        }
                        else if (npc.collideX)
                        {
                            npc.rotation = 4.71f;
                            npc.spriteDirection = 1; // <-
                        }
                    }
                    else if (npc.direction < 0)
                    {
                        if (npc.collideY)
                        {
                            npc.rotation = 0f;
                            npc.spriteDirection = -1; // ->
                        }
                        else if (npc.collideX)
                        {
                            npc.rotation = 1.57f;
                            npc.spriteDirection = 1; // <-
                        }
                    }
                    else if (npc.collideX)
                    {
                        npc.rotation = 4.71f;
                        npc.spriteDirection = -1; // ->
                    }
                    else if (npc.collideY)
                    {
                        npc.rotation = 0f;
                        npc.spriteDirection = 1; // <-
                    }
                    float num22 = npc.rotation;
                    npc.rotation = num21;
                    if ((double)npc.rotation > 6.28)
                    {
                        npc.rotation -= 6.28f;
                    }
                    if (npc.rotation < 0f)
                    {
                        npc.rotation += 6.28f;
                    }
                    float num23 = Math.Abs(npc.rotation - num22);
                    float num24 = 0.1f;
                    if (npc.rotation > num22)
                    {
                        if ((double)num23 > 3.14)
                        {
                            npc.rotation += num24;
                        }
                        else
                        {
                            npc.rotation -= num24;
                            if (npc.rotation < num22)
                            {
                                npc.rotation = num22;
                            }
                        }
                    }
                    if (npc.rotation < num22)
                    {
                        if ((double)num23 > 3.14)
                        {
                            npc.rotation -= num24;
                        }
                        else
                        {
                            npc.rotation += num24;
                            if (npc.rotation > num22)
                            {
                                npc.rotation = num22;
                            }
                        }
                    }
                }
                npc.velocity.X = num18 * (float)npc.direction;
                npc.velocity.Y = num18 * (float)npc.directionY;
            }
        }
        public void Collision_WalkDownSlopes()
        {
            NPC npc = Main.npc[Main.myPlayer];
            _ = npc.velocity;
            Vector4 vector = Collision.WalkDownSlope(npc.position, npc.velocity, npc.width, npc.height, gravity);
            npc.position.X = vector.X;
            npc.position.Y = vector.Y;
            npc.velocity.X = vector.Z;
            npc.velocity.Y = vector.W;
        }
        public void Collision_MoveWhileDry()
        {
            NPC npc = Main.npc[Main.myPlayer];
            if (Collision.up)
            {
                npc.velocity.Y = 0.01f;
            }
            if (npc.oldVelocity.X != npc.velocity.X)
            {
                npc.collideX = true;
            }
            if (npc.oldVelocity.Y != npc.velocity.Y)
            {
                npc.collideY = true;
            }
            npc.oldPosition = npc.position;
            npc.oldDirection = npc.direction;
            npc.position += npc.velocity;
        }

        public void ApplyTileCollision(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
           Collision_MoveNormal(fall, cPosition, cWidth, cHeight);
        }

        public void Collision_MoveSlopesAndStairFall(bool fall)
        {
            NPC npc = Main.npc[Main.myPlayer];
            if (fall)
            {
                stairFall = true;
            }
            if (npc.aiStyle == 7)
            {
                int num = (int)npc.Center.X / 16;
                int num2 = (int)npc.position.Y / 16;
                if (WorldGen.InWorld(num, num2))
                {
                    int num3 = 16;
                    bool flag = false;
                    if (Main.tile[num, num2] != null && Main.tile[num, num2].active() && Main.tileSolid[Main.tile[num, num2].type])
                    {
                        flag = true;
                    }
                    if (!Main.dayTime || Main.eclipse)
                    {
                        flag = true;
                    }
                    else
                    {
                        int num4 = (int)(npc.position.Y + (float)npc.height) / 16;
                        if (npc.homeTileY - num4 > num3)
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        if ((npc.position.Y + (float)npc.height - 8f) / 16f < (float)npc.homeTileY)
                        {
                            stairFall = true;
                        }
                        else
                        {
                            stairFall = false;
                        }
                    }
                }
            }
            GetTileCollisionParameters(out Vector2 cPosition, out int cWidth, out int cHeight);
            Vector2 vector = npc.position - cPosition;
            Vector4 vector2 = Collision.SlopeCollision(cPosition, npc.velocity, cWidth, cHeight, gravity, stairFall);
            if (Collision.stairFall)
            {
                stairFall = true;
            }
            else if (!fall)
            {
                stairFall = false;
            }
            if (Collision.stair && Math.Abs(vector2.Y - npc.position.Y) > 8f)
            {
                npc.gfxOffY -= vector2.Y - npc.position.Y;
                npc.stepSpeed = 2f;
            }
            npc.position.X = vector2.X;
            npc.position.Y = vector2.Y;
            npc.velocity.X = vector2.Z;
            npc.velocity.Y = vector2.W;
            npc.position += vector;
        }

        public void Collision_MoveSnailOnSlopes()
        {
            NPC npc = Main.npc[Main.myPlayer];
            Vector4 vector = Collision.SlopeCollision(npc.position, npc.velocity, npc.width, npc.height, gravity);
            if (npc.position.X != vector.X || npc.position.Y != vector.Y)
            {
                if (npc.ai[2] == 0f && npc.velocity.Y > 0f && ((npc.direction < 0 && npc.rotation == 1.57f && npc.spriteDirection == 1) || (npc.direction > 0 && npc.rotation == 4.71f && npc.spriteDirection == -1)))
                {
                    npc.direction *= -npc.direction;
                }
                npc.ai[2] = 2f;
                npc.directionY = 1;
                npc.rotation = 0f;
            }
            npc.position.X = vector.X;
            npc.position.Y = vector.Y;
            npc.velocity.X = vector.Z;
            npc.velocity.Y = vector.W;
        }

        public void Collision_MoveNormal(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            npc.velocity = Collision.TileCollision(cPosition, npc.velocity, cWidth, cHeight, fall, fall);
        }

        public void Collision_MoveSandshark(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            npc.velocity = Collision.AdvancedTileCollision(TileID.Sets.ForAdvancedCollision.ForSandshark, cPosition, npc.velocity, cWidth, cHeight, fall, fall);
        }

        public void Collision_MoveSolarSroller(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            npc.velocity = Collision.TileCollision(cPosition, npc.velocity, cWidth, cHeight, fall, fall);
            if (npc.ai[0] != 6f || !(npc.velocity != npc.oldVelocity))
            {
                return;
            }
            npc.ai[2] -= 1f;
            npc.ai[3] = 1f;
            if (npc.ai[2] > 0f)
            {
                if (npc.velocity.X != 0f && npc.velocity.X != npc.oldVelocity.X)
                {
                    npc.velocity.X = (0f - npc.oldVelocity.X) * 0.9f;
                    npc.direction *= -1;
                }
                if (npc.velocity.Y != 0f && npc.velocity.Y != npc.oldVelocity.Y)
                {
                    npc.velocity.Y = (0f - npc.oldVelocity.Y) * 0.9f;
                }
            }
        }

        public void Collision_MoveStardustCell(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            npc.velocity = Collision.TileCollision(cPosition, npc.velocity, cWidth, cHeight, fall, fall);
            if (npc.velocity != npc.oldVelocity)
            {
                if (npc.velocity.X != 0f && npc.velocity.X != npc.oldVelocity.X)
                {
                    npc.velocity.X = (0f - npc.oldVelocity.X) * 0.8f;
                }
                if (npc.velocity.Y != 0f && npc.velocity.Y != npc.oldVelocity.Y)
                {
                    npc.velocity.Y = (0f - npc.oldVelocity.Y) * 0.8f;
                }
            }
        }

        public void Collision_MoveBlazingWheel()
        {
            NPC npc = Main.npc[Main.myPlayer];
            Vector2 position = new Vector2(npc.position.X + (float)(npc.width / 2), npc.position.Y + (float)(npc.height / 2));
            int num = 12;
            int num2 = 12;
            position.X -= num / 2;
            position.Y -= num2 / 2;
            npc.velocity = Collision.noSlopeCollision(position, npc.velocity, num, num2, fallThrough: true, fall2: true);
        }

        public void Collision_MoveWaterOrLavaOld(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            Vector2 velocity = npc.velocity;
            npc.velocity = Collision.TileCollision(cPosition, npc.velocity, cWidth, cHeight, fall, fall);
            if (Collision.up)
            {
                npc.velocity.Y = 0.01f;
            }
            Vector2 vector = npc.velocity * 0.5f;
            if (npc.velocity.X != velocity.X)
            {
                vector.X = npc.velocity.X;
                npc.collideX = true;
            }
            if (npc.velocity.Y != velocity.Y)
            {
                vector.Y = npc.velocity.Y;
                npc.collideY = true;
            }
            npc.oldPosition = npc.position;
            npc.oldDirection = npc.direction;
            npc.position += vector;
        }

        public void Collision_MoveHoneyOld(bool fall, Vector2 cPosition, int cWidth, int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            Vector2 velocity = npc.velocity;
            npc.velocity = Collision.TileCollision(cPosition, npc.velocity, cWidth, cHeight, fall, fall);
            if (Collision.up)
            {
                npc.velocity.Y = 0.01f;
            }
            Vector2 vector = npc.velocity * 0.25f;
            if (npc.velocity.X != velocity.X)
            {
                vector.X = npc.velocity.X;
                npc.collideX = true;
            }
            if (npc.velocity.Y != velocity.Y)
            {
                vector.Y = npc.velocity.Y;
                npc.collideY = true;
            }
            npc.oldPosition = npc.position;
            npc.oldDirection = npc.direction;
            npc.position += vector;
        }

        public void Collision_MoveWhileWet(Vector2 oldDryVelocity, float Slowdown = 0.5f)
        {
            NPC npc = Main.npc[Main.myPlayer];
            if (Collision.up)
            {
                npc.velocity.Y = 0.01f;
            }
            Vector2 vector = npc.velocity * Slowdown;
            if (npc.velocity.X != oldDryVelocity.X)
            {
                vector.X = npc.velocity.X;
                npc.collideX = true;
            }
            if (npc.velocity.Y != oldDryVelocity.Y)
            {
                vector.Y = npc.velocity.Y;
                npc.collideY = true;
            }
            npc.oldPosition = npc.position;
            npc.oldDirection = npc.direction;
            npc.position += vector;
        }

        public void GetTileCollisionParameters(out Vector2 cPosition, out int cWidth, out int cHeight)
        {
            NPC npc = Main.npc[Main.myPlayer];
            cPosition = npc.position;
            cWidth = npc.width;
            cHeight = npc.height;
            if (cHeight != npc.height)
            {
                cPosition.Y += npc.height - cHeight;
            }
        }
        public bool Collision_DecideFallThroughPlatforms()
        {
            NPC npc = Main.npc[Main.myPlayer];
            bool result = false;
            return result;
        }
    }
}