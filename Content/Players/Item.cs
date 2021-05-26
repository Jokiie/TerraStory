using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.UI;
using TerraStory.Enums;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using System.Collections.Generic;
using static TerraStory.Content.Players.ContentSamples;
using TerraStory.Content.Players;
using TerraStory.Items.Weapons.Warrior;
using TerraStory.Projectiles.Minions.SoldierHong;

namespace TerraStory.Players
{
    public class Item
	{
		public Item item;

		internal GlobalItem[] globalItems = new GlobalItem[0];

		private string _nameOverride;

		public const int flaskTime = 72000;

		public const int copper = 1;

		public const int silver = 100;

		public const int gold = 10000;

		public const int platinum = 1000000;

		public static int[] itemCaches = ItemID.Sets.Factory.CreateIntSet(-1);

		public static int potionDelay = 3600;

		public static int restorationDelay = 3000;

		public bool questItem;

		public static int[] headType = new int[216];

		public static int[] bodyType = new int[210];

		public static int[] legType = new int[161];

		public static bool[] staff = new bool[3930];

		public static bool[] claw = new bool[3930];

		public bool flame;

		public bool mech;

		public int noGrabDelay;

		public bool beingGrabbed;

		public bool isBeingGrabbed;

		public int spawnTime;

		public int tileWand = -1;

		public bool wornArmor;

		public byte dye;

		public int fishingPole = 1;

		public int bait;

		public static int coinGrabRange = 350;

		public static int manaGrabRange = 300;

		public static int lifeGrabRange = 250;

		public short makeNPC;

		public bool expertOnly;

		public bool expert;

		public short hairDye = -1;

		public byte paint;

		public bool instanced;

		public int ownIgnore = -1;

		public int ownTime;

		public int keepTime;

		public int type;

		public bool favorited;

		public int holdStyle;

		public int useStyle;

		public bool channel;

		public bool accessory;

		public int useAnimation;

		public int useTime;

		public int stack;

		public int maxStack;

		public int pick;

		public int axe;

		public int hammer;

		public int tileBoost;

		public int createTile = -1;

		public int createWall = -1;

		public int placeStyle;

		public int damage;

		public float knockBack;

		public int healLife;

		public int healMana;

		public bool potion;

		public bool consumable;

		public bool autoReuse;

		public bool useTurn;

		public Color color;

		public int alpha;

		public short glowMask;

		public float scale = 1f;

		public LegacySoundStyle UseSound;

		public int defense;

		public int headSlot = -1;

		public int bodySlot = -1;

		public int legSlot = -1;

		public sbyte handOnSlot = -1;

		public sbyte handOffSlot = -1;

		public sbyte backSlot = -1;

		public sbyte frontSlot = -1;

		public sbyte shoeSlot = -1;

		public sbyte waistSlot = -1;

		public sbyte wingSlot = -1;

		public sbyte shieldSlot = -1;

		public sbyte neckSlot = -1;

		public sbyte faceSlot = -1;

		public sbyte balloonSlot = -1;

		public int stringColor;

		public ItemTooltip ToolTip;

		public int owner = 255;

		public int rare;

		public int shoot;

		public float shootSpeed;

		public int ammo = AmmoID.None;

		public bool notAmmo;

		public int useAmmo = AmmoID.None;

		public int lifeRegen;

		public int manaIncrease;

		public bool buyOnce;

		public int mana;

		public bool noUseGraphic;

		public bool noMelee;

		public int release;

		public int value;

		public bool buy;

		public bool social;

		public bool vanity;

		public bool material;

		public bool noWet;

		public int buffType;

		public int buffTime;

		public int mountType = -1;

		public bool cartTrack;

		public bool uniqueStack;

		public int shopSpecialCurrency = -1;

		public int ReqStats = -1;

		public int? shopCustomPrice;

		public bool DD2Summon;

		public int netID;

		public int crit;

		public byte prefix;

		public bool melee;

		public bool magic;

		public bool ranged;

		public bool thrown;

		public bool summon;

		public bool sentry;

		public int reuseDelay;

		public bool newAndShiny;

		public string Name => _nameOverride ?? Lang.GetItemNameValue(type);

		public ModItem modItem
		{
			get;
			internal set;
		}

		public int ReqLevel;


		public int STR = player.GetModPlayer<PlayerCharacter>().TotalStats(PlayerStats.STR);

		public int DEX = player.GetModPlayer<PlayerCharacter>().TotalStats(PlayerStats.DEX);

		public int INT = player.GetModPlayer<PlayerCharacter>().TotalStats(PlayerStats.INT);

		public int LUK = player.GetModPlayer<PlayerCharacter>().TotalStats(PlayerStats.LUK);

		public string AffixName()
		{
			if (prefix < 0 || prefix >= Lang.prefix.Length)
			{
				return Name;
			}
			string text = Lang.prefix[prefix].Value;
			if (text == "")
			{
				return Name;
			}
			if (text.StartsWith("("))
			{
				return Name + " " + text;
			}
			return text + " " + Name;
		}
		public string HoverName
		{
			get
			{
				string text = AffixName();
				if (stack > 1)
				{
					text = text + " (" + stack + ")";
				}
				return text;
			}
		}
		public bool IsAir
		{
			get
			{
				if (type > 0)
				{
					return stack <= 0;
				}
				return true;
			}
		}
		public Item Clone()
		{
			return (Item)MemberwiseClone();
		}

		public Item DeepClone()
		{
			return (Item)MemberwiseClone();
		}

		public bool IsTheSameAs(Item compareItem)
		{
			if (netID == compareItem.netID)
			{
				return type == compareItem.type;
			}
			return false;
		}

		public bool IsNotTheSameAs(Item compareItem)
		{
			if (netID == compareItem.netID && stack == compareItem.stack)
			{
				return prefix != compareItem.prefix;
			}
			return true;
		}
		public void SetNameOverride(string name)
		{
			_nameOverride = name;
		}

		public void ClearNameOverride()
		{
			_nameOverride = null;
		}

		public int itemReqLevel(Player player)
		{

			if (item.ReqLevel >= player.GetModPlayer<PlayerCharacter>().Level)
			{ 
				CanThePlayerEquipThis(player);
				return value;
			}
		    return 0;
		}

		public bool CanThePlayerEquipThis(Player player)
		{
			for (int i = 0; i < item.ReqLevel; i++)
			{
				if (item.ReqLevel >= player.GetModPlayer<PlayerCharacter>().Level || item.ReqLevel == player.GetModPlayer<PlayerCharacter>().Level)
				{
					item.ReqLevel = player.GetModPlayer<PlayerCharacter>().Level;
					return true;
				}
			}
			return false;
		}

		public void TurnToAir()
		{
			type = 0;
			stack = 0;
			netID = 0;
			dye = 0;
			shoot = 0;
			mountType = -1;
		}

		public void SetDefaults()
		{
			if( item is Item)
		    STR = 0;
			DEX = 0;
			INT = 0;
			LUK = 0;
			item.ReqLevel = value;
			SetDefaults();
		}

		public void SetStaticDefaults()
        {
			SetStaticDefaults();
		}
	}
}