using System.Collections.Generic;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.ModLoader;
using ReLogic.Graphics;

namespace TerraStory.Content.GFX
{
    public static class GFX
    {
        private const string SFXLEVELUP = "Content/SFX/LevelUP";
        private const string QUEENBEESOUND = "Sounds/Custom/QueenBeeSound";

        private const string UI_DIRECTORY = "Content/GFX/UI/";
        private const string NUMERALS = UI_DIRECTORY + "Num/Black_";
        private const string MSNO = UI_DIRECTORY + "Num_Exp/MSNo_";
        private const string LEVEL = UI_DIRECTORY + "LevelNum/Level_";
        private const string LEVEL_LV = UI_DIRECTORY + "LevelNum/Level_Lv";
        private const string STATUSBARS_COVER1 = UI_DIRECTORY + "StatusBars_Cover1";
        private const string STATUSBARS_BG = UI_DIRECTORY + "StatusBars_BG";
        private const string HP_LAYER = UI_DIRECTORY + "HP_Layer";
        private const string MP_LAYER = UI_DIRECTORY + "MP_Layer";
        private const string STATUSBARS_COVER = UI_DIRECTORY + "StatusBars_Cover";
        private const string INVENTORY_PANEL = UI_DIRECTORY + "Inventory_Panel";
        private const string FAVOURITEDSLOT = UI_DIRECTORY + "FavouritedSlot";
        private const string SELECTEDSLOT = UI_DIRECTORY + "SelectedSlot";
        private const string ITEM_SLOT = UI_DIRECTORY + "ItemSlot";
        private const string INVENTORY_STATS = UI_DIRECTORY + "Inventory_Stats";
        private const string BUBBLES_LAVA = UI_DIRECTORY + "Bubbles_Lava";
        private const string BUBBLES_WATER = UI_DIRECTORY + "Bubbles_Water";
        private const string SHOP_BG = UI_DIRECTORY + "Shop_BG";
        private const string STATS_PANEL = UI_DIRECTORY + "Stats_Panel";
        private const string LEVELUP = UI_DIRECTORY + "LevelUP";
        private const string PANELBACKGROUND = UI_DIRECTORY + "PanelBackground";
        private const string INNERPANELBACKGROUND = UI_DIRECTORY + "InnerPanelBackground";
        private const string PANELBORDER = UI_DIRECTORY + "PanelBorder";
        private const string CHAT_BACK = UI_DIRECTORY + "Chat_Back";
        private const string CHAT = UI_DIRECTORY + "Chat";
        private const string CHAT2 = UI_DIRECTORY + "Chat2";

        private const string BAGSLOTON = UI_DIRECTORY + "BagSlotOn";
        private const string BAGITEM = UI_DIRECTORY + "BagItem";

        private const string MAPLESTORY_BOLD = "Fonts/Maplestory_Bold";
        private const string MAPLESTORY_BOLD24 = "Fonts/Maplestory_Bold24";

        private const string QUEST_TRACKER = UI_DIRECTORY + "Quest_Tracker";
        private const string QUESTICONUI = UI_DIRECTORY + "QuestIconUI";
        private const string QUESTICON = UI_DIRECTORY + "QuestIcon";
        private const string QUESTICON2 = UI_DIRECTORY + "QuestIcon2";
        private const string QUESTICON3 = UI_DIRECTORY + "QuestIcon3";

        private const string BUTTONNEXT_NORMAL = UI_DIRECTORY + "Buttons/Next_Normal";
        private const string BUTTONNEXT_PRESSED = UI_DIRECTORY + "Buttons/Next_Pressed";

        private const string UP_NORMAL = UI_DIRECTORY + "Buttons/UP_Normal";
        private const string UP_PRESSED = UI_DIRECTORY + "Buttons/UP_Pressed";

        private const string CONFIRM_NORMAL = UI_DIRECTORY + "Buttons/Confirm_Normal";
        private const string CONFIRM_PRESSED = UI_DIRECTORY + "Buttons/Confirm_Pressed";

        private const string CANCEL_NORMAL = UI_DIRECTORY + "Buttons/Cancel_Normal";
        private const string CANCEL_PRESSED = UI_DIRECTORY + "Buttons/Cancel_Pressed";
        private const string CANCEL_MOUSEOVER = UI_DIRECTORY + "Buttons/Cancel_MouseOver";
        private const string CANCEL_DISABLED = UI_DIRECTORY + "Buttons/Cancel_Disabled";

