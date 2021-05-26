using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using TerraStory.Buffs;
using TerraStory.Dusts;

namespace TerraStory.NPCs.Bosses
{

	[AutoloadBossHead]
	public class Mano : ModNPC
	{

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mano");
			Main.npcFrameCount[npc.type] = 8;
		}
		public override void SetDefaults()
		{
            npc.width = 20;
			npc.height = 100;
			npc.boss = true;
			npc.aiStyle = -1;
            npc.npcSlots = 3f;
			npc.scale = 0.90f;
			npc.lifeMax = 700;
			npc.damage = 10;
			npc.defense = 0;
			npc.knockBackResist = 0f;
			npc.value = Item.buyPrice(gold: 1);
            npc.noGravity = false;
			npc.lavaImmune = true;
			npc.buffImmune[BuffID.Slow] = true;
			npc.buffImmune[BuffID.Confused] = true;
			npc.HitSound = mod.GetLegacySoundSlot(SoundType.NPCHit, "Sounds/NPCHit/ManoHit");
			npc.DeathSound = mod.GetLegacySoundSlot(SoundType.NPCKilled, "Sounds/NPCKilled/ManoDie");
			music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/RedWitch");

			bossBag = mod.ItemType("ManoTreasureBag");
		}
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = (int)(npc.lifeMax * bossLifeScale);
			npc.damage = (int)(npc.damage * 0.5f);
		}

		public override void NPCLoot()
		{
			World.downedMano = true;
			if (Main.expertMode)
			{
				npc.DropBossBags();
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
				Item.NewItem((int)npc.position.X + 20, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
				Item.NewItem((int)npc.position.X + 50, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
				Item.NewItem((int)npc.position.X + 80, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
				Item.NewItem((int)npc.position.X - 20, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
				Item.NewItem((int)npc.position.X - 50, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
				Item.NewItem((int)npc.position.X - 80, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
				Item.NewItem((int)npc.position.X - 110, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
				Item.NewItem((int)npc.position.X + 110, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
			}
			else
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GoldCoin, Main.rand.Next(0, 1));
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.Mushroom, Main.rand.Next(1, 20));
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
                Item.NewItem((int)npc.position.X + 20, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
                Item.NewItem((int)npc.position.X + 50, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RedSnailShell"), 1);
                Item.NewItem((int)npc.position.X + 80, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
                Item.NewItem((int)npc.position.X - 20, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
                Item.NewItem((int)npc.position.X - 50, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlueSnailShell"), 1);
                Item.NewItem((int)npc.position.X - 80, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
                Item.NewItem((int)npc.position.X - 110, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
                Item.NewItem((int)npc.position.X + 110, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GreenSnailShell"), 1);
                // Can add : if(Main.Hardmode) to add hardmode items
            }
		}
		public override void BossLoot(ref string name, ref int potionType)
		{
			potionType = ItemID.LesserHealingPotion;
		}

		public bool moving = false;

		public override void FindFrame(int frameHeight)
		{
            // This makes the sprite flip horizontally in conjunction with the npc.direction.
            npc.spriteDirection = npc.direction;
            // Determines the animation speed . positive value ex: 0.5f = higher speed
            npc.frameCounter -= -11.9f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
		{
			scale = 1.5f;
			return null;
		}
        public override void PostAI()
        {
            npc.TargetClosest();
            Player player = Main.player[Main.myPlayer];
            float distance = npc.Distance(Main.player[npc.target].Center);
            if (distance >= 300 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
            {
                Main.player[npc.target].AddBuff(BuffID.Slow, 120, true);
            }
            if (distance == 300 && !Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
            {
                switch (Main.rand.Next(4))
                {
                    case 0:
                        Main.NewText("Mano : How do you feel when you're a snail? ", Color.Red, false);
                        break;
                    case 1:
                        Main.NewText("Mano : When you are a snail, you are immune to slow! oh i just gave you an idea ... ", Color.Red, false);
                        break;
                    case 2:
                        Main.NewText("Mano : Wanna do a race? ", Color.Red, false);
                        break;
                    default:
                        Main.NewText("Mano : Hehe..If you wanna be yourself again, get to me! ", Color.Red, false);
                        break;
                }
                if (!Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                {
                    npc.velocity.Y *= 2f;
                }
            }
        }
        public override void AI()
        {
            int DespawnTimer = 0;
            for (int Cible = 0; Cible < 255; Cible++)
            {
                if (Main.player[Cible].active && !Main.player[Cible].dead && (npc.Center - Main.player[Cible].Center).Length() < 1000f)
                {
                    DespawnTimer++;
                }
            }
            if (npc.target < 0 || npc.target == 225 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest();
            }
            if (Main.player[npc.target].dead && Main.expertMode)
            {
                if ((double)npc.position.Y < Main.worldSurface * 16.0 + 2000.0)
                {
                    npc.velocity.Y += 0.04f;
                }
                if (npc.position.X < (float)(Main.maxTilesX * 8))
                {
                    npc.velocity.X -= 0.04f;
                }
                else
                {
                    npc.velocity.X += 0.04f;
                }
                if (npc.timeLeft > 10)
                {
                    npc.timeLeft = 10;
                }
            }
            if (npc.ai[3] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                npc.ai[3] = (float)Main.rand.Next(80, 111) * 0.01f;
                npc.netUpdate = true;
            }
            float num18 = 1f;
            if (npc.ai[0] == 0f)
            {
                npc.TargetClosest();
                npc.velocity.Y *= 1f;
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

        public override void HitEffect(int hitDirection, double damage)
        {
            /*
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                return;
            }
            if (Main.expertMode)
            {
                int SnailSpawn = 1;
                for (int SnailSpawned = 0; SnailSpawned < SnailSpawn; SnailSpawned++)
                {

                    int x = (int)(Main.player[npc.target].position.X + (Main.player[npc.target].width - 1));
                    int y = (int)(Main.player[npc.target].position.Y + (Main.player[npc.target].height - 500));
                    int GreenSnail = ModContent.NPCType<GreenSnail>();
                    int BlueSnail = ModContent.NPCType<BlueSnail>();
                    int RedSnail = ModContent.NPCType<RedSnail>();
                    int num750 = NPC.NewNPC(x, y, GreenSnail);
                    Main.npc[num750].SetDefaults(GreenSnail);
                    int num751 = NPC.NewNPC(x, y, BlueSnail);
                    Main.npc[num751].SetDefaults(BlueSnail);

                    npc.direction = Main.player[npc.target].direction;
                    if (Main.expertMode && Main.rand.Next(4) == 0)
                    {
                        num751 = NPC.NewNPC(x, y, RedSnail);
                        Main.npc[num751].SetDefaults(RedSnail);
                    }
                    if (Main.netMode == NetmodeID.Server && num750 < 200)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num750);
                    }
                    if (Main.netMode == NetmodeID.Server && num751 < 200)
                    {
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, num751);
                    }*/
                    if (npc.life <= 0)
                    {
                        Main.NewText("Mano : I just wanted to drink the clear dew..", Color.Red);
            }
        }
		public override void OnHitPlayer(Player player, int damage, bool crit)
		{
			player.AddBuff(ModContent.BuffType<SlowDebuff>(), 60, true);
		}
		public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
		{
			player.AddBuff(ModContent.BuffType<SlowDebuff>(), 60, true);
		}
		public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
		{
			Main.player[npc.target].AddBuff(ModContent.BuffType<SlowDebuff>(), 60, true);
		}
	}
}