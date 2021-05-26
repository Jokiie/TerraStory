using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Projectiles.Pets
{

    public class BlackDragon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Summons a Black Dragon!");
        }

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 70;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft *= 5;
            projectile.light = 0.5f;
            Main.projFrames[projectile.type] = 5;
            Main.projPet[projectile.type] = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
        }
        private int targetLoot()
        {
            int target = -1;
            float range = 400f;
            float playerRange = 700f;
            float targetDistance = float.MaxValue;

            for (int j = 0; j < 400; j++)
            {
                if (Main.item[j].active && Main.item[j].noGrabDelay == 0 && Main.item[j].owner == projectile.owner && ItemLoader.CanPickup(Main.item[j], Main.player[Main.item[j].owner]) && Main.player[Main.item[j].owner].ItemSpace(Main.item[j]))
                {
                    if (ItemID.Sets.NebulaPickup[Main.item[j].type])
                    {
                        continue;
                    }
                    if (Main.item[j].type == ItemID.Heart || Main.item[j].type == ItemID.CandyApple || Main.item[j].type == ItemID.CandyCane)
                    {
                        continue;
                    }
                    else if (Main.item[j].type == ItemID.Star || Main.item[j].type == ItemID.SoulCake || Main.item[j].type == ItemID.SugarPlum)
                    {
                        continue;
                    }


                    float distanceToPotential = projectile.Distance(Main.item[j].Center);
                    float playerDistanceToPotential = Main.player[projectile.owner].Distance(Main.item[j].Center);
                    if (playerDistanceToPotential < playerRange && distanceToPotential < targetDistance && distanceToPotential < range)
                    {
                        target = j;
                        targetDistance = distanceToPotential;
                    }
                }
            }

            return target;
        }

        private void pickup()
        {
            int defaultItemGrabRange = 150;
            Player thisPlayer = Main.player[projectile.owner];

            for (int j = 0; j < 400; j++)
            {
                if (Main.item[j].active && Main.item[j].noGrabDelay == 0 && Main.item[j].owner == projectile.owner && ItemLoader.CanPickup(Main.item[j], thisPlayer))
                {
                    int num = defaultItemGrabRange;//Player.defaultItemGrabRange;

                    if (new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height).Intersects(new Rectangle((int)Main.item[j].position.X, (int)Main.item[j].position.Y, Main.item[j].width, Main.item[j].height)))
                    {
                        if (projectile.owner == Main.myPlayer && (thisPlayer.inventory[thisPlayer.selectedItem].type != ItemID.None || thisPlayer.itemAnimation <= 0))
                        {
                            if (!ItemLoader.OnPickup(Main.item[j], thisPlayer))
                            {
                                Main.item[j] = new Item();
                                if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
                                }
                                continue;
                            }

                            if (ItemID.Sets.NebulaPickup[Main.item[j].type])
                            {
                                continue;
                            }
                            if (Main.item[j].type == ItemID.Heart || Main.item[j].type == ItemID.CandyApple || Main.item[j].type == ItemID.CandyCane)
                            {
                                continue;
                            }
                            else if (Main.item[j].type == ItemID.Star || Main.item[j].type == ItemID.SoulCake || Main.item[j].type == ItemID.SugarPlum)
                            {
                                continue;
                            }
                            else
                            {
                                Main.item[j] = thisPlayer.GetItem(projectile.owner, Main.item[j], false, false);
                                if (Main.netMode == NetmodeID.MultiplayerClient)
                                {
                                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, j, 0f, 0f, 0f, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }
        }


        public override void AI()
        {

            Entity target = Main.player[projectile.owner];

            if (!Main.player[projectile.owner].active)
            {
                projectile.active = false;
                return;
            }

            pickup();

            int num = 85;
            int lootTarget = targetLoot();

            if (lootTarget != -1)
            {
                target = Main.item[lootTarget];
                num = 0;
            }

            bool playerOutOfRangeLeftSide = false;
            bool playerOutOfRangeRightSide = false;
            bool playerBelowPet = false;
            bool flag4 = false;

            Player player = Main.player[projectile.owner];
            TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();

            if (Main.player[projectile.owner].dead)
            {
                modPlayer.BlackDragon = false;
            }
            if (modPlayer.BlackDragon)
            {
                projectile.timeLeft = 2;
            }


            if (target.position.X + (float)(target.width / 2) < projectile.position.X + (float)(projectile.width / 2) - (float)num)
            {
                playerOutOfRangeLeftSide = true;
            }
            else if (target.position.X + (float)(target.width / 2) > projectile.position.X + (float)(projectile.width / 2) + (float)num)
            {
                playerOutOfRangeRightSide = true;
            }
            {
                {
                    if (projectile.ai[1] == 0f)
                    {
                        int num36 = 1000;

                        if (Main.player[projectile.owner].rocketDelay2 > 0)
                        {
                            projectile.ai[0] = 1f;
                        }
                        Vector2 petCenter = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                        float distanceXToPlayer = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - petCenter.X;

                        float distanceYToPlayer = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - petCenter.Y;
                        float distanceToPlayer = (float)Math.Sqrt((double)(distanceXToPlayer * distanceXToPlayer + distanceYToPlayer * distanceYToPlayer));
                        if (distanceToPlayer > 2000f)
                        {
                            // teleport now!
                            projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
                            projectile.position.Y = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - (float)(projectile.height / 2);
                        }
                        else if (distanceToPlayer > (float)num36 || (Math.Abs(distanceYToPlayer) > 800f && (true || projectile.localAI[0] <= 0f)))
                        {
                            projectile.ai[0] = 1f;
                        }
                    }
                    // if teleporting.
                    if (/*projectile.type == 209 && */projectile.ai[0] != 0f)
                    {
                        if (Main.player[projectile.owner].velocity.Y == 0f && projectile.alpha >= 100)
                        {
                            // Teleport Directly to player
                            projectile.position.X = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - (float)(projectile.width / 2);
                            projectile.position.Y = Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height - (float)projectile.height;
                            projectile.ai[0] = 0f;
                            return;
                        }
                        projectile.velocity.X = 0f;
                        projectile.velocity.Y = 0f;
                        projectile.alpha += 5;
                        if (projectile.alpha > 255)
                        {
                            projectile.alpha = 255;
                            return;
                        }
                    }
                    else
                    {
                        Vector2 vector9 = Vector2.Zero;

                        if (projectile.ai[1] != 0f)
                        {
                            playerOutOfRangeLeftSide = false;
                            playerOutOfRangeRightSide = false;
                        }
                        float xAccelerationValue = 0.15f;
                        float xVelocityLimit = 10f;
                        if (playerOutOfRangeLeftSide)
                        {
                            if ((double)projectile.velocity.X > -3.5)
                            {
                                projectile.velocity.X = projectile.velocity.X - xAccelerationValue;
                            }
                            else
                            {
                                projectile.velocity.X = projectile.velocity.X - xAccelerationValue * 0.50f;
                            }
                        }
                        else if (playerOutOfRangeRightSide)
                        {
                            if ((double)projectile.velocity.X < 3.5)
                            {
                                projectile.velocity.X = projectile.velocity.X + xAccelerationValue;
                            }
                            else
                            {
                                projectile.velocity.X = projectile.velocity.X + xAccelerationValue * 0.25f;
                            }
                        }
                        else
                        {
                            projectile.velocity.X = projectile.velocity.X * 0.9f;
                            if (projectile.velocity.X >= -xAccelerationValue && projectile.velocity.X <= xAccelerationValue)
                            {
                                projectile.velocity.X = 0f;
                            }
                        }
                        if (playerOutOfRangeLeftSide || playerOutOfRangeRightSide)
                        {
                            int petPositionTileX = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
                            int petPositionTileY = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16;

                            if (playerOutOfRangeLeftSide)
                            {
                                petPositionTileX--;
                            }
                            if (playerOutOfRangeRightSide)
                            {
                                petPositionTileX++;
                            }
                            petPositionTileX += (int)projectile.velocity.X;
                            if (WorldGen.SolidTile(petPositionTileX, petPositionTileY))
                            {
                                flag4 = true;
                            }
                        }
                        if (Main.player[projectile.owner].position.Y + (float)Main.player[projectile.owner].height - 8f > projectile.position.Y + (float)projectile.height)
                        {
                            playerBelowPet = true;
                        }
                        Collision.StepUp(ref projectile.position, ref projectile.velocity, projectile.width, projectile.height, ref projectile.stepSpeed, ref projectile.gfxOffY, 1, false, 0);
                        if (projectile.velocity.Y == 0f)
                        {
                            if (!playerBelowPet && (projectile.velocity.X < 0f || projectile.velocity.X > 0f))
                            {
                                int num102 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
                                int j3 = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16 + 1;
                                if (playerOutOfRangeLeftSide)
                                {
                                    num102--;
                                }
                                if (playerOutOfRangeRightSide)
                                {
                                    num102++;
                                }
                                WorldGen.SolidTile(num102, j3);
                            }
                            if (flag4)
                            {
                                int num103 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
                                int num104 = (int)(projectile.position.Y + (float)projectile.height) / 16 + 1;
                                if (WorldGen.SolidTile(num103, num104) || Main.tile[num103, num104].halfBrick() || Main.tile[num103, num104].slope() > 0 /*|| projectile.type == 200*/)
                                {

                                    {
                                        try
                                        {
                                            num103 = (int)(projectile.position.X + (float)(projectile.width / 2)) / 16;
                                            num104 = (int)(projectile.position.Y + (float)(projectile.height / 2)) / 16;
                                            if (playerOutOfRangeLeftSide)
                                            {
                                                num103--;
                                            }
                                            if (playerOutOfRangeRightSide)
                                            {
                                                num103++;
                                            }
                                            num103 += (int)projectile.velocity.X;
                                            if (!WorldGen.SolidTile(num103, num104 - 1) && !WorldGen.SolidTile(num103, num104 - 2))
                                            {
                                                projectile.velocity.Y = -5.1f;
                                            }
                                            else if (!WorldGen.SolidTile(num103, num104 - 2))
                                            {
                                                projectile.velocity.Y = -7.1f;
                                            }
                                            else if (WorldGen.SolidTile(num103, num104 - 5))
                                            {
                                                projectile.velocity.Y = -11.1f;
                                            }
                                            else if (WorldGen.SolidTile(num103, num104 - 4))
                                            {
                                                projectile.velocity.Y = -10.1f;
                                            }
                                            else
                                            {
                                                projectile.velocity.Y = -9.1f;
                                            }
                                        }
                                        catch
                                        {
                                            projectile.velocity.Y = -9.1f;
                                        }
                                    }

                                }
                            }

                        }
                        if (projectile.velocity.X > xVelocityLimit)
                        {
                            projectile.velocity.X = xVelocityLimit;
                        }
                        if (projectile.velocity.X < -xVelocityLimit)
                        {
                            projectile.velocity.X = -xVelocityLimit;
                        }
                        if (projectile.velocity.X < 0f)
                        {
                            projectile.direction = -1;
                        }
                        if (projectile.velocity.X > 0f)
                        {
                            projectile.direction = 1;
                        }
                        if (projectile.velocity.X > xAccelerationValue && playerOutOfRangeRightSide)
                        {
                            projectile.direction = 1;
                        }
                        if (projectile.velocity.X < -xAccelerationValue && playerOutOfRangeLeftSide)
                        {
                            projectile.direction = -1;
                        }

                        if ((double)projectile.velocity.X > 0.5)
                        {
                            projectile.spriteDirection = -1;
                        }
                        else if ((double)projectile.velocity.X < -0.5)
                        {
                            projectile.spriteDirection = 1;
                        }


                        //else if (projectile.type == 209)
                        {
                            if (projectile.alpha > 0)
                            {
                                projectile.alpha -= 5;
                                if (projectile.alpha < 0)
                                {
                                    projectile.alpha = 0;
                                }
                            }
                            if (projectile.velocity.Y == 0f)
                            {
                                if (projectile.velocity.X == 0f)
                                {
                                    projectile.frameCounter++;
                                    if (projectile.frameCounter >= 8) //amount of game frames before the frame changes
                                    {
                                        projectile.frameCounter = 0;
                                        projectile.frame = (projectile.frame + 1) % 5; //total amount of frames
                                    }
                                }
                                else if (projectile.velocity.X < -0.1 || projectile.velocity.X > 0.1)
                                {
                                    projectile.frameCounter++;
                                    if (projectile.frameCounter >= 8) //amount of game frames before the frame changes
                                    {
                                        projectile.frameCounter = 0;
                                        projectile.frame = (projectile.frame + 1) % 5; //total amount of frames
                                    }
                                }
                                else
                                {
                                    projectile.frameCounter++;
                                    if (projectile.frameCounter >= 8) //amount of game frames before the frame changes
                                    {
                                        projectile.frameCounter = 0;
                                        projectile.frame = (projectile.frame + 1) % 5; //total amount of frames
                                    }
                                }
                            }
                            else
                            {
                                projectile.frameCounter++;
                                if (projectile.frameCounter >= 8) //amount of game frames before the frame changes
                                {
                                    projectile.frameCounter = 0;
                                    projectile.frame = (projectile.frame + 1) % 5; //total amount of frames
                                }
                                else
                                {
                                    projectile.frameCounter++;
                                    if (projectile.frameCounter >= 8) //amount of game frames before the frame changes
                                    {
                                        projectile.frameCounter = 0;
                                        projectile.frame = (projectile.frame + 1) % 5; //total amount of frames
                                    }
                                }
                            }
                            projectile.velocity.Y = projectile.velocity.Y + 0.4f;
                            if (projectile.velocity.Y > 10f)
                            {
                                projectile.velocity.Y = 10f;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}