        private const string BLUEMAX_NORMAL = UI_DIRECTORY + "Buttons/BlueMax_Normal";
        private const string BLUEMAX_PRESSED = UI_DIRECTORY + "Buttons/BlueMax_Pressed";
        private const string BLUEMAX_MOUSEOVER = UI_DIRECTORY + "Buttons/BlueMax_MouseOver";
        private const string BLUEMAX_DISABLED = UI_DIRECTORY + "Buttons/BlueMax_Disabled";

        private const string BLUEMIN_NORMAL = UI_DIRECTORY + "Buttons/BlueMin_Normal";
        private const string BLUEMIN_PRESSED = UI_DIRECTORY + "Buttons/BlueMin_Pressed";
        private const string BLUEMIN_MOUSEOVER = UI_DIRECTORY + "Buttons/BlueMin_MouseOver";
        private const string BLUEMIN_DISABLED = UI_DIRECTORY + "Buttons/BlueMin_Disabled";

        private const string MAX_NORMAL = UI_DIRECTORY + "Buttons/Max_Normal";
        private const string MAX_PRESSED = UI_DIRECTORY + "Buttons/Max_Pressed";
        private const string MAX_MOUSEOVER = UI_DIRECTORY + "Buttons/Max_MouseOver";
        private const string MAX_DISABLED = UI_DIRECTORY + "Buttons/Max_Disabled";

        private const string CLAIM_NORMAL = UI_DIRECTORY + "Buttons/Claim_Normal";
        private const string CLAIM_PRESSED = UI_DIRECTORY + "Buttons/Claim_Pressed";
        private const string CLAIM_MOUSEOVER = UI_DIRECTORY + "Buttons/Claim_MouseOver";
        private const string CLAIM_DISABLED = UI_DIRECTORY + "Buttons/Claim_Disabled";

        private const string SAVE_NORMAL = UI_DIRECTORY + "Buttons/Save_Normal";
        private const string SAVE_PRESSED = UI_DIRECTORY + "Buttons/Save_Pressed";
        private const string SAVE_MOUSEOVER = UI_DIRECTORY + "Buttons/Save_MouseOver";
        private const string SAVE_DISABLED = UI_DIRECTORY + "Buttons/Save_Disabled";

        private const string EXPBAR_GAUGE = UI_DIRECTORY + "ExpBar/1920p/EXPBar_Gauge";
        private const string EXPBAR_LAYERBACK = UI_DIRECTORY + "ExpBar/1920p/EXPBar_LayerBack";
        private const string EXPBAR_LAYERCOVER = UI_DIRECTORY + "ExpBar/1920p/EXPBar_LayerCover";

        //Sounds
        public static SoundEffect SfxLevelUp { get; set; }
        public static SoundEffect SfxQueenBeeSound{ get; set; }

        public static DynamicSpriteFont MaplestoryBold { get; set; }
        public static DynamicSpriteFont MaplestoryBold24 { get; set; }


        public static Dictionary<PlayerStats, Texture2D> Stats { get; set; } = new Dictionary<PlayerStats, Texture2D>();
        public static Texture2D StatsConverted { get; set; }
        public static Texture2D LevelLv { get; set; }
        public static Texture2D ExpBarLayerBack { get; set; }
        public static Texture2D ExpBarLayerCover { get; set; }
        public static Texture2D ExpBarGauge { get; set; }
        public static Texture2D SaveDisabled { get; set; }
        public static Texture2D SavePressed { get; set; }
        public static Texture2D SaveMouseOver { get; set; }
        public static Texture2D SaveNormal { get; set; }
        public static Texture2D ClaimDisabled { get; set; }
        public static Texture2D ClaimPressed { get; set; }
        public static Texture2D ClaimMouseOver { get; set; }
        public static Texture2D ClaimNormal { get; set; }
        public static Texture2D BlueMinDisabled { get; set; }
        public static Texture2D BlueMinMouseOver { get; set; }
        public static Texture2D BlueMinPressed { get; set; }
        public static Texture2D BlueMinNormal { get; set; }
        public static Texture2D BlueMaxDisabled { get; set; }
        public static Texture2D BlueMaxMouseOver { get; set; }
        public static Texture2D BlueMaxPressed { get; set; }
        public static Texture2D BlueMaxNormal { get; set; }
        public static Texture2D MaxDisabled { get; set; }
        public static Texture2D MaxMouseOver { get; set; }
        public static Texture2D MaxPressed { get; set; }
        public static Texture2D MaxNormal { get; set; }
        public static Texture2D CancelDisabled { get; set; }
        public static Texture2D CancelMouseOver { get; set; }
        public static Texture2D CancelPressed { get; set; }
        public static Texture2D CancelNormal { get; set; }
        public static Texture2D UPNormal { get; set; }
        public static Texture2D UPPressed { get; set; }
        public static Texture2D ConfirmNormal { get; set; }
        public static Texture2D ConfirmPressed { get; set; }
        public static Texture2D NextPressed { get; set; }
        public static Texture2D NextNormal { get; set; }
        
