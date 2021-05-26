using System;
using System.Linq;
using System.Net;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using ReLogic.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using TerraStory.Packets;
using TerraStory.Enums;
using TerraStory.Content.GFX;
using TerraStory.Content.UI.Base;
using TerraStory.NPCs.Bosses;
using TerraStory.Items.Boss;
using TerraStory.Items.Placeable.Trophies;
using TerraStory.Items.Vanity;
using TerraStory.Items;
using TerraStory.Items.Weapons.Minions;
using TerraStory.Items.Weapons.Warrior;
using TerraStory.Items.Weapons.Mage;
using TerraStory.Items.MountsSummon;
using TerraStory.Items.Ect;

namespace TerraStory
{
    public class TerraStory : Mod
    {
        public static TerraStory Instance;

        public static int CustomCurrencyID;

        private DynamicSpriteFont Maplestory_Bold;

        private DynamicSpriteFont Maplestory_Bold24;

        public TerraStory()
        {
            Instance = this;

            Mod = this;

            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true,
                AutoloadBackgrounds = true
            };
        }

        public static void LogMessage(string msg)
        {
            Debug.WriteLine("MESSAGE: " + msg);
            ModLoader.GetMod(Constants.ModName).Logger.InfoFormat(msg);
        }

        public Texture2D[] InvSlot { get; set; } = new Texture2D[16];

        public static TerraStory Mod { get; set; }

        public static Mod StructureHelper { get; set; }

        public static bool PlayerEnteredWorld { get; set; } = false;

        //public static Mod Overhaul { get; set; }

        public bool DrawInterface()
        {

            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            if (Main.netMode == NetmodeID.Server || Main.gameMenu) return true;
            try
            {
                for (int i = 0; i < BaseUI.UIElements.Count; i += 1)
                {
                    BaseUI UI = BaseUI.UIElements[i];
                    if (UI.PreDraw())
                        UI.Draw(Main.spriteBatch, Main.LocalPlayer);
                }
            }
            catch (SystemException e)
            {
                LogMessage(e.ToString());

            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScaleMatrix);
            return true;
        }

