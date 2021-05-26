using TerraStory.Content.SFX;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using TerraStory.Items;
using TerraStory.Content.UI;
using TerraStory.Content.UI.Base;
using TerraStory.Packets;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using TerraStory.Items.Tools;

namespace TerraStory.Content.Players
{
    public class AIHelper : ModPlayer
    {

		public bool IsABestiaryIconDummy;

		public bool ForcePartyHatOn;

		public static int MoonLordFightingDistance = 4500;

		public static int MoonLordCountdown = 0;

		public const int MaxMoonLordCountdown = 3600;

		public int teleportStyle;

		public float teleportTime;

		public static int immuneTime = 20;

		public static int maxAI = 4;

		public int netSpam;

		public static int goldCritterChance = 400;

		public static int[] killCount = new int[663];

		public static float waveKills = 0f;

		public static int waveNumber = 0;

		public const float nameOverIncrement = 0.025f;

		public const float nameOverDistance = 350f;

		public float nameOver;

		public bool SpawnedFromStatue;

		public bool dripping;

		public bool drippingSlime;

		public bool drippingSparkleSlime;

		public static readonly int AFKTimeNeededForNoWorms = 300;

		public int altTexture;

		public int townNpcVariationIndex;

		public Vector2 netOffset = Vector2.Zero;

		public short catchItem;

		public short releaseOwner = 255;

		public int rarity;

		public static bool taxCollector = false;

		public bool[] playerInteraction = new bool[256];

		public int lastInteraction = 255;

		public float takenDamageMultiplier = 1f;

		public static bool freeCake = false;

		public float gfxOffY;

		public float stepSpeed;

		public bool teleporting;

		public bool stairFall;

		public static int fireFlyFriendly = 0;

		public static int fireFlyChance = 0;

		public static int fireFlyMultiple = 0;

		public static int butterflyChance = 0;

		public Vector2[] oldPos = new Vector2[10];

		public float[] oldRot = new float[10];

		public bool setFrameSize;

		public static int golemBoss = -1;

		public static int plantBoss = -1;

		public static int crimsonBoss = -1;

		public int netSkip;

		public bool netAlways;

		public int realLife = -1;

		public static int sWidth = 1920;

		public static int sHeight = 1080;

		public static int safeRangeX = (int)((double)(sWidth / 16) * 0.52);

		public static int safeRangeY = (int)((double)(sHeight / 16) * 0.52);

		public float npcSlots = 1f;

		public static bool noSpawnCycle = false;

		public static int activeTime = 750;

		public static int defaultSpawnRate = 600;

		public static int defaultMaxSpawns = 5;

		public bool dontCountMe;

		public const int maxBuffs = 5;

		public int[] buffType = new int[5];

		public int[] buffTime = new int[5];

		public bool[] buffImmune = new bool[323];

		public bool midas;

		public bool ichor;

		public bool onFire;

		public bool onFire2;

		public bool onFrostBurn;

		public bool poisoned;

		public bool markedByScytheWhip;

		public bool markedByThornWhip;

		public bool markedByFireWhip;

		public bool markedByRainbowWhip;

		public bool markedByBlandWhip;

		public bool markedBySwordWhip;

		public bool markedByMaceWhip;

		public bool venom;

		public bool shadowFlame;

		public bool soulDrain;

		public int lifeRegen;

		public int lifeRegenCount;

		public int lifeRegenExpectedLossPerSecond = -1;

		public bool confused;

		public bool loveStruck;

		public bool stinky;

		public bool dryadWard;

		public bool immortal;

		public bool chaseable = true;

		public bool canGhostHeal = true;

		public bool javelined;

		public bool celled;

		public bool dryadBane;

		public bool daybreak;

		public bool dontTakeDamageFromHostiles;

		public bool betsysCurse;

		public bool oiled;

		public static bool combatBookWasUsed = false;

		public static bool[] npcsFoundForCheckActive = new bool[663];

		public static int[] lazyNPCOwnedProjectileSearchArray = new int[200];

		private static int spawnRate = defaultSpawnRate;

		private static int maxSpawns = defaultMaxSpawns;

		public int soundDelay;

		public int[] immune = new int[256];

		public int directionY = 1;

		public int type;

		public float[] ai = new float[maxAI];

		public float[] localAI = new float[maxAI];

		public int aiAction;

		public int aiStyle;

		public bool justHit;

		public int timeLeft;

		public int target = -1;

		public int damage;

		public int defense;

		public int defDamage;

		public int defDefense;

		public bool coldDamage;

		public bool trapImmune;

		public LegacySoundStyle HitSound;

		public LegacySoundStyle DeathSound;

		public int life;

		public int lifeMax;

		public Rectangle targetRect;

		public double frameCounter;

		public Rectangle frame;

		public Color color;

		public int alpha;

		public bool hide;

		public float scale = 1f;

		public float knockBackResist = 1f;

		public virtual Vector2 VisualPosition => position;
		public int oldDirectionY;

		public int oldTarget;