        public static Texture2D QuestIcon3 { get; set; }
        public static Texture2D QuestIcon2 { get; set; }
        public static Texture2D QuestIcon { get; set; }
        public static Texture2D QuestIconUI { get; set; }
        public static Texture2D QuestTracker { get; set; }
        public static Texture2D Chat2 { get; set; }
        public static Texture2D Chat { get; set; }
        public static Texture2D ChatBack { get; set; }
        public static Texture2D PanelBorder { get; set; }
        public static Texture2D InnerPanelBackground { get; set; }
        public static Texture2D PanelBackground { get; set; }
        public static Texture2D LevelUP { get; set; }
        public static Texture2D StatsPanel { get; set; }
        public static Texture2D ShopBG { get; set; }
        public static Texture2D BubblesWater { get; set; }
        public static Texture2D BubblesLava { get; set; }
        public static Texture2D InventoryStats { get; set; }
        public static Texture2D SelectedSlot { get; set; }
        public static Texture2D ItemSlot { get; set; }
        public static Texture2D FavouritedSlot { get; set; }
        public static Texture2D InventoryPanel { get; set; }
        public static Texture2D StatusBarsBG { get; set; }
        public static Texture2D HPLayer { get; set; }
        public static Texture2D MPLayer { get; set; }
        public static Texture2D StatusBarsCover { get; set; }
        public static Texture2D[] MSNo { get; set; } = new Texture2D[11];
        public static Texture2D[] BlackNumeral { get; set; } = new Texture2D[11];
        public static Texture2D[] LevelNum { get; set; } = new Texture2D[11];
        public static Texture2D UnspentPoints { get; set; }
        public static Texture2D StatusBarsCover1 { get; set; }
        public static Texture2D BagSlotOn { get; set; }
        public static Texture2D BagItem { get; set; }


        public static Texture2D CombineTextures(List<Texture2D> textures, List<Point> origins, Point final_size)
        {
            Texture2D texture = new Texture2D(Main.spriteBatch.GraphicsDevice, final_size.X * 2, final_size.Y * 2);
            Color[] combinedTexture = new Color[texture.Width * texture.Height];

            for (int tex = 0; tex < textures.Count; tex += 1)
            {
                if (textures[tex] == null) continue;

                Color[] pixels = new Color[textures[tex].Width * textures[tex].Height];
                textures[tex].GetData(pixels);

                for (int x = 0; x < textures[tex].Width; x += 1)
                    for (int y = 0; y < textures[tex].Height; y += 1)
                        if (pixels[x + y * textures[tex].Width].A > 0)
                            for (int i = 0; i < 4; i += 1)
                                combinedTexture[origins[tex].X * 2 + x * 2 + i % 2 + (origins[tex].Y * 2 + y * 2 + i / 2) * texture.Width] =
                                    pixels[x + y * textures[tex].Width];
            }

            texture.SetData(combinedTexture);
            return texture;
        }