        public override void Load()
        {
            TSConfig.Initialize();
            if (Main.netMode != NetmodeID.Server)
            {
                GFX.LoadGfx();
                InvSlot[0] = Main.inventoryBackTexture;
                InvSlot[1] = Main.inventoryBack2Texture;
                InvSlot[2] = Main.inventoryBack3Texture;
                InvSlot[3] = Main.inventoryBack4Texture;
                InvSlot[4] = Main.inventoryBack5Texture;
                InvSlot[5] = Main.inventoryBack6Texture;
                InvSlot[6] = Main.inventoryBack7Texture;
                InvSlot[7] = Main.inventoryBack8Texture;
                InvSlot[8] = Main.inventoryBack9Texture;
                InvSlot[9] = Main.inventoryBack10Texture;
                InvSlot[10] = Main.inventoryBack11Texture;
                InvSlot[11] = Main.inventoryBack12Texture;
                InvSlot[12] = Main.inventoryBack13Texture;
                InvSlot[13] = Main.inventoryBack14Texture;
                InvSlot[14] = Main.inventoryBack15Texture;
                Main.inventoryBackTexture = GFX.ItemSlot;
                Main.inventoryBack2Texture = GFX.ItemSlot;
                Main.inventoryBack3Texture = GFX.ItemSlot;
                Main.inventoryBack4Texture = GFX.ItemSlot;
                Main.inventoryBack5Texture = GFX.ItemSlot;
                Main.inventoryBack6Texture = GFX.ItemSlot;
                Main.inventoryBack7Texture = GFX.ItemSlot;
                Main.inventoryBack8Texture = GFX.ItemSlot;
                Main.inventoryBack9Texture = GFX.ItemSlot;
                Main.inventoryBack10Texture = GFX.FavouritedSlot;
                Main.inventoryBack11Texture = GFX.ItemSlot;
                Main.inventoryBack12Texture = GFX.ItemSlot;
                Main.inventoryBack13Texture = GFX.ItemSlot;
                Main.inventoryBack14Texture = GFX.SelectedSlot;
                Main.inventoryBack15Texture = GFX.ItemSlot;
                Main.inventoryBack16Texture = GFX.ItemSlot;
                Main.chatTexture = GFX.Chat;
                Main.chatBackTexture = GFX.ChatBack;
                Main.chat2Texture = GFX.Chat2;
                CustomCurrencyID = CustomCurrencyManager.RegisterCurrency(new BundleOfMesos(ModContent.ItemType<Items.BundleOfMesos>(), 999L));
                LoadFonts();
                LoadCoinTexture();
                Main.player[Main.myPlayer].hbLocked = false;
                Main.instance.LoadNPC(NPCID.BlueSlime);
                Main.instance.LoadNPC(NPCID.IceSlime);
                Main.instance.LoadNPC(NPCID.SandSlime);
                Main.instance.LoadNPC(NPCID.MotherSlime);
                Main.instance.LoadNPC(NPCID.LavaSlime);
                Main.instance.LoadNPC(NPCID.Crimslime);
                Main.instance.LoadNPC(NPCID.CorruptSlime);
                Main.instance.LoadNPC(NPCID.JungleBat);
                Main.npcTexture[NPCID.JungleBat] = GetTexture("Resprite/JungleBat");
                Main.npcTexture[NPCID.BlueSlime] = GetTexture("Resprite/Slime");
                Main.npcTexture[NPCID.Crimslime] = GetTexture("Resprite/CrimSlime");
                Main.npcTexture[NPCID.CorruptSlime] = GetTexture("Resprite/CorruptSlime");
                Main.npcTexture[NPCID.SandSlime] = GetTexture("Resprite/SandSlime");
                Main.npcTexture[NPCID.IceSlime] = GetTexture("Resprite/IceSlime");
                Main.npcTexture[NPCID.MotherSlime] = GetTexture("Resprite/MotherSlime");
                Main.npcTexture[NPCID.LavaSlime] = GetTexture("Resprite/LavaSlime");

                if (!Main.dedServ)
                {
                    // Warrior .. to rework!
                    AddEquipTexture(null, EquipType.Legs, "WhiteCrusaderChainMail_Legs", "TerraStory/Items/Armor/Maple/WhiteCrusaderChainMail_Legs");
                    AddEquipTexture(null, EquipType.Legs, "WhiteKendoRobe_Legs", "TerraStory/Items/Armor/Maple/WhiteKendoRobe_Legs");

                    // Cannoneer
                    AddEquipTexture(null, EquipType.Legs, "BrownCottonLagger_Legs", "TerraStory/Items/Armor/Cannoneer/BrownCottonLagger_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BrownPollard_Legs", "TerraStory/Items/Armor/Cannoneer/BrownPollard_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BeigeCarribean_Legs", "TerraStory/Items/Armor/Cannoneer/BeigeCarribean_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BlueBraceLook_Legs", "TerraStory/Items/Armor/Cannoneer/BlueBraceLook_Legs");
                    AddEquipTexture(null, EquipType.Legs, "CrimsonBarbay_Legs", "TerraStory/Items/Armor/Cannoneer/CrimsonBarbay_Legs");
                    AddEquipTexture(null, EquipType.Legs, "CorruptedBarbay_Legs", "TerraStory/Items/Armor/Cannoneer/CorruptedBarbay_Legs");
                    AddEquipTexture(null, EquipType.Legs, "GreenPlasteer_Legs", "TerraStory/Items/Armor/Cannoneer/GreenPlasteer_Legs");
                    AddEquipTexture(null, EquipType.Legs, "TurkGally_Legs", "TerraStory/Items/Armor/Cannoneer/TurkGally_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BlackRoyalBaron_Legs", "TerraStory/Items/Armor/Cannoneer/BlackRoyalBaron_Legs");

                    // Thief
                    AddEquipTexture(null, EquipType.Legs, "BlueCloth_Legs", "TerraStory/Items/Armor/Thief/BlueCloth_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BrownSneak_Legs", "TerraStory/Items/Armor/Thief/BrownSneak_Legs");
                    AddEquipTexture(null, EquipType.Legs, "RedStealer_Legs", "TerraStory/Items/Armor/Thief/RedStealer_Legs");
                    AddEquipTexture(null, EquipType.Legs, "PurpleStealer_Legs", "TerraStory/Items/Armor/Thief/PurpleStealer_Legs");
                    AddEquipTexture(null, EquipType.Legs, "DarkNightShift_Legs", "TerraStory/Items/Armor/Thief/DarkNightShift_Legs");
                    AddEquipTexture(null, EquipType.Legs, "RedKnuckleVest_Legs", "TerraStory/Items/Armor/Thief/RedKnuckleVest_Legs");
                    AddEquipTexture(null, EquipType.Legs, "GreenKnuckleVest_Legs", "TerraStory/Items/Armor/Thief/GreenKnuckleVest_Legs");
                    AddEquipTexture(null, EquipType.Legs, "DarkShadow_Legs", "TerraStory/Items/Armor/Thief/DarkShadow_Legs");
                    AddEquipTexture(null, EquipType.Legs, "BluePao_Legs", "TerraStory/Items/Armor/Thief/BluePao_Legs");
                }
            }
        }
        public override void Unload()
        {

            Main.inventoryBackTexture = InvSlot[0];
            Main.inventoryBack2Texture = InvSlot[1];
            Main.inventoryBack3Texture = InvSlot[2];
            Main.inventoryBack4Texture = InvSlot[3];
            Main.inventoryBack5Texture = InvSlot[4];
            Main.inventoryBack6Texture = InvSlot[5];
            Main.inventoryBack7Texture = InvSlot[6];
            Main.inventoryBack8Texture = InvSlot[7];
            Main.inventoryBack9Texture = InvSlot[8];
            Main.inventoryBack10Texture = InvSlot[9];
            Main.inventoryBack11Texture = InvSlot[10];
            Main.inventoryBack12Texture = InvSlot[11];
            Main.inventoryBack13Texture = InvSlot[12];
            Main.inventoryBack14Texture = InvSlot[13];
            Main.inventoryBack15Texture = InvSlot[14];
            Main.inventoryBack16Texture = InvSlot[15];
            GFX.Chat = Main.chatTexture;
            GFX.Chat2 = Main.chat2Texture;
            GFX.ChatBack = Main.chatBackTexture;
            GFX.UnloadGfx();
            Main.instance.invBottom = 210;
            Instance = null;
            StructureHelper = null;
            UnloadFonts();
            UnloadCoinTexture();
        }
        private void UnloadFonts()
        {
            Main.fontDeathText = Maplestory_Bold24;
            Main.fontItemStack = Maplestory_Bold;
            Main.fontMouseText = Maplestory_Bold;
            Maplestory_Bold = null;
            Maplestory_Bold24 = null;
        }
        private void LoadFonts()
        {
            Maplestory_Bold24 = Main.fontDeathText;
            Maplestory_Bold = Main.fontItemStack;
            Maplestory_Bold = Main.fontMouseText;
            Main.fontItemStack = GetFont("Fonts/Maplestory_Bold");
            Main.fontMouseText = GetFont("Fonts/Maplestory_Bold");
            Main.fontDeathText = GetFont("Fonts/Maplestory_Bold24");
            Main.fontCombatText = (DynamicSpriteFont[])(object)new DynamicSpriteFont[2]
            {
                GetFont("Fonts/Maplestory_Bold"),
                GetFont("Fonts/Maplestory_Bold")
            };
        }
        private void LoadCoinTexture()
        {
          //  Main.coinTexture[ItemID.PlatinumCoin] = PlatinumMesos;
          //  PlatinumMesos = Main.coinTexture[ItemID.PlatinumCoin];
          //  PlatinumMesos = GetTexture("Resprite/Coin_3");
            Main.itemTexture[ItemID.PlatinumCoin] = GetTexture("Resprite/Item_74");

           // Main.coinTexture[ItemID.GoldCoin] = GoldMesos;
           // GoldMesos = Main.coinTexture[ItemID.GoldCoin];
         //   GoldMesos = GetTexture("Resprite/Coin_2");
            Main.itemTexture[ItemID.GoldCoin] = GetTexture("Resprite/Item_73");

           // Main.coinTexture[ItemID.SilverCoin] = SilverMesos;
           // SilverMesos = Main.coinTexture[ItemID.SilverCoin];
           // SilverMesos = GetTexture("Resprite/Coin_1");
            Main.itemTexture[ItemID.SilverCoin] = GetTexture("Resprite/Item_72");

            //Main.coinTexture[ItemID.CopperCoin] = CopperMesos;
           // CopperMesos = Main.coinTexture[ItemID.CopperCoin];
            //CopperMesos = GetTexture("Resprite/Coin_0");
            Main.itemTexture[ItemID.CopperCoin] = GetTexture("Resprite/Item_71");

            /*Main.itemTexture[ItemID.WoodenArrow] = GetTexture("Resprite/Item_40");

            Main.projectileTexture[ProjectileID.WoodenArrowFriendly] = GetTexture("Resprite/Projectile_1");

            Main.projectileTexture[ProjectileID.WoodenArrowHostile] = GetTexture("Resprite/Projectile_1");

            Main.itemTexture[ItemID.FlamingArrow] = GetTexture("Resprite/Item_41");

            Main.projectileTexture[ProjectileID.FlamingArrow] = GetTexture("Resprite/Projectile_2");

            Main.projectileTexture[ProjectileID.FireArrow] = GetTexture("Resprite/Projectile_2");*/

            Main.coinTexture = (Texture2D[])(object)new Texture2D[4]
            {
                 GetTexture("Resprite/Coin_0"),
                 GetTexture("Resprite/Coin_1"),
                 GetTexture("Resprite/Coin_2"),
                 GetTexture("Resprite/Coin_3")
            };
        }
        private void UnloadCoinTexture()
        {
            Main.coinTexture = null;
        }
        /*
        private void UnloadResprite()
        {
            Main.itemTexture = null;
            Main.projectileTexture = null;
        }*/
        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            Mod MagicStorage = ModLoader.GetMod("MagicStorage");
            MagicStorage?.ModifyInterfaceLayers(layers);
            layers.Find(layer => layer.Name == "Vanilla: Resource Bars").Active = false;
            layers[layers.FindIndex(layer => layer.Name == "Vanilla: Inventory")] = new LegacyGameInterfaceLayer(Constants.ModName, DrawInterface, InterfaceScaleType.UI);
            layers.Find(layer => layer.Name == "Vanilla: Hotbar").Active = false;
        }
        public void AddLayer(List<GameInterfaceLayer> layers, UserInterface userInterface, UIState state, int index, bool visible)
        {
            string name = state == null ? "Unknown" : state.ToString();
            layers.Insert(index, new LegacyGameInterfaceLayer("TerraStory: " + name,
                delegate
                {
                    if (visible)
                    {
                        userInterface.Update(Main._drawInterfaceGameTime);
                        state.Draw(Main.spriteBatch);
                    }
                    return true;
                }, InterfaceScaleType.UI));
        }

        public override void PostSetupContent()
        {

            Mod StructureHelper = ModLoader.GetMod("StructureHelper");
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                // AddBoss, bossname, order or value in terms of vanilla bosses, inline method for retrieving downed value.
                bossChecklist.Call("AddBoss", "True Queen Bee", 7f, (Func<bool>)(() => World.downedTrueQueenBee));

                bossChecklist.Call(
                "AddBoss",
                0.4f,
                ModContent.NPCType<Mano>(),
                this,
                "Mano",
                (Func<bool>)(() => World.downedMano),
                ModContent.ItemType<MossySnailShell>(),
                new List<int> {ItemID.LesserHealingPotion,ItemID.LesserManaPotion, ModContent.ItemType<RedSnailShell>(), ModContent.ItemType<BlueSnailShell>(), ModContent.ItemType<GreenSnailShell>() },
                new List<int> { ModContent.ItemType<ManoTreasureBag>(), ItemID.LesserHealingPotion, ItemID.LesserManaPotion, ModContent.ItemType<RainbowColoredSnailShell>() },
                "Use a [i: " + ModContent.ItemType<MossySnailShell>() + "] anywhere during daytime",
                null,
                "TerraStory/Textures/BossChecklist/ManoTexture",
                "TerraStory/NPCs/Bosses/Mano_Head_Boss",
                null);

                bossChecklist.Call(
                "AddBoss",
                0.5f,
                ModContent.NPCType<OrangeMushmom>(),
                this,
                "Mushmom",
                (Func<bool>)(() => World.downedOrangeMushmom),
                ModContent.ItemType<OrangeMushmomDoll>(),
                new List<int> { ItemID.Mushroom, ModContent.ItemType<MushmomTrophy>(), ModContent.ItemType<MushmomCap>(), ModContent.ItemType<OrangeMushroomStaff>(), ModContent.ItemType<MushMush>(), ModContent.ItemType<BigMushroom>() },
                new List<int> { ModContent.ItemType<OrangeMushmomTreasureBag>(), ItemID.LesserHealingPotion, ItemID.LesserManaPotion, ModContent.ItemType<KinoBadge>() },
                "Use a [i: " + ModContent.ItemType<OrangeMushmomDoll>() + "] anywhere during daytime",
                null,
                "TerraStory/Textures/BossChecklist/MushmomTexture",
                "TerraStory/NPCs/Bosses/OrangeMushmom_Head_Boss",
                null);
                
                bossChecklist.Call(
                "AddBoss",
                1.4f,
                ModContent.NPCType<ZombieMushmom>(),
                this,
                "Zombie Mushmom",
                (Func<bool>)(() => World.downedZombieMushmom),
                ModContent.ItemType<ZombieMushmomDoll>(),
                new List<int> { ModContent.ItemType<ZombieMushmomTrophy>() },
                new List<int> { ModContent.ItemType<ZombieMushmomTreasureBag>(), ItemID.VileMushroom, ItemID.ViciousMushroom, ItemID.Deathweed},
                "Use a [i: " + ModContent.ItemType<ZombieMushmomDoll>() + "] during night",
                null,
                "TerraStory/Textures/BossChecklist/ZombieMushmomTexture",
                "TerraStory/NPCs/Bosses/ZombieMushmom_Head_Boss",
                null);

                bossChecklist.Call(
                "AddBoss",
                1.6f,
                ModContent.NPCType<BlueMushmom>(),
                this,
                "Blue Mushmom",
                (Func<bool>)(() => World.downedBlueMushmom),
                ModContent.ItemType<BlueMushmomDoll>(),
                new List<int> { ModContent.ItemType<BlueMushmomTrophy>() },
                new List<int> { ModContent.ItemType<BlueMushmomTreasureBag>() },
                "Use a [i: " + ModContent.ItemType<BlueMushmomDoll>() + "] in glowshrooms biome",
                null,
                "TerraStory/Textures/BossChecklist/BlueMushmomTexture",
                "TerraStory/NPCs/Bosses/BlueMushmom_Head_Boss",
                null);

                bossChecklist.Call(
                "AddBoss",
                5.5f,
                ModContent.NPCType<PapaPixie>(),
                this,
                "Papa Pixie",
                (Func<bool>)(() => World.downedPapaPixie),
                ModContent.ItemType<PapaPixieHat>(),
                new List<int> { ItemID.FallenStar },
                new List<int> { ModContent.ItemType<StarStaff>() },
                new List<int> { ModContent.ItemType<PapaPixieTreasureBag>() },
                "Use a [i: " + ModContent.ItemType<PapaPixieHat>() + "] during night time.",
                null,
                "TerraStory/Textures/BossChecklist/PapaPixieTexture",
                "TerraStory/NPCs/Bosses/PapaPixie_Head_Boss",
                null);

                
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            Message msg = (Message)reader.ReadByte();
#if DEBUG
            LogMessage($"Handling {msg}");
#endif
            switch (msg)
            {

                case Message.SyncStats:
                    SyncStatsPacket.Read(reader);
                    break;
                case Message.SyncLevel:
                    SyncLevelPacket.Read(reader);
                    PlayerEnteredWorld = true;
                    break;
                case Message.AddXp:
                    AddXPPacket.Read(reader);
                    break;
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu)
            {

                if (Main.dayTime
                && Main.player[Main.myPlayer].active 
                && Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && Main.player[Main.myPlayer].ZoneOverworldHeight)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/Ludibrium");
                    priority = MusicPriority.BiomeLow;
                }
                if (!Main.dayTime
                && Main.player[Main.myPlayer].active 
                && Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && Main.player[Main.myPlayer].ZoneOverworldHeight)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/LudibriumNT");
                    priority = MusicPriority.BiomeLow;
                }
                if (Main.player[Main.myPlayer].active 
                && Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && Main.player[Main.myPlayer].ZoneDirtLayerHeight)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/LudibriumUG");
                    priority = MusicPriority.Environment;
                }
                if (Main.player[Main.myPlayer].active 
                && Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && Main.player[Main.myPlayer].ZoneRockLayerHeight)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/LudibriumCavern");
                    priority = MusicPriority.Environment;
                }
                if (Main.player[Main.myPlayer].ZoneRain
                && Main.player[Main.myPlayer].ZoneOverworldHeight
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneBeach
                && !Main.player[Main.myPlayer].ZoneHoly
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneSkyHeight
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/MissingYouRain");
                    priority = MusicPriority.Event;
                }
                if (Main.player[Main.myPlayer].ZoneRockLayerHeight
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/AbandonnedMines");
                    priority = MusicPriority.Environment;
                }
                if (!Main.dayTime
                && Main.player[Main.myPlayer].ZoneOverworldHeight
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                //&& !Main.player[Main.myPlayer].ZoneHoly
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/HenesysShadow");
                    priority = MusicPriority.BiomeLow;
                }
                if (Main.dayTime
                && Main.player[Main.myPlayer].ZoneOverworldHeight
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && !Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/HenesysFields");
                    priority = MusicPriority.BiomeLow;
                }
                if (Main.player[Main.myPlayer].ZoneDirtLayerHeight
                && !Main.player[Main.myPlayer].ZoneJungle
                && !Main.player[Main.myPlayer].ZoneSnow
                && !Main.player[Main.myPlayer].ZoneCrimson
                && !Main.player[Main.myPlayer].ZoneCorrupt
                && !Main.player[Main.myPlayer].ZoneHoly
                && !Main.player[Main.myPlayer].ZoneDesert
                && !Main.player[Main.myPlayer].GetModPlayer<TerraStoryPlayer>().ZoneLudibrium)
                {
                    music = (this).GetSoundSlot(SoundType.Music, "Sounds/Music/UnderGround");
                    priority = MusicPriority.Environment;
            }
        }
    }
}