		public float rotation;

		public bool noGravity;

		public bool noTileCollide;

		public bool netUpdate;

		public bool netUpdate2;

		public bool collideX;

		public bool collideY;

		public bool boss;

		public int spriteDirection = -1;

		public bool behindTiles;

		public bool lavaImmune;

		public float value;

		public int extraValue;

		public bool dontTakeDamage;

		public int netID;

		public int statsAreScaledForThisManyPlayers;

		public float strengthMultiplier = 1f;

		public bool townNPC;

		public static bool travelNPC = false;

		public bool homeless;

		public int homeTileX = -1;

		public int homeTileY = -1;

		public int housingCategory;

		public bool oldHomeless;

		public int oldHomeTileX = -1;

		public int oldHomeTileY = -1;

		public bool friendly;

		public bool closeDoor;

		public int doorX;

		public int doorY;

		public int friendlyRegen;

		public int breath;

		public const int breathMax = 200;

		public int breathCounter;

		public bool reflectsProjectiles;

		public int lastPortalColorIndex;

		public bool despawnEncouraged;

		public static int[,] cavernMonsterType = new int[2, 3];

		public static bool fairyLog = false;

		public static int ladyBugGoodLuckTime = 43200;

		public static int ladyBugBadLuckTime = -10800;


		public static int offSetDelayTime = 60;

		public Vector2 velocity;

		public Vector2 position;

		public float light;

		public bool active;

		public bool sticky = true;

		public int whoAmI;

		public Vector2 oldPosition;

		public Vector2 oldVelocity;

		public int oldDirection;

		public int direction = 1;

		public int width;

		public int height;

		public bool wet;

		public bool honeyWet;

		public byte wetCount;

		public bool lavaWet;

		public void EncourageDespawn(int despawnTime)
		{
			if (timeLeft > despawnTime)
			{
				timeLeft = despawnTime;
			}
			despawnEncouraged = true;
		}

		public bool CanTalk
		{
			get
			{
				if (isLikeATownNPC && aiStyle == 7 && velocity.Y == 0f)
				{

				}
				return false;
			}
		}
		public bool CanBeTalkedTo
		{
			get
			{
				if (isLikeATownNPC && aiStyle == 7)
				{
					return velocity.Y == 0f;
				}
				return false;
			}
		}

		public bool SupportsNPCTargets => NPCID.Sets.UsesNewTargetting[type];


		public bool HasPlayerTarget
		{
			get
			{
				if (target >= 0)
				{
					return target < 255;
				}
				return false;
			}
		}
		public bool HasNPCTarget
		{
			get
			{
				if (target >= 300)
				{
					return target < 500;
				}
				return false;
			}
		}

		public float Opacity
		{
			get
			{
				return 1f - (float)alpha / 255f;
			}
			set
			{
				alpha = (int)MathHelper.Clamp((1f - value) * 255f, 0f, 255f);
			}
		}

		public bool isLikeATownNPC
		{
			get
			{
				if (type == 453)
				{
					return true;
				}
				return townNPC;
			}
		}
		public Vector2 Center
		{
			get
			{
				return new Vector2(position.X + (float)(width / 2), position.Y + (float)(height / 2));
			}
			set
			{
				position = new Vector2(value.X - (float)(width / 2), value.Y - (float)(height / 2));
			}
		}
		public Vector2 Left
		{
			get
			{
				return new Vector2(position.X, position.Y + (float)(height / 2));
			}
			set
			{
				position = new Vector2(value.X, value.Y - (float)(height / 2));
			}
		}

		public Vector2 Right
		{
			get
			{
				return new Vector2(position.X + (float)width, position.Y + (float)(height / 2));
			}
			set
			{
				position = new Vector2(value.X - (float)width, value.Y - (float)(height / 2));
			}
		}

		public Vector2 Top
		{
			get
			{
				return new Vector2(position.X + (float)(width / 2), position.Y);
			}
			set
			{
				position = new Vector2(value.X - (float)(width / 2), value.Y);
			}
		}

		public Vector2 TopLeft
		{
			get
			{
				return position;
			}
			set
			{
				position = value;
			}
		}

		public Vector2 TopRight
		{
			get
			{
				return new Vector2(position.X + (float)width, position.Y);
			}
			set
			{
				position = new Vector2(value.X - (float)width, value.Y);
			}
		}

		public Vector2 Bottom
		{
			get
			{
				return new Vector2(position.X + (float)(width / 2), position.Y + (float)height);
			}
			set
			{
				position = new Vector2(value.X - (float)(width / 2), value.Y - (float)height);
			}
		}

		public Vector2 BottomLeft
		{
			get
			{
				return new Vector2(position.X, position.Y + (float)height);
			}
			set
			{
				position = new Vector2(value.X, value.Y - (float)height);
			}
		}

		public Vector2 BottomRight
		{
			get
			{
				return new Vector2(position.X + (float)width, position.Y + (float)height);
			}
			set
			{
				position = new Vector2(value.X - (float)width, value.Y - (float)height);
			}
		}

