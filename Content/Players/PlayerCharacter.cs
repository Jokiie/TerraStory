using System;
using System.Collections.Generic;
using System.Linq;
using TerraStory.Items;
using TerraStory.Content.UI;
using TerraStory.Content.UI.Base;
using TerraStory.Enums;
using TerraStory.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using TerraStory.Items.Tools;
using TerraStory.Items.Weapons.Thief.Garnier;
using TerraStory.Items.Weapons.Thief.IronTitan;
using TerraStory.Items.Weapons.Thief.GoldTitan;
using TerraStory.Items.Weapons.Thief.CopperIgor;
using TerraStory.Items.Weapons.Thief.BloodSlain;
using TerraStory.Items.Weapons.Thief.CorruptedSlain;
using TerraStory.Items.Weapons.Thief.Scarab;
using TerraStory.Items.Weapons.Thief.SilverGuardian;
using TerraStory.Items.Weapons.Mage;
using TerraStory.Items.Weapons.Thief.NinjaClaw;
using TerraStory.Items.Weapons.Thief.MapleClaw;
using TerraStory.Items.Weapons.Thief.PinkRabbitPuppet;
using TerraStory.Items.Weapons.Thief.DragonGreenSleeve;
using TerraStory.Items.Weapons.Cannoneer;

namespace TerraStory.Content.Players
{
    public class PlayerCharacter : ModPlayer
    {
        public PlayerCharacter()
        {
            // Stats Permanenent, stats via les buffs
            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                BaseStats[stat] = 0;
                TempStats[stat] = 0;
            }
            Inventories = new Item[3][];
            for (int inventoryPage = 0; inventoryPage < Inventories.Length; inventoryPage += 1)
            {
                Inventories[inventoryPage] = new Item[40];
                for (int inventorySlot = 0; inventorySlot < Inventories[inventoryPage].Length; inventorySlot += 1)
                {
                    Inventories[inventoryPage][inventorySlot] = new Item();
                    Inventories[inventoryPage][inventorySlot].SetDefaults(2, true);
                }
            }
        }


        // Region UI
        public bool StatPage { get; set; } = true;

        public bool InvPage { get; set; } = true;

        public int ActiveStatPage { get; set; }

        public int ActiveInvPage { get; set; }

        public Item[][] Inventories { get; set; }

        public InventoryUI InventoryUI { get; set; }



        public int whoAmI;

        public bool active;

        public static int WorldTime = -1;

        public double ItemRotation { get; set; }

        public StatusBar StatusBar { get; set; }

        public EXPBar EXPBar { get; set; }


        private int LevelAnimation { get; set; }

        private bool Initialized { get; set; }

        public Chat Chat { get; set; }

        // Region Stats
        public int BonusHP { get; set; }

        public int BonusMP { get; set; }

        public float HPDegen { get; set; }

        public float HPLeech { get; set; }

        public float HPRegen { get; set; } = 1f;

        private float DegenTimer { get; set; }

        private float RegenTimer { get; set; }

        public int MP { get; set; }

        public int HP { get; set; }

        public float MPRegen { get; set; }

        public bool CanHealMP { get; set; } = true;

        private int LeechCooldown { get; set; }

        public int CritBoost { get; set; }

        public float CritHitChance { get; set; } = 1f;

        public float CritMultiplier { get; set; } = 1f;

        public float DamageMultiplierPercent => 0.01f;

        public int LastTalkNpc = -1;

        // Region Level ,Xp values
        public int Experience { get; set; }

        public int Level { get; set; } = 1;

        public int PointsAllocated

        {
            get { return Enum.GetValues(typeof(PlayerStats)).Cast<PlayerStats>().Sum(stat => BaseStats[stat]); }
        }

        public Dictionary<PlayerStats, int> BaseStats { get; set; } = new Dictionary<PlayerStats, int>();

        public Dictionary<PlayerStats, int> TempStats { get; set; } = new Dictionary<PlayerStats, int>();

        public void AddXp(int xp)
        {
            if (Main.gameMenu)
                return;
            if (xp == 0)
                return;
            Experience += xp;

        Check:
            if (Experience >= ExperienceToLevel())
            {
                Experience -= ExperienceToLevel();
                LevelUP();
                goto Check;
            }

            CombatText.NewText(player.getRect(), new Color(255, 215, 0), xp + "XP");
        }