        public static void LoadGfx()
        {
            Mod loader = ModLoader.GetMod(Constants.ModName);

            SfxLevelUp = loader.GetSound(SFXLEVELUP);
            SfxQueenBeeSound = loader.GetSound(QUEENBEESOUND);

            Stats[PlayerStats.HP] = loader.GetTexture(BLUEMAX_NORMAL);
            Stats[PlayerStats.MP] = loader.GetTexture(BLUEMAX_NORMAL);
            Stats[PlayerStats.STR] = loader.GetTexture(BLUEMAX_NORMAL);
            Stats[PlayerStats.DEX] = loader.GetTexture(BLUEMAX_NORMAL);
            Stats[PlayerStats.INT] = loader.GetTexture(BLUEMAX_NORMAL);
            Stats[PlayerStats.LUK] = loader.GetTexture(BLUEMAX_NORMAL);
            StatsConverted = loader.GetTexture(MAX_PRESSED);

            MaplestoryBold = loader.GetFont(MAPLESTORY_BOLD);
            MaplestoryBold24 = loader.GetFont(MAPLESTORY_BOLD24);

            LevelLv = loader.GetTexture(LEVEL_LV);
            ExpBarLayerBack = loader.GetTexture(EXPBAR_LAYERBACK);
            ExpBarGauge = loader.GetTexture(EXPBAR_GAUGE);
            ExpBarLayerCover = loader.GetTexture(EXPBAR_LAYERCOVER);

            SavePressed = loader.GetTexture(SAVE_PRESSED);
            SaveNormal = loader.GetTexture(SAVE_NORMAL);
            SaveMouseOver = loader.GetTexture(SAVE_MOUSEOVER);
            SaveDisabled = loader.GetTexture(SAVE_DISABLED);

            ClaimPressed = loader.GetTexture(CLAIM_PRESSED);
            ClaimNormal = loader.GetTexture(CLAIM_NORMAL);
            ClaimMouseOver = loader.GetTexture(CLAIM_MOUSEOVER);
            ClaimDisabled = loader.GetTexture(CLAIM_DISABLED);

            BlueMaxPressed = loader.GetTexture(BLUEMAX_PRESSED);
            BlueMaxNormal = loader.GetTexture(BLUEMAX_NORMAL);
            BlueMaxMouseOver = loader.GetTexture(BLUEMAX_MOUSEOVER);
            BlueMaxDisabled = loader.GetTexture(BLUEMAX_DISABLED);

            BlueMinPressed = loader.GetTexture(BLUEMIN_PRESSED);
            BlueMinNormal = loader.GetTexture(BLUEMIN_NORMAL);
            BlueMinMouseOver = loader.GetTexture(BLUEMIN_MOUSEOVER);
            BlueMinDisabled = loader.GetTexture(BLUEMIN_DISABLED);

            MaxPressed = loader.GetTexture(MAX_PRESSED);
            MaxNormal = loader.GetTexture(MAX_NORMAL);
            MaxMouseOver = loader.GetTexture(MAX_MOUSEOVER);
            MaxDisabled = loader.GetTexture(MAX_DISABLED);

            CancelPressed = loader.GetTexture(CANCEL_PRESSED);
            CancelNormal = loader.GetTexture(CANCEL_NORMAL);
            CancelMouseOver = loader.GetTexture(CANCEL_MOUSEOVER);
            CancelDisabled = loader.GetTexture(CANCEL_DISABLED);

            ConfirmPressed = loader.GetTexture(CONFIRM_PRESSED);
            ConfirmNormal = loader.GetTexture(CONFIRM_NORMAL);

            UPPressed = loader.GetTexture(UP_PRESSED);
            UPNormal = loader.GetTexture(UP_NORMAL);

            NextPressed = loader.GetTexture(BUTTONNEXT_PRESSED);
            NextNormal = loader.GetTexture(BUTTONNEXT_NORMAL);

            LevelUP = loader.GetTexture(LEVELUP);

            QuestIcon3 = loader.GetTexture(QUESTICON3);
            QuestIcon2 = loader.GetTexture(QUESTICON2);
            QuestIcon = loader.GetTexture(QUESTICON);
            QuestIconUI = loader.GetTexture(QUESTICON);
            QuestTracker = loader.GetTexture(QUEST_TRACKER);
            Chat2 = loader.GetTexture(CHAT2);
            Chat = loader.GetTexture(CHAT);
            ChatBack = loader.GetTexture(CHAT_BACK);
            PanelBorder = loader.GetTexture(PANELBACKGROUND);
            InnerPanelBackground = loader.GetTexture(INNERPANELBACKGROUND);
            PanelBackground = loader.GetTexture(PANELBACKGROUND);
            StatsPanel = loader.GetTexture(STATS_PANEL);
            ShopBG = loader.GetTexture(SHOP_BG);
            BubblesWater = loader.GetTexture(BUBBLES_WATER);
            BubblesLava = loader.GetTexture(BUBBLES_LAVA);
            InventoryStats = loader.GetTexture(INVENTORY_STATS);
            SelectedSlot = loader.GetTexture(SELECTEDSLOT);
            ItemSlot = loader.GetTexture(ITEM_SLOT);
            FavouritedSlot = loader.GetTexture(FAVOURITEDSLOT);
            InventoryPanel = loader.GetTexture(INVENTORY_PANEL);
            StatusBarsBG = loader.GetTexture(STATUSBARS_BG);
            HPLayer = loader.GetTexture(HP_LAYER);
            MPLayer = loader.GetTexture(MP_LAYER);
            StatusBarsCover = loader.GetTexture(STATUSBARS_COVER);
            StatusBarsCover1 = loader.GetTexture(STATUSBARS_COVER1);
            BagSlotOn = loader.GetTexture(BAGSLOTON);
            BagItem = loader.GetTexture(BAGITEM);
            for (int i = 0; i < 10; i++)
                BlackNumeral[i] = loader.GetTexture(NUMERALS + i);
            for (int i = 0; i < 10; i++)
                LevelNum[i] = loader.GetTexture(LEVEL + i);
            for (int i = 0; i < 10; i++)
                MSNo[i] = loader.GetTexture(MSNO + i);
        }