		public Vector2 Size
		{
			get
			{
				return new Vector2(width, height);
			}
			set
			{
				width = (int)value.X;
				height = (int)value.Y;
			}
		}

		public Rectangle Hitbox
		{
			get
			{
				return new Rectangle((int)position.X, (int)position.Y, width, height);
			}
			set
			{
				position = new Vector2(value.X, value.Y);
				width = value.Width;
				height = value.Height;
			}
		}
		public float AngleTo(Vector2 Destination)
		{
			return (float)Math.Atan2(Destination.Y - Center.Y, Destination.X - Center.X);
		}

		public float AngleFrom(Vector2 Source)
		{
			return (float)Math.Atan2(Center.Y - Source.Y, Center.X - Source.X);
		}

		public float Distance(Vector2 Other)
		{
			return Vector2.Distance(Center, Other);
		}

		public float DistanceSQ(Vector2 Other)
		{
			return Vector2.DistanceSquared(Center, Other);
		}

		public Vector2 DirectionTo(Vector2 Destination)
		{
			return Vector2.Normalize(Destination - Center);
		}

		public Vector2 DirectionFrom(Vector2 Source)
		{
			return Vector2.Normalize(Center - Source);
		}

		public bool WithinRange(Vector2 Target, float MaxRange)
		{
			return Vector2.DistanceSquared(Center, Target) <= MaxRange * MaxRange;
		}

		public void TargetClosest(bool faceTarget = true)
		{
			float num = 0f;
			float num2 = 0f;
			bool flag = false;
			int num3 = -1;
			for (int i = 0; i < 255; i++)
			{
				if (!Main.player[i].active || Main.player[i].dead || Main.player[i].ghost)
				{
					continue;
				}
				float num4 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - position.X + (float)(width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - position.Y + (float)(height / 2));
				num4 -= (float)Main.player[i].aggro;
				if (Main.player[i].npcTypeNoAggro[type] && direction != 0)
				{
					num4 += 1000f;
				}
				if (!flag || num4 < num)
				{
					flag = true;
					num3 = -1;
					num2 = Math.Abs(Main.player[i].position.X + (float)(Main.player[i].width / 2) - position.X + (float)(width / 2)) + Math.Abs(Main.player[i].position.Y + (float)(Main.player[i].height / 2) - position.Y + (float)(height / 2));
					num = num4;
					target = i;
				}
				if (Main.player[i].tankPet >= 0 && !Main.player[i].npcTypeNoAggro[type])
				{
					int tankPet = Main.player[i].tankPet;
					float num5 = Math.Abs(Main.projectile[tankPet].position.X + (float)(Main.projectile[tankPet].width / 2) - position.X + (float)(width / 2)) + Math.Abs(Main.projectile[tankPet].position.Y + (float)(Main.projectile[tankPet].height / 2) - position.Y + (float)(height / 2));
					num5 -= 200f;
					if (num5 < num && num5 < 200f && Collision.CanHit(Center, 1, 1, Main.projectile[tankPet].Center, 1, 1))
					{
						num3 = tankPet;
					}
				}
			}
			if (num3 >= 0)
			{
				int num6 = num3;
				targetRect = new Rectangle((int)Main.projectile[num6].position.X, (int)Main.projectile[num6].position.Y, Main.projectile[num6].width, Main.projectile[num6].height);
				direction = 1;
				if ((float)(targetRect.X + targetRect.Width / 2) < position.X + (float)(width / 2))
				{
					direction = -1;
				}
				directionY = 1;
				if ((float)(targetRect.Y + targetRect.Height / 2) < position.Y + (float)(height / 2))
				{
					directionY = -1;
				}
			}
			else
			{
				if (target < 0 || target >= 255)
				{
					target = 0;
				}
				targetRect = new Rectangle((int)Main.player[target].position.X, (int)Main.player[target].position.Y, Main.player[target].width, Main.player[target].height);
				if (Main.player[target].dead)
				{
					faceTarget = false;
				}
				if (Main.player[target].npcTypeNoAggro[type] && direction != 0)
				{
					faceTarget = false;
				}
				if (faceTarget)
				{
					_ = Main.player[target].aggro;
					_ = (Main.player[target].height + Main.player[target].width + height + width) / 4;
					if (Main.player[target].itemAnimation != 0 || Main.player[target].aggro >= 0 || oldTarget < 0 || oldTarget > 254)
					{
						direction = 1;
						if ((float)(targetRect.X + targetRect.Width / 2) < position.X + (float)(width / 2))
						{
							direction = -1;
						}
						directionY = 1;
						if ((float)(targetRect.Y + targetRect.Height / 2) < position.Y + (float)(height / 2))
						{
							directionY = -1;
						}
					}
				}
			}
			if (confused)
			{
				direction *= -1;
			}
			if ((direction != oldDirection || directionY != oldDirectionY || target != oldTarget) && !collideX && !collideY)
			{
				netUpdate = true;
			}
		}
	}
}