        public override void DrawEffects(PlayerDrawInfo drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (Main.netMode == NetmodeID.Server || Main.myPlayer != player.whoAmI) return;
            if (player.statLife < 1) return;

            if (Main.netMode == NetmodeID.Server) return;

            SpriteBatch spriteBatch = Main.spriteBatch;

            if (LevelAnimation >= 60)
                return;
            if (LevelAnimation < 20)
            {
                fullBright = true;
                Lighting.AddLight(player.position, 4f, 4f, 4f);
            }
            else
            {
                Lighting.AddLight(player.position, 2f, 2f, 2f);
            }
            // LevelAnimation / 4= vitesse de l'animation  450, 450, 450 = size dune frame de lanimation
            spriteBatch.Draw(GFX.GFX.LevelUP, player.Bottom - new Vector2(225, 420) - Main.screenPosition, new Rectangle(0, LevelAnimation / 3 * 450, 450, 450),
                Color.White);
            LevelAnimation += 1;
        }


        public int ExperienceToLevel()
        {
            if (Level == 1)
                return (15);
            if (Level == 2)
                return (34);
            if (Level == 3)
                return 57;
            if (Level == 4)
                return 92;
            if (Level == 5)
                return 135;
            if (Level == 6)
                return 372;
            if (Level == 7)
                return 560;
            if (Level == 8)
                return 840;
            if (Level == 9)
                return 1242;
            if (Level == 10)
                return 1242;
            if (Level == 11)
                return 1242;
            if (Level == 12)
                return 1242;
            if (Level == 13)
                return 1242;
            if (Level == 14)
                return 1242;
            if (Level == 15)
                return 1490;
            if (Level == 16)
                return 1788;
            if (Level == 17)
                return 2145;
            if (Level == 18)
                return 2574;
            if (Level == 19)
                return 3088;
            if (Level == 20)
                return 3705;
            if (Level == 21)
                return 4446;
            if (Level == 22)
                return 5335;
            if (Level == 23)
                return 6402;
            if (Level == 24)
                return 7682;
            if (Level == 25)
                return 9218;
            if (Level == 26)
                return 11061;
            if (Level == 27)
                return 13273;
            if (Level == 28)
                return 15927;
            if (Level == 29)
                return 19122;
            if (Level == 30)
                return 19122;
            if (Level == 31)
                return 19122;
            if (Level == 32)
                return 19122;
            if (Level == 33)
                return 19122;
            if (Level == 34)
                return 19122;
            if (Level == 35)
                return 22934;
            if (Level == 36)
                return 27520;
            if (Level == 37)
                return 33024;
            if (Level == 38)
                return 39628;
            if (Level == 39)
                return 47553;
            if (Level == 40)
                return 51357;
            if (Level == 41)
                return 55465;
            if (Level == 42)
                return 59902;
            if (Level == 43)
                return 64694;
            if (Level == 44)
                return 69869;
            if (Level == 45)
                return 75458;
            if (Level == 46)
                return 81494;
            if (Level == 47)
                return 88013;
            if (Level == 48)
                return 95054;
            if (Level == 49)
                return 102658;
            if (Level == 50)
                return 110870;
            if (Level == 51)
                return 119739;
            else
                return Level = 51;

        }

        public void LevelUP()
        {
            Level += 1;
            if (!Main.gameMenu)
                GFX.GFX.SfxLevelUp.Play(0.8f * Main.soundVolume, 0f, 0f);
            SyncLevelPacket.Write(player.whoAmI, Level);
            LevelAnimation = 0;
            CombatText.NewText(player.getRect(), new Color(255, 225, 255), "Level" + Level);
        }
        /*
        private void LeechLife(Item item, int damage)
        {
            if (LeechCooldown != 0)
                return;
            int leechAmount = Math.Min((int)(damage * HPLeech), (int)(player.inventory[player.selectedItem].damage / 2f * (1 + HPLeech)));
            leechAmount = Math.Min(leechAmount, (int)(player.statLifeMax2 * HPLeech * 0.2));
            if (leechAmount > 1)
            {
                player.statLife += leechAmount;
                player.HealEffect(leechAmount);
                LeechCooldown = item.useAnimation * 3;
            }

            else if (HPLeech > 0f)
            {
                player.statLife += 1;
                player.HealEffect(1);
                LeechCooldown = (int)(item.useAnimation * (3 - Math.Min(1.4f, HPLeech * 10f)));
            }
        }
        public override void OnHitNPC(Item item, NPC target, int damage, float knockBack, bool crit)
        {
            LeechLife(item, damage);
        }*/