        public static void UnloadGfx()
        {
            SfxQueenBeeSound = null;
            SfxLevelUp = null;

            Stats[PlayerStats.HP] = null;
            Stats[PlayerStats.MP] = null;
            Stats[PlayerStats.STR] = null;
            Stats[PlayerStats.DEX] = null;
            Stats[PlayerStats.INT] = null;
            Stats[PlayerStats.LUK] = null;
            StatsConverted = null;
            MaplestoryBold = null;
            MaplestoryBold24 = null;
            LevelLv = null;
            ExpBarLayerCover = null;
            ExpBarLayerBack = null;
            ExpBarGauge = null;
            SaveDisabled = null;
            SaveMouseOver = null;
            SaveNormal = null;
            SavePressed = null;
            ClaimDisabled = null;
            ClaimMouseOver = null;
            ClaimNormal = null;
            ClaimPressed = null;
            BlueMinNormal = null;
            BlueMinPressed = null;
            BlueMinMouseOver = null;
            BlueMinDisabled = null;
            BlueMaxNormal = null;
            BlueMaxPressed = null;
            BlueMaxMouseOver = null;
            BlueMaxDisabled = null;
            MaxNormal = null;
            MaxPressed = null;
            MaxMouseOver = null;
            MaxDisabled = null;
            CancelPressed = null;
            CancelNormal = null;
            CancelDisabled = null;
            CancelMouseOver = null;
            ConfirmPressed = null;
            ConfirmNormal = null;
            UPPressed = null;
            UPNormal = null;
            NextPressed = null;
            NextNormal = null;
            LevelUP = null;
            QuestIcon3 = null;
            QuestIcon2 = null;
            QuestIcon = null;
            QuestIconUI = null;
            QuestTracker = null;
            Chat2 = null;
            Chat = null;
            ChatBack = null;
            InnerPanelBackground = null;
            PanelBorder = null;
            PanelBackground = null;
            StatsPanel = null;
            ShopBG = null;
            BubblesLava = null;
            BubblesWater = null;
            InventoryStats = null;
            SelectedSlot = null;
            ItemSlot = null;
            FavouritedSlot = null;
            InventoryPanel = null;
            StatusBarsBG = null;
            MPLayer = null;
            HPLayer = null;
            StatusBarsCover1 = null;
            StatusBarsCover = null;
            BagSlotOn = null;
            BagItem = null;
            for (int i = 0; i < 10; i++)
                BlackNumeral[i] = null;
            for (int i = 0; i < 10; i++)
                LevelNum[i] = null;
            for (int i = 0; i < 10; i++)
                MSNo[i] = null;
        }
    }
}