        public override void PostUpdateEquips()
        {
            if (!Initialized)
            {
                InitializeUI();
                Initialized = true;
            }

            UpdateStats();
            if (HPRegen > 0 && !player.bleed && !player.onFire && !player.poisoned && !player.onFire2 && !player.venom && !player.onFrostBurn)
                RegenTimer += 1f;
            if (RegenTimer > 60f / HPRegen)
            {
                player.statLife = Math.Min(player.statLife + (int)(RegenTimer / (60f / HPRegen)), player.statLifeMax2);
                RegenTimer = RegenTimer % (60f / HPRegen);
            }

            if (HPDegen > 0) DegenTimer += 1f;
            if (DegenTimer >= 20f)
            {
                int amount = (int)Math.Round(HPDegen / 3, 1);
                player.statLife = player.statLife - amount;
                CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(255, 95, 31),
                    amount);
                DegenTimer = 0;
                if (player.statLife <= 0) player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " got killed."), amount, 0);
            }
        }

        public override void PreUpdate()
        {
            if (Main.chatRelease && !Main.drawingPlayerChat && !Main.editSign && !Main.editChest && Main.netMode != NetmodeID.Server)
            {
                if (PlayerInput.Triggers.Current.QuickHeal)
                    if (!PlayerInput.Triggers.Old.QuickHeal)
                    {
                        player.ApiQuickHeal();
                        PlayerInput.Triggers.Old.QuickHeal = true;
                    }

                if (PlayerInput.Triggers.Current.QuickMana)
                    if (!PlayerInput.Triggers.Old.QuickMana)
                    {
                        player.ApiQuickMana();
                        PlayerInput.Triggers.Old.QuickMana = true;
                    }

                if (PlayerInput.Triggers.Current.QuickBuff)
                    if (!PlayerInput.Triggers.Old.QuickBuff)
                    {
                        player.ApiQuickBuff();
                        PlayerInput.Triggers.Old.QuickBuff = true;
                    }
            }

            player.QuicksRadial.Update();

            if (player.QuicksRadial.SelectedBinding == -1 || !PlayerInput.Triggers.JustReleased.RadialQuickbar ||
                PlayerInput.MiscSettingsTEMP.HotbarRadialShouldBeUsed)
                return;

            switch (player.QuicksRadial.SelectedBinding)
            {
                case 0:
                    player.ApiQuickHeal();
                    break;
                case 1:
                    player.ApiQuickBuff();
                    break;
                case 2:
                    player.ApiQuickMana();
                    break;
            }

            PlayerInput.Triggers.JustReleased.RadialQuickbar = false;
        }

        public override void PreUpdateBuffs()
        {

        }

        public override void ResetEffects()
        {
            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            TempStats[stat] = 0;
            BonusHP = 0;
            BonusMP = 0;
            HPRegen = 1;
            HPDegen = 0;
            MPRegen = 0;
            CanHealMP = true;

            CritBoost = 0;
            CritMultiplier = 0f;
            HPLeech = 0f;

            if (Math.Abs(Main.time % 300) < .01)
                SyncStatsPacket.Write(
                    player.whoAmI,
                    Level, 
                    BaseStats[PlayerStats.HP],
                    BaseStats[PlayerStats.MP],
                    BaseStats[PlayerStats.STR],
                    BaseStats[PlayerStats.DEX],
                    BaseStats[PlayerStats.INT],
                    BaseStats[PlayerStats.LUK]);
        }

        public override void SetupStartInventory(IList<Item> items, bool mediumCoreDeath)
        {
            Random rand = new Random();
            switch (rand.Next(1))
            {
            default:
            items[0].SetDefaults(ModContent.ItemType<Lollipop>());
            items[1].SetDefaults(ModContent.ItemType<Spoon>());
            items[2].SetDefaults(ModContent.ItemType<BeginnerBag>());
            break;
            }
        }
        

        public void OpenInventoryPage(int page)
        {
            for (int inventorySlot = 0; inventorySlot < 40; inventorySlot += 1)
                player.inventory[inventorySlot + 10] = Inventories[page][inventorySlot];

            ActiveInvPage = page;
            StatPage = true;
            InvPage = true;

            Recipe.FindRecipes();

            for (int i = 0; i < 50; i += 1)

                if (player.inventory[i].type == ItemID.SilverCoin || player.inventory[i].type == ItemID.CopperCoin || player.inventory[i].type == ItemID.GoldCoin || player.inventory[i].type == ItemID.PlatinumCoin)
                    player.DoCoins(i);
        }

        public override void PlayerConnect(Player playerObj)
        {
            SyncLevelPacket.Write(playerObj.whoAmI, Level, true);
        }

        public int TotalStats(PlayerStats playerStats)
        {
            return BaseStats[playerStats] + TempStats[playerStats];
        }
        public bool UnspentPoints()
        {
            return PointsAllocated < Level - 1;
        }

        public void UpdateStats()
        {
            HPLeech += TotalStats(PlayerStats.STR) * 0.001f;

            HPLeech += Math.Min(0.003f, TotalStats(PlayerStats.STR) * 0.001f);

            player.statLifeMax2 += TotalStats(PlayerStats.HP) * 5;
            player.statManaMax2 += TotalStats(PlayerStats.MP) * 5;

            player.meleeDamage += TotalStats(PlayerStats.STR) * 0.01f;

            player.thrownDamage += TotalStats(PlayerStats.DEX) * 0.01f;

            player.rangedDamage += TotalStats(PlayerStats.DEX) * 0.01f;

            player.magicDamage += TotalStats(PlayerStats.INT) * 0.01f;

            player.minionDamage += TotalStats(PlayerStats.LUK) * 0.01f;

            CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);

            modPlayer.cannonDamageAdd += TotalStats(PlayerStats.STR) * 0.01f;

            CritMultiplier += TotalStats(PlayerStats.LUK) * 0.01f;

            //CritBoost += Math.Min(TotalStats(PlayerStats.LUK), Math.Max(4, TotalStats(PlayerStats.LUK) / 2 + 2));

            player.magicCrit += TotalStats(PlayerStats.LUK) * 1;

            player.meleeCrit += TotalStats(PlayerStats.LUK) * 1;

            player.rangedCrit += TotalStats(PlayerStats.LUK) * 1;

            player.thrownCrit += TotalStats(PlayerStats.LUK) * 1;

            modPlayer.cannonCrit += TotalStats(PlayerStats.LUK) * 1;

        }
        public override void FrameEffects()
        {
            if (player.HeldItem.type == ModContent.ItemType<Garnier>())
            { player.handon = (sbyte)mod.GetEquipSlot("Garnier", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<CopperIgor>())
            { player.handon = (sbyte)mod.GetEquipSlot("CopperIgor", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<IronTitan>())
            { player.handon = (sbyte)mod.GetEquipSlot("IronTitan", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<SilverGuardian>())
            { player.handon = (sbyte)mod.GetEquipSlot("SilverGuardian", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<GoldTitan>())
            { player.handon = (sbyte)mod.GetEquipSlot("GoldTitan", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<BloodSlain>())
            { player.handon = (sbyte)mod.GetEquipSlot("BloodSlain", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<CorruptedSlain>())
            { player.handon = (sbyte)mod.GetEquipSlot("CorruptedSlain", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<DesertScarab>())
            { player.handon = (sbyte)mod.GetEquipSlot("DesertScarab", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<MagicClaw>())
            { player.handon = (sbyte)mod.GetEquipSlot("MagicClaw", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<NinjaClaw>())
            { player.handon = (sbyte)mod.GetEquipSlot("NinjaClaw", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<MapleClaw>())
            { player.handon = (sbyte)mod.GetEquipSlot("MapleClaw", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<PinkRabbitPuppet>())
            { player.handon = (sbyte)mod.GetEquipSlot("PinkRabbitPuppet", EquipType.HandsOn); }
            if (player.HeldItem.type == ModContent.ItemType<DragonGreenSleeve>())
            { player.handon = (sbyte)mod.GetEquipSlot("DragonGreenSleeve", EquipType.HandsOn); }
            { }
        }
        public override void Initialize()
        {
            BaseStats = new Dictionary<PlayerStats, int>();
            TempStats = new Dictionary<PlayerStats, int>();
            Level = 1;
            Experience = 0;

            foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
            {
                BaseStats[stat] = 0;
                TempStats[stat] = 0;
            }

            Inventories = new Item[3][];
            for (int inventoryPageId = 0; inventoryPageId < Inventories.Length; inventoryPageId += 1)
            {
                Inventories[inventoryPageId] = new Item[40];
            for (int inventorySlotId = 0; inventorySlotId < Inventories[inventoryPageId].Length; inventorySlotId += 1)
                Inventories[inventoryPageId][inventorySlotId] = new Item();
           
            }
        }

        public void InitializeUI()
        {
            if (Main.netMode == NetmodeID.Server)
                 return;
            BaseUI.UIElements.Clear();
            StatusBar = new StatusBar(this) { UIActive = true };
            InventoryUI = new InventoryUI(this, mod);
            EXPBar = new EXPBar(this) { UIActive = true };
            Chat = new Chat { UIActive = true };
        }
        
        public void CloseUI()
        {
            InventoryUI.CloseUI();
        }

        public override void OnEnterWorld(Player playerObj)
        {
            TerraStory.PlayerEnteredWorld = true;
            InitializeUI();
        }

        public override TagCompound Save()
        {
            TagCompound tagCompound = new TagCompound
            {
                {"level", Level},
                {"Experience", Experience},
                {"baseHP", BaseStats[PlayerStats.HP]},
                {"baseMP", BaseStats[PlayerStats.MP]},
                {"baseSTR", BaseStats[PlayerStats.STR]},
                {"baseDEX", BaseStats[PlayerStats.DEX]},
                {"baseINT", BaseStats[PlayerStats.INT]},
                {"baseLUK", BaseStats[PlayerStats.LUK]},
                {"HP", player.statLife},
                {"MP", player.statMana}

            };

            try
            {
                for (int inventoryPageId = 0; inventoryPageId < Inventories.Length; inventoryPageId += 1)
                    for (int inventorySlotId = 0; inventorySlotId < Inventories[inventoryPageId].Length; inventorySlotId += 1)
                        tagCompound.Add("item" + inventoryPageId + inventorySlotId, ItemIO.Save(Inventories[inventoryPageId][inventorySlotId]));
            }
            catch (SystemException e)
            {
                ModLoader.GetMod(Constants.ModName).Logger.InfoFormat("@Inventories :: " + e);
            }

            return tagCompound;
        }

        public override void Load(TagCompound tag)
        {
            try
            {
                Level = tag.GetInt("level");
                Experience = tag.GetInt("Experience");
                
            }
            catch (SystemException e)
            {
                ModLoader.GetMod(Constants.ModName).Logger.InfoFormat("@Level&XP&Event :: " + e);
            }

            try
            {
                foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
                    BaseStats[stat] = tag.GetInt("base" + stat.ToString().ToUpper());
            }
            catch (SystemException e)
            {
                ModLoader.GetMod(Constants.ModName).Logger.InfoFormat("@Stats :: " + e);
            }

            try
            {
                for (int inventoryPageId = 0; inventoryPageId < Inventories.Length; inventoryPageId += 1)
                    for (int inventorySlotId = 0; inventorySlotId < Inventories[inventoryPageId].Length; inventorySlotId += 1)
                        Inventories[inventoryPageId][inventorySlotId] = ItemIO.Load(tag.GetCompound("item" + inventoryPageId + inventorySlotId));
                OpenInventoryPage(0);
            }
            catch (SystemException e)
            {
                ModLoader.GetMod(Constants.ModName).Logger.InfoFormat("@Inventory :: " + e);
            }

            try
            {
                player.statLife = tag.GetInt("HP");
                player.statMana = tag.GetInt("MP");
            }
            catch (SystemException e)
            {
                ModLoader.GetMod(Constants.ModName).Logger.InfoFormat("@Mana&Life :: " + e);
            }
        }
    }
}