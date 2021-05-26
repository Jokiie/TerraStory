using System;
using System.Globalization;
using System.Reflection;
using System.Linq;
using TerraStory.Content.Recipes;
using TerraStory.Content.GFX;
using TerraStory.Content.UI.Base;
using TerraStory.Content.SFX;
using TerraStory.Content.Players;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.GameContent.UI.States;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Map;
using Terraria.ModLoader;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using TerraStory.Content.Stats;
using System.Collections.Generic;

namespace TerraStory.Content.UI
{
    public class InventoryUI : BaseUI
    {
        public Dictionary<PlayerStats, int> allocated = new Dictionary<PlayerStats, int> { { PlayerStats.HP, 0 }, { PlayerStats.MP, 0 }, { PlayerStats.STR, 0 }, { PlayerStats.DEX, 0 }, { PlayerStats.INT, 0 }, { PlayerStats.LUK, 0 } };
        private PlayerCharacter character;

        private static readonly FieldInfo AllChestStackHover = typeof(Main).GetField("allChestStackHover", BindingFlags.NonPublic | BindingFlags.Static);
        private const int BarLength = 192;
        private static readonly MethodInfo DrawPvp = typeof(Main).GetMethod("DrawPVPIcons", BindingFlags.NonPublic | BindingFlags.Static);

        private static readonly FieldInfo InventorySortMouseOver = typeof(Main).GetField("inventorySortMouseOver", BindingFlags.NonPublic | BindingFlags.Static);

        private static readonly FieldInfo Mh = typeof(Main).GetField("mH", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo MouseReforge = typeof(Main).GetField("mouseReforge", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly MethodInfo PageIcons = typeof(Main).GetMethod("DrawPageIcons", BindingFlags.NonPublic | BindingFlags.Static);
        private static readonly FieldInfo ReforgeScale = typeof(Main).GetField("reforgeScale", BindingFlags.NonPublic | BindingFlags.Static);

        public InventoryUI(PlayerCharacter character, Mod mod)
        {
            AddButton(() => new Rectangle((int)(Origin.X + 20 * Scale), (int)(Origin.Y + 343 * Scale), (int)(GFX.GFX.MaxNormal.Width * Scale), (int)(GFX.GFX.MaxNormal.Height * Scale)), delegate (Player player)
            {
                Main.PlaySound(SoundID.MenuTick);
                PlayerCharacter pc = player.GetModPlayer<PlayerCharacter>();
                pc.StatPage = !pc.StatPage;
            });
            AddButton(() => new Rectangle((int)(Origin.X + 50 * Scale), (int)(Origin.Y + 340 * Scale), (int)(GFX.GFX.NextNormal.Width * Scale), (int)(GFX.GFX.NextNormal.Height * Scale)), delegate (Player player)
            {
                Main.PlaySound(SoundID.MenuTick);
                PlayerCharacter pc = player.GetModPlayer<PlayerCharacter>();
                pc.OpenInventoryPage(0);
            });
            AddButton(() => new Rectangle((int)(Origin.X + 80 * Scale), (int)(Origin.Y + 340 * Scale), (int)(GFX.GFX.NextNormal.Width * Scale), (int)(GFX.GFX.NextNormal.Height * Scale)), delegate (Player player)
            {
                Main.PlaySound(SoundID.MenuTick);
                PlayerCharacter pc = player.GetModPlayer<PlayerCharacter>();
                pc.OpenInventoryPage(1);
            });
            AddButton(() => new Rectangle((int)(Origin.X + 110 * Scale), (int)(Origin.Y + 340 * Scale), (int)(GFX.GFX.NextNormal.Width * Scale), (int)(GFX.GFX.NextNormal.Height * Scale)), delegate (Player player)
            {
                Main.PlaySound(SoundID.MenuTick);
                PlayerCharacter pc = player.GetModPlayer<PlayerCharacter>();
                pc.OpenInventoryPage(2);
            });

        }


        public Vector2 Origin => new Vector2(2f, 10f) * Scale; //emplacement de l'inventaire a lecran
        private static float Scale => 0.92f;


        private void DrawHotbar()
        {
            float oldInvScale = Main.inventoryScale;
            Main.inventoryScale = Scale - 0.03f;  // scale grosseur des icons de la Hotbar de linventaire et grosseur des slots
            for (int i = 0; i < 10; i++) // Nombres de slot dans la HotBar
            {
                float x2 = Origin.X + 15 * Scale + i * 52 * Scale; //( X/Y = Emplacement a lecran) (52 = dimension des slots) (Scale = Espace entre les slots)
                float y2 = Origin.Y + 12 * Scale; // si on augment Y, ca se deplace vers le bas
                if (Main.mouseX >= x2 && Main.mouseX <= x2 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= y2 && Main.mouseY <= y2 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                    !PlayerInput.IgnoreMouseInterface)
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                    ItemSlot.OverrideHover(Main.player[Main.myPlayer].inventory, 0, i);
                    if (Main.player[Main.myPlayer].inventoryChestStack[i] && (Main.player[Main.myPlayer].inventory[i].type == ItemID.None || Main.player[Main.myPlayer].inventory[i].stack == 0))
                        Main.player[Main.myPlayer].inventoryChestStack[i] = false;

                    if (!Main.player[Main.myPlayer].inventoryChestStack[i])
                    {
                        if (Main.mouseLeftRelease && Main.mouseLeft)
                        {
                            ItemSlot.LeftClick(Main.player[Main.myPlayer].inventory, 0, i);
                            Recipe.FindRecipes();
                        }
                        else
                        {
                            ItemSlot.RightClick(Main.player[Main.myPlayer].inventory, 0, i);
                        }
                    }

                    ItemSlot.MouseHover(Main.player[Main.myPlayer].inventory, 0, i);
                }

                Main.spriteBatch.ItemSlotDrawExtension(Main.player[Main.myPlayer].inventory, 0, i, new Vector2(x2, y2), Color.White, Color.White);
            }

            Main.inventoryScale = oldInvScale;
        }

        private Vector2 StatsPosition => new Vector2(Origin.X + 600, Origin.Y + 90);

        private Dictionary<PlayerStats, Vector2> Position => new Dictionary<PlayerStats, Vector2>
        {
                {PlayerStats.HP, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y - 2 * Scale)},
                {PlayerStats.MP, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y + 18 * Scale)},
                {PlayerStats.STR, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y + 42 * Scale)},
                {PlayerStats.DEX, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y + 63 * Scale)},
                {PlayerStats.INT, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y + 82 * Scale)},
                {PlayerStats.LUK, new Vector2(StatsPosition.X + 125 * Scale, StatsPosition.Y + 103 * Scale)}
        };
        private PlayerStats Id { get; }

        private int Allocated
        {
            get => allocated[Id];
            set => allocated[Id] = value;
        }
        public void DrawStatPage(SpriteBatch spriteBatch, PlayerCharacter character, Player player)
        {
            character = player.GetModPlayer<PlayerCharacter>();
            Dictionary<PlayerStats, CharacterStats> characterStats;
            this.character = character;

            characterStats = new Dictionary<PlayerStats, CharacterStats>
            {
                [PlayerStats.HP] = new CharacterStats(this, PlayerStats.HP, () => Position[PlayerStats.HP], GFX.GFX.Stats[PlayerStats.HP]),
                [PlayerStats.MP] = new CharacterStats(this, PlayerStats.MP, () => Position[PlayerStats.MP], GFX.GFX.Stats[PlayerStats.MP]),
                [PlayerStats.STR] = new CharacterStats(this, PlayerStats.STR, () => Position[PlayerStats.STR], GFX.GFX.Stats[PlayerStats.STR]),
                [PlayerStats.DEX] = new CharacterStats(this, PlayerStats.DEX, () => Position[PlayerStats.DEX], GFX.GFX.Stats[PlayerStats.DEX]),
                [PlayerStats.INT] = new CharacterStats(this, PlayerStats.INT, () => Position[PlayerStats.INT], GFX.GFX.Stats[PlayerStats.INT]),
                [PlayerStats.LUK] = new CharacterStats(this, PlayerStats.LUK, () => Position[PlayerStats.LUK], GFX.GFX.Stats[PlayerStats.LUK]),
            };

            Vector2 panelOrigin = new Vector2(Origin.X + 600, Origin.Y + 90);

            spriteBatch.Draw(GFX.GFX.StatsPanel, panelOrigin, Color.White);

            int remaining = character.Level - character.PointsAllocated - 1;
            remaining = allocated.Keys.Aggregate(remaining, (current, stat) => current - allocated[stat]);
            string text = (remaining == 0 ? "0" : remaining.ToString()) + (remaining == 1 ? " point " : " ");
            float width = Main.fontMouseText.MeasureString(text).X * Scale;

            spriteBatch.DrawStringWithShadow(Main.fontMouseText, text, new Vector2(Origin.X + 680, Origin.Y + 238), Color.White, Scale / 1.2f);

            Vector2 buttonPosition = new Vector2(Origin.X + 780, Origin.Y + 260) * Scale;
            spriteBatch.Draw(GFX.GFX.CancelNormal, buttonPosition, Color.White * Scale);

            if (Main.mouseX >= buttonPosition.X && Main.mouseY >= buttonPosition.Y && Main.mouseX <= buttonPosition.X + GFX.GFX.CancelNormal.Width && Main.mouseY <= buttonPosition.Y + GFX.GFX.CancelNormal.Height * Scale)
            // spriteBatch.Draw(GFX.GFX.CancelMouseOver, buttonPosition, Color.White * Scale);
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    PlayerCharacter pc = player.GetModPlayer<PlayerCharacter>();
                    foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
                        allocated[stat] = 0;
                    SoundManager.PlaySound(Sounds.MenuTick);
                    // pc.StatPage = !pc.StatPage;
                    //  CloseUI();
                    return;
                }
            }

            characterStats[PlayerStats.HP].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.HP].Update(spriteBatch, player);

            characterStats[PlayerStats.MP].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.MP].Update(spriteBatch, player);

            characterStats[PlayerStats.STR].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.STR].Update(spriteBatch, player);

            characterStats[PlayerStats.DEX].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.DEX].Update(spriteBatch, player);

            characterStats[PlayerStats.INT].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.INT].Update(spriteBatch, player);

            characterStats[PlayerStats.LUK].Draw(spriteBatch, player, Scale);
            characterStats[PlayerStats.LUK].Update(spriteBatch, player);

            buttonPosition = new Vector2(Origin.X + 670, Origin.Y + 260) * Scale;
            spriteBatch.Draw(GFX.GFX.SaveNormal, buttonPosition, Color.White * Scale);

            if (Main.mouseX >= buttonPosition.X && Main.mouseY >= buttonPosition.Y && Main.mouseX <= buttonPosition.X + GFX.GFX.SaveNormal.Width && Main.mouseY <= buttonPosition.Y + GFX.GFX.SaveNormal.Height)
            //spriteBatch.Draw(GFX.GFX.SaveMouseOver, buttonPosition, Color.White * Scale);
            {
                Main.LocalPlayer.mouseInterface = true;
                if (Main.mouseLeft && Main.mouseLeftRelease)
                {
                    SoundManager.PlaySound(Sounds.MenuTick);
                    foreach (PlayerStats s in allocated.Keys)
                        character.BaseStats[s] += allocated[s];
                    foreach (PlayerStats stat in Enum.GetValues(typeof(PlayerStats)))
                        allocated[stat] = 0;
                    spriteBatch.Draw(GFX.GFX.SavePressed, buttonPosition, Color.White * Scale);
                    //GFX.GFX.SfxLevelUp.Play(0.2f * Main.soundVolume, -0.2f, -0.2f);
                    return;
                }
            }
            // HP
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 33), Color.White);
            //spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.HP).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 35f * Scale), Color.White, 0.8f);
            //MP
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 53), Color.White);
            // spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.MP).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 45f * Scale), Color.White, 0.8f);
            //STR
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 75), Color.White);
            //spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.STR).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 77f * Scale), Color.White, 0.8f);
            //DEX
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 95), Color.White);
            //spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.DEX).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 91f * Scale), Color.White, 0.8f);
            //INT
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 113), Color.White);
            // spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.INT).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 110f * Scale), Color.White, 0.8f);
            //LUK
            spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
            new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 131), Color.White);
            //spriteBatch.DrawStringWithShadow(Main.fontMouseText, character.TotalStats(PlayerStats.LUK).ToString(), new Vector2(StatsPosition.X + 70f * Scale, StatsPosition.Y + 130f * Scale), Color.White, 0.8f);

            PlayerStats? hoverStat = null;
            foreach (PlayerStats s in characterStats.Keys.Where(s => characterStats[s].CheckHover()))
                hoverStat = s;
            if (hoverStat != null)
                spriteBatch.Draw(GFX.GFX.BlueMaxNormal,
                new Vector2(panelOrigin.X + 157f, panelOrigin.Y + 131), Color.White);
        }
        private void DrawInventory(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GFX.GFX.InventoryPanel, new Vector2(Origin.X, Origin.Y + 70), Color.White * Scale);
            float oldInvScale = Main.inventoryScale;
            Main.inventoryScale = Scale - 0.06f; // Scale = grosseur des icons dans l'inventaire
            for (int i = 0; i < 10; i++) // 10 slot par rangees
                for (int j = 1; j < 5; j++) // 5 rangees
                {
                    float x2 = Origin.X + 18 * Scale + i * 53.1f * Scale;
                    float y2 = Origin.Y + 69 * Scale + j * 53.1f * Scale;
                    int id = i + j * 10;

                    if (Main.mouseX >= x2 && Main.mouseX <= x2 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= y2 && Main.mouseY <= y2 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                    !PlayerInput.IgnoreMouseInterface)
                    {
                        Main.player[Main.myPlayer].mouseInterface = true;
                        // sert a pouvoir interagir avec les slots
                        ItemSlot.OverrideHover(Main.player[Main.myPlayer].inventory, 0, id);
                        if (Main.player[Main.myPlayer].inventoryChestStack[id] && (Main.player[Main.myPlayer].inventory[id].type == ItemID.None || Main.player[Main.myPlayer].inventory[id].stack == 0))
                            Main.player[Main.myPlayer].inventoryChestStack[id] = false;

                        if (!Main.player[Main.myPlayer].inventoryChestStack[id])
                        {
                            if (Main.mouseLeftRelease && Main.mouseLeft)
                            {
                                ItemSlot.LeftClick(Main.player[Main.myPlayer].inventory, 0, id);
                                Recipe.FindRecipes();
                            }
                            else
                            {
                                ItemSlot.RightClick(Main.player[Main.myPlayer].inventory, 0, id);
                            }
                        }
                        // sert a pouvoir interagir avec les slots
                        ItemSlot.MouseHover(Main.player[Main.myPlayer].inventory, 0, id);
                    }
                    // si je monte Y ,ca descend // Si je monte X ca bouge a droite
                    SpriteBatchHelper.ItemSlotDrawExtension(Main.spriteBatch, Main.player[Main.myPlayer].inventory, 0, id, new Vector2(x2, y2), Color.White, Color.White);
                    Main.instance.MouseTextHackZoom("");
                }
                
            Main.inventoryScale = oldInvScale;
            Main.instance.invBottom = (int)(436 * Scale);
        }

        public override void PostDraw(SpriteBatch spriteBatch, Player player)
        {
            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();

            for (int i = 0; i < 40; i += 1)
            {
                character.Inventories[character.ActiveInvPage][i] = player.inventory[i + 10];
            }
            if ((int)Main.time % 30 < 1)
            {
                Recipe.FindRecipes();
            }
            if (character.InvPage)
            {
                Vanilla(character.InvPage);
                StatusBar.DrawBuffs();
            }
            if (character.StatPage)
            {
                DrawStatPage(spriteBatch, character, player);
            }
            DrawHotbar();

            spriteBatch.Draw(character.StatPage ? GFX.GFX.MaxPressed : GFX.GFX.MaxNormal, Origin + new Vector2(20, 343) * Scale, Color.White * Scale);

            spriteBatch.Draw(character.ActiveInvPage == 0 ? GFX.GFX.NextPressed : GFX.GFX.NextNormal, Origin + new Vector2(50, 340) * Scale, Color.White * Scale);

            spriteBatch.Draw(character.ActiveInvPage == 1 ? GFX.GFX.NextPressed : GFX.GFX.NextNormal, Origin + new Vector2(80, 340) * Scale, Color.White * Scale);

            spriteBatch.Draw(character.ActiveInvPage == 2 ? GFX.GFX.NextPressed : GFX.GFX.NextNormal, Origin + new Vector2(110, 340) * Scale, Color.White * Scale);

            StatusBar.DrawBuffs();
            //if (character.UnspentPoints())
            //spriteBatch.Draw(GFX.GFX.ClaimMouseOver, PointsOrigin, Color.White * Scale);

        }

        public override bool PreDraw()
        {
            return Main.playerInventory;
        }

        private void Vanilla(bool drawInv)
        {
            try
            {
                Main main = Main.instance;

                if (Main.ShouldPVPDraw)
                    DrawPvp.Invoke(null, null);
                // Scale du equip inventory + poubelle
                Main.inventoryScale = 0.90f;

                //Position de la poubelle

                int TrashX = 22;
                // Y -> Si on descend la valeur, l'image monte!
                int TrashY = 366;  //366

                if (Main.mouseX >= TrashX && Main.mouseX <= TrashX + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= TrashY && Main.mouseY <= TrashY + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                    !PlayerInput.IgnoreMouseInterface)
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                    if (Main.mouseLeftRelease && Main.mouseLeft)
                    {
                        ItemSlot.LeftClick(ref Main.player[Main.myPlayer].trashItem, 6);
                        Recipe.FindRecipes();
                    }

                    ItemSlot.MouseHover(ref Main.player[Main.myPlayer].trashItem, 6);
                }

                ItemSlot.Draw(Main.spriteBatch, ref Main.player[Main.myPlayer].trashItem, 6, new Vector2(TrashX, TrashY));

                if (drawInv)
                    DrawInventory(Main.spriteBatch);

                if (!PlayerInput.UsingGamepad)
                {
                    int num6 = 0;
                    int num7 = 2;
                    int num8 = 32;
                    Player player = Main.player[Main.myPlayer];
                    int num9 = player.InfoAccMechShowWires.ToInt() * 6 + player.rulerLine.ToInt() + player.rulerGrid.ToInt() + player.autoActuator.ToInt() + player.autoPaint.ToInt();
                    if (num9 >= 8)
                        num8 = 2;

                    if (!Main.player[Main.myPlayer].hbLocked)
                        num6 = 1;

                    Main.spriteBatch.Draw(Main.HBLockTexture[num6], new Vector2(num7, num8), new Rectangle(0, 0, Main.HBLockTexture[num6].Width, Main.HBLockTexture[num6].Height), Main.inventoryBack, 0f, default, 0.9f, SpriteEffects.None, 0f);
                    if (Main.mouseX > num7 && Main.mouseX < num7 + Main.HBLockTexture[num6].Width * 0.9f && Main.mouseY > num8 && Main.mouseY < num8 + Main.HBLockTexture[num6].Height * 0.9f)
                    {
                        Main.player[Main.myPlayer].mouseInterface = true;
                        if (!Main.player[Main.myPlayer].hbLocked)
                        {
                            main.MouseText(Language.GetTextValue("LegacyInterface.5"));
                            Main.mouseText = true;
                        }
                        else
                        {
                            main.MouseText(Language.GetTextValue("LegacyInterface.6"));
                            Main.mouseText = true;
                        }

                        if (Main.mouseLeft && Main.mouseLeftRelease)
                        {
                            Main.PlaySound(SoundID.MenuTick);

                            Main.player[Main.myPlayer].hbLocked = !Main.player[Main.myPlayer].hbLocked;
                        }
                    }
                }

                // Mini map

                ItemSlot.DrawRadialDpad(Main.spriteBatch, new Vector2(20f) + new Vector2(56f * Main.inventoryScale * 10f, 56f * Main.inventoryScale * 5f) + new Vector2(26f, 70f));
                if (Main.mapEnabled)
                {
                    bool flag = false;
                    int num10 = Main.screenWidth - 200;
                    int num11 = 40;
                    if (Main.screenWidth < 940)
                        flag = true;

                    if (flag)
                    {
                        num10 = Main.screenWidth - 40;
                        num11 = Main.screenHeight - 2001    ;
                    }

                    for (int k = 0; k < 4; k++)
                    {
                        int num12 = num10 + k * 32;
                        int num13 = num11;
                        if (flag)
                        {
                            num12 = num10;
                            num13 = num11 + k * 32;
                        }

                        int num14 = k;
                        int num15 = 120;
                        if (k > 0 && Main.mapStyle == k - 1)
                            num15 = 200;

                        if (Main.mouseX >= num12 && Main.mouseX <= num12 + 32 && Main.mouseY >= num13 && Main.mouseY <= num13 + 30 && !PlayerInput.IgnoreMouseInterface)
                        {
                            num15 = 255;
                            num14 += 4;
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                                switch (k)
                                {
                                    case 0:
                                        {
                                            Main.playerInventory = false;
                                            Main.player[Main.myPlayer].talkNPC = -1;
                                            Main.npcChatCornerItem = 0;
                                            //Main.PlaySound(10);
                                            SoundManager.PlaySound(Sounds.DoorClosed);
                                            float num16 = 2.5f;
                                            Main.mapFullscreenScale = num16;
                                            Main.mapFullscreen = true;
                                            Main.resetMapFull = true;
                                            break;
                                        }
                                        case 1:
                                            Main.mapStyle = 0;
                                            //Main.PlaySound(12);
                                            SoundManager.PlaySound(Sounds.MenuClose);
                                            break;
                                        case 2:
                                            Main.mapStyle = 1;
                                            //Main.PlaySound(12);
                                            SoundManager.PlaySound(Sounds.MenuClose);
                                            break;
                                        case 3:
                                            Main.mapStyle = 2;
                                            //Main.PlaySound(12);
                                            SoundManager.PlaySound(Sounds.MenuClose);
                                            break;
                                    }
                            }

                            Main.spriteBatch.Draw(Main.mapIconTexture[num14], new Vector2(num12, num13), new Rectangle(0, 0, Main.mapIconTexture[num14].Width, Main.mapIconTexture[num14].Height), new Color(num15, num15, num15, num15), 0f, default, 1f,
                                SpriteEffects.None, 0f);
                        }
                    }    

                    // Option pour cacher l'armure
                    if (Main.armorHide)
                    {
                        Main.armorAlpha -= 0.1f;
                        if (Main.armorAlpha < 0f)
                            Main.armorAlpha = 0f;
                    }
                    else
                    {
                        Main.armorAlpha += 0.025f;
                        if (Main.armorAlpha > 1f)
                            Main.armorAlpha = 1f;
                    }

                    new Color((byte)(Main.mouseTextColor * Main.armorAlpha), (byte)(Main.mouseTextColor * Main.armorAlpha), (byte)(Main.mouseTextColor * Main.armorAlpha), (byte)(Main.mouseTextColor * Main.armorAlpha));
                    Main.armorHide = false;
                    int num17 = (int)PageIcons.Invoke(main, null);
                    if (num17 > -1)
                    {
                        Main.HoverItem = new Item();
                        switch (num17)
                        {
                            case 1:
                                Main.hoverItemName = Language.GetTextValue("LegacyInterface.80"); // Lang.inter[80].Value;
                                break;
                            case 2:
                                Main.hoverItemName = Language.GetTextValue("LegacyInterface.79"); // Lang.inter[79].Value;
                                break;
                            case 3:
                                //Main.hoverItemName = Main.CaptureModeDisabled ? Lang.inter[115].Value : Lang.inter[81].Value;
                                Main.hoverItemName = Main.CaptureModeDisabled ? Language.GetTextValue("LegacyInterface.115") : Language.GetTextValue("LegacyInterface.81");
                                break;
                        }
                    }

                    // Page 2 d'equipement : Hooks, Pets , Mounts ect.

                    if (Main.EquipPage == 2)
                    {
                        Point value = new Point(Main.mouseX, Main.mouseY);
                        Rectangle r = new Rectangle(0, 0, (int)(Main.inventoryBackTexture.Width * Main.inventoryScale), (int)(Main.inventoryBackTexture.Height * Main.inventoryScale));
                        Item[] inv = Main.player[Main.myPlayer].miscEquips;
                        int num18 = Main.screenWidth - 92;
                        int num19 = (int)Mh.GetValue(null) + 174;
                        for (int l = 0; l < 2; l++)
                        {
                            if (l == 0)
                                inv = Main.player[Main.myPlayer].miscEquips;
                            else if (l == 1)
                                inv = Main.player[Main.myPlayer].miscDyes;

                            r.X = num18 + l * -47;
                            for (int m = 0; m < 5; m++)
                            {
                                int context = 0;
                                int num20 = -1;
                                switch (m)
                                {
                                    case 0:
                                        context = 19;
                                        num20 = 0;
                                        break;
                                    case 1:
                                        context = 20;
                                        num20 = 1;
                                        break;
                                    case 2:
                                        context = 18;
                                        break;
                                    case 3:
                                        context = 17;
                                        break;
                                    case 4:
                                        context = 16;
                                        break;
                                }

                                if (l == 1)
                                {
                                    context = 12;
                                    num20 = -1;
                                }

                                r.Y = num19 + m * 47;
                                Texture2D texture2D = Main.inventoryTickOnTexture;
                                if (Main.player[Main.myPlayer].hideMisc[num20])
                                    texture2D = Main.inventoryTickOffTexture;

                                Rectangle r2 = new Rectangle(r.Left + 34, r.Top - 2, texture2D.Width, texture2D.Height);
                                int hiddenVisible = 0;
                                bool flag2 = false;
                                if (r2.Contains(value) && !PlayerInput.IgnoreMouseInterface)
                                {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                flag2 = true;
                                if (Main.mouseLeft && Main.mouseLeftRelease)
                                {
                                    switch (num20)
                                    {
                                        case 0:
                                            Main.player[Main.myPlayer].TogglePet();
                                            break;
                                        case 1:
                                            Main.player[Main.myPlayer].ToggleLight();
                                            break;
                                    }

                                    Main.mouseLeftRelease = false;
                                    Main.PlaySound(SoundID.MenuTick);
                                    if (Main.netMode == NetmodeID.MultiplayerClient)
                                        NetMessage.SendData((int)PacketTypes.PlayerInfo, -1, -1, null, Main.myPlayer);
                                }

                                hiddenVisible = Main.player[Main.myPlayer].hideMisc[num20] ? 2 : 1;
                            }

                            if (r.Contains(value) && !flag2 && !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                Main.armorHide = true;
                                ItemSlot.Handle(inv, context, m);
                            }

                            ItemSlot.Draw(Main.spriteBatch, inv, context, m, r.TopLeft());
                            if (num20 == -1)
                                continue;
                            Main.spriteBatch.Draw(texture2D, r2.TopLeft(), Color.White * 0.7f);
                            if (hiddenVisible <= 0)
                                continue;
                            Main.HoverItem = new Item();

                            Main.hoverItemName = Language.GetTextValue("LegacyInterface." + (58 + hiddenVisible));
                        }
                    }

                    num19 += 247;
                    num18 += 8;
                    int num22 = -1;
                    int num23 = 0;
                    int num24 = 3;
                    int num25 = 260;
                    if (Main.screenHeight > 630 + num25 * (Main.mapStyle == 1).ToInt())
                        num24++;

                    if (Main.screenHeight > 680 + num25 * (Main.mapStyle == 1).ToInt())
                        num24++;

                    if (Main.screenHeight > 730 + num25 * (Main.mapStyle == 1).ToInt())
                        num24++;

                    UILinkPointNavigator.Shortcuts.BUFFS_DRAWN = num23;
                    UILinkPointNavigator.Shortcuts.BUFFS_PER_COLUMN = num24;
                    if (num22 >= 0)
                    {
                        int num30 = Main.player[Main.myPlayer].buffType[num22];
                        if (num30 > 0)
                        {
                            Main.buffString = Lang.GetBuffDescription(num30);
                            switch (num30)
                            {
                                case 26 when Main.expertMode:
                                    Main.buffString = Language.GetTextValue("BuffDescription.WellFed_Expert");
                                    break;
                                case 147:
                                    Main.bannerMouseOver = true;
                                    break;
                                case 94:
                                    {
                                        int num31 = (int)(Main.player[Main.myPlayer].manaSickReduction * 100f) + 1;
                                        Main.buffString = Main.buffString + num31 + "%";
                                        break;
                                    }
                            }

                            int itemRarity = 0;
                            if (Main.meleeBuff[num30])
                                itemRarity = -10;

                            BuffLoader.ModifyBuffTip(num30, ref Main.buffString, ref itemRarity);
                            main.MouseTextHackZoom(Lang.GetBuffName(num30), itemRarity);
                        }
                    }
                }

                // Equip page 1 : Armure et accessories

                else if (Main.EquipPage == 1)
                {
                    UILinkPointNavigator.Shortcuts.NPCS_LastHovered = -1;
                    if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)(Main.screenWidth - 64 - 28 + 56f * Main.inventoryScale) && Main.mouseY > 174 + (int)Mh.GetValue(null) &&
                        Main.mouseY < (int)(174 + (int)Mh.GetValue(null) + 448f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                        Main.player[Main.myPlayer].mouseInterface = true;

                    int num32 = 0;
                    string text = "";
                    int num33 = 0;
                    int num34 = 0;
                    for (int num35 = 0; num35 < Main.npcHeadTexture.Length; num35++)
                    {
                        bool flag3 = false;
                        int num36 = 0;
                        switch (num35)
                        {
                            case 0:
                                flag3 = true;
                                break;
                            case 21:
                                flag3 = false;
                                break;
                            default:
                                {
                                    for (int num37 = 0; num37 < 200; num37++)
                                    {
                                        if (!Main.npc[num37].active || NPC.TypeToHeadIndex(Main.npc[num37].type) != num35)
                                            continue;
                                        flag3 = true;
                                        num36 = num37;
                                        break;
                                    }

                                    break;
                                }
                            }

                            if (flag3)
                            {
                                int num38 = Main.screenWidth - 64 - 28 + num34;
                                int num39 = (int)(174 + (int)Mh.GetValue(null) + num32 * 56 * Main.inventoryScale) + num33;
                                Color white = new Color(100, 100, 100, 100);
                                if (num39 > Main.screenHeight - 80)
                                {
                                    num34 -= 48;
                                    num33 -= num39 - (174 + (int)Mh.GetValue(null));
                                    num38 = Main.screenWidth - 64 - 28 + num34;
                                    num39 = (int)(174 + (int)Mh.GetValue(null) + num32 * 56 * Main.inventoryScale) + num33;
                                    if (UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn == 100)
                                        UILinkPointNavigator.Shortcuts.NPCS_IconsPerColumn = num32;
                                }

                                if (Main.mouseX >= num38 && Main.mouseX <= num38 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num39 && Main.mouseY <= num39 + Main.inventoryBackTexture.Height * Main.inventoryScale)
                                {
                                    UILinkPointNavigator.Shortcuts.NPCS_LastHovered = num36;
                                    Main.mouseText = true;

                                    text = num35 == 0 ? Language.GetTextValue("LegacyInterface.8") : Main.npc[num36].FullName;

                                    if (!PlayerInput.IgnoreMouseInterface)
                                    {
                                        Main.player[Main.myPlayer].mouseInterface = true;
                                        if (Main.mouseLeftRelease && Main.mouseLeft && !PlayerInput.UsingGamepadUI && Main.mouseItem.type == ItemID.None)
                                        {
                                            Main.PlaySound(SoundID.MenuTick);
                                            main.mouseNPC = num35;
                                            Main.mouseLeftRelease = false;
                                        }
                                    }
                                }

                                UILinkPointNavigator.SetPosition(600 + num32, new Vector2(num38, num39) + Main.inventoryBackTexture.Size() * 0.75f);
                                Texture2D texture = Main.inventoryBack11Texture;
                                Color white2 = Main.inventoryBack;
                                if (UILinkPointNavigator.CurrentPoint - 600 == num32)
                                {
                                    texture = Main.inventoryBack14Texture;
                                    white2 = Color.White;
                                }

                                Main.spriteBatch.Draw(texture, new Vector2(num38, num39), new Rectangle(0, 0, Main.inventoryBackTexture.Width, Main.inventoryBackTexture.Height), white2, 0f, default, Main.inventoryScale, SpriteEffects.None, 0f);
                                white = Color.White;
                                int num40 = num35;
                                float s = 1f;
                                float num41;
                                if (Main.npcHeadTexture[num40].Width > Main.npcHeadTexture[num40].Height)
                                    num41 = Main.npcHeadTexture[num40].Width;
                                else
                                    num41 = Main.npcHeadTexture[num40].Height;

                                if (num41 > 36f)
                                    s = 36f / num41;

                                Main.spriteBatch.Draw(Main.npcHeadTexture[num40], new Vector2(num38 + 26f * Main.inventoryScale, num39 + 26f * Main.inventoryScale), new Rectangle(0, 0, Main.npcHeadTexture[num40].Width, Main.npcHeadTexture[num40].Height),
                                    white, 0f, new Vector2(Main.npcHeadTexture[num40].Width / 2f, Main.npcHeadTexture[num40].Height / 2f), s, SpriteEffects.None, 0f);
                                num32++;
                            }

                            UILinkPointNavigator.Shortcuts.NPCS_IconsTotal = num32;
                        }

                        if (text != "" && Main.mouseItem.type == ItemID.None)
                            main.MouseText(text);
                    }
                    else
                    {
                        int num42 = 4;
                        if (Main.mouseX > Main.screenWidth - 64 - 28 && Main.mouseX < (int)(Main.screenWidth - 64 - 28 + 56f * Main.inventoryScale) && Main.mouseY > 174 + (int)Mh.GetValue(null) &&
                            Main.mouseY < (int)(174 + (int)Mh.GetValue(null) + 448f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                            Main.player[Main.myPlayer].mouseInterface = true;

                        float num43 = Main.inventoryScale;
                        int num44 = 8 + Main.player[Main.myPlayer].extraAccessorySlots;
                        bool flag4 = false;
                        int num45 = num44 - 1;
                        if (num44 == 8 && (Main.player[Main.myPlayer].armor[8].type > ItemID.None || Main.player[Main.myPlayer].armor[18].type > ItemID.None || Main.player[Main.myPlayer].dye[8].type > ItemID.None))
                        {
                            num44 = 9;
                            flag4 = true;
                            num45 = 7;
                        }

                        if (Main.screenHeight < 900 && num45 == 8)
                           num45--;

                        Color color = Main.inventoryBack;
                        Color color2 = new Color(80, 80, 80, 80);
                        for (int num46 = 0; num46 < num44; num46++)
                        {
                            bool flag5 = flag4 && num46 == num44 - 1 && Main.mouseItem.type > ItemID.None;
                            int num47 = Main.screenWidth - 64 - 28;
                            int num48 = (int)(174 + (int)Mh.GetValue(null) + num46 * 56 * Main.inventoryScale);
                            new Color(100, 100, 100, 100);
                            if (num46 > 2)
                                num48 += num42;

                            if (num46 == num45)
                            {
                                Vector2 vector = new Vector2(num47 - 10 - 47 - 47 - 14, num48 + Main.inventoryBackTexture.Height * 0.5f);
                                Main.spriteBatch.Draw(Main.extraTexture[58], vector, null, Color.White, 0f, Main.extraTexture[58].Size() / 2f, Main.inventoryScale, SpriteEffects.None, 0f);
                                Vector2 value2 = Main.fontMouseText.MeasureString(Main.player[Main.myPlayer].statDefense.ToString());
                                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, Main.player[Main.myPlayer].statDefense.ToString(), vector - value2 * 0.5f * Main.inventoryScale, Color.White, 0f, Vector2.Zero,
                                    new Vector2(Main.inventoryScale));
                                if (Utils.CenteredRectangle(vector, Main.extraTexture[58].Size()).Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;

                                    string value3 = Main.player[Main.myPlayer].statDefense + " " + Language.GetTextValue("LegacyInterface.10");

                                    if (!string.IsNullOrEmpty(value3))
                                        Main.hoverItemName = value3;
                                }

                                UILinkPointNavigator.SetPosition(1557, vector + Main.extraTexture[58].Size() * Main.inventoryScale / 4f);
                            }

                            int context2 = 8;
                            if (num46 > 2)
                                context2 = 10;

                            Texture2D texture2D2 = Main.inventoryTickOnTexture;
                            if (Main.player[Main.myPlayer].hideVisual[num46])
                                texture2D2 = Main.inventoryTickOffTexture;

                            int num49 = Main.screenWidth - 58;
                            int num50 = (int)(172 + (int)Mh.GetValue(null) + num46 * 56 * Main.inventoryScale);
                            if (num46 > 2)
                                num50 += num42;

                            Rectangle rectangle = new Rectangle(num49, num50, texture2D2.Width, texture2D2.Height);
                            int num51 = 0;
                            if (num46 >= 3 && num46 < num44 && rectangle.Contains(new Point(Main.mouseX, Main.mouseY)) && !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                Main.player[Main.myPlayer].hideVisual[num46] = !Main.player[Main.myPlayer].hideVisual[num46];
                                Main.PlaySound(SoundID.MenuTick);
                            }

                            num51 = Main.player[Main.myPlayer].hideVisual[num46] ? 2 : 1;
                        }
                        else if (Main.mouseX >= num47 && Main.mouseX <= num47 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num48 && Main.mouseY <= num48 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                 !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.armorHide = true;
                            Main.player[Main.myPlayer].mouseInterface = true;
                            ItemSlot.OverrideHover(Main.player[Main.myPlayer].armor, context2, num46);
                            if (!flag5 && Main.mouseLeftRelease && Main.mouseLeft)
                                ItemSlot.LeftClick(Main.player[Main.myPlayer].armor, context2, num46);

                            ItemSlot.MouseHover(Main.player[Main.myPlayer].armor, context2, num46);
                        }

                        if (flag4 && num46 == num44 - 1)
                            Main.inventoryBack = color2;

                        ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].armor, context2, num46, new Vector2(num47, num48));
                        if (num46 <= 2 || num46 >= num44)
                            continue;
                        Main.spriteBatch.Draw(texture2D2, new Vector2(num49, num50), Color.White * 0.7f);
                        if (num51 <= 0)
                            continue;
                        Main.HoverItem = new Item();
                        Main.hoverItemName = Language.GetTextValue("LegacyInterface." + (58 + num51));
                    }

                    Main.inventoryBack = color;

                    if (Main.mouseX > Main.screenWidth - 64 - 28 - 47 && Main.mouseX < (int)(Main.screenWidth - 64 - 20 - 47 + 56f * Main.inventoryScale) && Main.mouseY > 174 + (int)Mh.GetValue(null) &&
                        Main.mouseY < (int)(174 + (int)Mh.GetValue(null) + 168f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                        Main.player[Main.myPlayer].mouseInterface = true;

                    for (int num52 = 10; num52 < 10 + num44; num52++)
                    {
                        bool flag6 = false || flag4 && num52 == 10 + num44 - 1 && Main.mouseItem.type > ItemID.None;
                        int num53 = Main.screenWidth - 64 - 28 - 47;
                        int num54 = (int)(174 + (int)Mh.GetValue(null) + (num52 - 10) * 56 * Main.inventoryScale);
                        new Color(100, 100, 100, 100);
                        if (num52 > 12)
                            num54 += num42;

                        int context3 = 9;
                        if (num52 > 12)
                            context3 = 11;

                        if (Main.mouseX >= num53 && Main.mouseX <= num53 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num54 && Main.mouseY <= num54 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                            !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            Main.armorHide = true;
                            ItemSlot.OverrideHover(Main.player[Main.myPlayer].armor, context3, num52);
                            if (!flag6)
                            {
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                    ItemSlot.LeftClick(Main.player[Main.myPlayer].armor, context3, num52);
                                else
                                    ItemSlot.RightClick(Main.player[Main.myPlayer].armor, context3, num52);
                            }

                            ItemSlot.MouseHover(Main.player[Main.myPlayer].armor, context3, num52);
                        }

                        if (flag4 && num52 == num44 + 10 - 1)
                            Main.inventoryBack = color2;

                        ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].armor, context3, num52, new Vector2(num53, num54));
                    }

                    Main.inventoryBack = color;
                    if (Main.mouseX > Main.screenWidth - 64 - 28 - 47 && Main.mouseX < (int)(Main.screenWidth - 64 - 20 - 47 + 56f * Main.inventoryScale) && Main.mouseY > 174 + (int)Mh.GetValue(null) &&
                        Main.mouseY < (int)(174 + (int)Mh.GetValue(null) + 168f * Main.inventoryScale) && !PlayerInput.IgnoreMouseInterface)
                        Main.player[Main.myPlayer].mouseInterface = true;

                    for (int num55 = 0; num55 < num44; num55++)
                    {
                        bool flag7 = flag4 && num55 == num44 - 1 && Main.mouseItem.type > ItemID.None;
                        int num56 = Main.screenWidth - 64 - 28 - 47 - 47;
                        int num57 = (int)(174 + (int)Mh.GetValue(null) + num55 * 56 * Main.inventoryScale);
                        new Color(100, 100, 100, 100);
                        if (num55 > 2)
                            num57 += num42;

                        if (Main.mouseX >= num56 && Main.mouseX <= num56 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num57 && Main.mouseY <= num57 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                            !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            Main.armorHide = true;
                            ItemSlot.OverrideHover(Main.player[Main.myPlayer].dye, 12, num55);
                            if (!flag7)
                            {
                                if (Main.mouseRightRelease && Main.mouseRight)
                                    ItemSlot.RightClick(Main.player[Main.myPlayer].dye, 12, num55);
                                else if (Main.mouseLeftRelease && Main.mouseLeft)
                                    ItemSlot.LeftClick(Main.player[Main.myPlayer].dye, 12, num55);
                            }

                            ItemSlot.MouseHover(Main.player[Main.myPlayer].dye, 12, num55);
                        }

                        if (flag4 && num55 == num44 - 1)
                            Main.inventoryBack = color2;

                        ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].dye, 12, num55, new Vector2(num56, num57));
                    }

                    Main.inventoryBack = color;
                    Main.inventoryScale = num43;
                }
                // Crafting menu du Goblin Tinkerer

                int num58 = (Main.screenHeight - 600) / 2;
                int num59 = (int)(Main.screenHeight / 600f * 250f);
                if (Main.screenHeight < 700)
                {
                    num58 = (Main.screenHeight - 508) / 2;
                    num59 = (int)(Main.screenHeight / 600f * 200f);
                }
                else if (Main.screenHeight < 850)
                {
                    num59 = (int)(Main.screenHeight / 600f * 225f);
                    }

                    if (Main.craftingHide)
                    {
                        Main.craftingAlpha -= 0.1f;
                        if (Main.craftingAlpha < 0f)
                            Main.craftingAlpha = 0f;
                    }
                    else
                    {
                        Main.craftingAlpha += 0.025f;
                        if (Main.craftingAlpha > 1f)
                            Main.craftingAlpha = 1f;
                    }

                    Color color3 = new Color((byte)(Main.mouseTextColor * Main.craftingAlpha), (byte)(Main.mouseTextColor * Main.craftingAlpha), (byte)(Main.mouseTextColor * Main.craftingAlpha), (byte)(Main.mouseTextColor * Main.craftingAlpha));
                    Main.craftingHide = false;
                    if (Main.InReforgeMenu)
                    {
                        if ((bool)MouseReforge.GetValue(null))
                        {
                            if ((float)ReforgeScale.GetValue(null) < 1f)
                                ReforgeScale.SetValue(null, (float)ReforgeScale.GetValue(null) + 0.02f);
                        }
                        else if ((float)ReforgeScale.GetValue(null) > 1f)
                        {
                            ReforgeScale.SetValue(null, (float)ReforgeScale.GetValue(null) - 0.02f);
                        }

                        if (Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 || Main.player[Main.myPlayer].talkNPC == -1 || Main.InGuideCraftMenu)
                        {
                            Main.InReforgeMenu = false;
                            Main.player[Main.myPlayer].dropItemCheck();
                            Recipe.FindRecipes();
                        }
                        else
                        {
                            int num60 = 50;
                            int num61 = 448;
                            string text2 = Language.GetTextValue("LegacyInterface.46") + ": ";
                            if (Main.reforgeItem.type > ItemID.None)
                            {
                                int num62 = Main.reforgeItem.value;
                                if (Main.player[Main.myPlayer].discount)
                                    num62 = (int)(num62 * 0.8);

                                num62 /= 3;
                                string text3 = "";
                                int num63 = 0;
                                int num64 = 0;
                                int num65 = 0;
                                int num66 = 0;
                                int num67 = num62;
                                if (num67 < 1)
                                    num67 = 1;

                                if (num67 >= 1000000)
                                {
                                    num63 = num67 / 1000000;
                                    num67 -= num63 * 1000000;
                                }

                                if (num67 >= 10000)
                                {
                                    num64 = num67 / 10000;
                                    num67 -= num64 * 10000;
                                }

                                if (num67 >= 100)
                                {
                                    num65 = num67 / 100;
                                    num67 -= num65 * 100;
                                }

                                if (num67 >= 1)
                                    num66 = num67;

                                if (num63 > 0)
                                {
                                    object obj = text3;
                                    text3 = string.Concat(obj, "[c/", Colors.AlphaDarken(Colors.CoinPlatinum).Hex3(), ":", num63, " ", Language.GetTextValue("LegacyInterface.15"), "] ");
                                }

                                if (num64 > 0)
                                {
                                    object obj2 = text3;
                                    text3 = string.Concat(obj2, "[c/", Colors.AlphaDarken(Colors.CoinGold).Hex3(), ":", num64, " ", Language.GetTextValue("LegacyInterface.16"), "] ");
                                }

                                if (num65 > 0)
                                {
                                    object obj3 = text3;
                                    text3 = string.Concat(obj3, "[c/", Colors.AlphaDarken(Colors.CoinSilver).Hex3(), ":", num65, " ", Language.GetTextValue("LegacyInterface.17"), "] ");
                                }

                                if (num66 > 0)
                                {
                                    object obj4 = text3;
                                    text3 = string.Concat(obj4, "[c/", Colors.AlphaDarken(Colors.CoinCopper).Hex3(), ":", num66, " ", Language.GetTextValue("LegacyInterface.18"), "] ");
                                }

                                // Savings pig
                                ItemSlot.DrawSavings(Main.spriteBatch, num60 + 270, main.invBottom - 60, true);
                                ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, text3, new Vector2(num60 + 50 + Main.fontMouseText.MeasureString(text2).X, num61), Color.White, 0f, Vector2.Zero, Vector2.One);
                                int num68 = num60 + 70;
                                int num69 = num61 + 40;
                                bool flag8 = Main.mouseX > num68 - 15 && Main.mouseX < num68 + 15 && Main.mouseY > num69 - 15 && Main.mouseY < num69 + 15 && !PlayerInput.IgnoreMouseInterface;
                                Texture2D texture2D3 = Main.reforgeTexture[0];
                                if (flag8)
                                    texture2D3 = Main.reforgeTexture[1];

                                Main.spriteBatch.Draw(texture2D3, new Vector2(num68, num69), null, Color.White, 0f, texture2D3.Size() / 2f, (float)ReforgeScale.GetValue(null), SpriteEffects.None, 0f);
                                UILinkPointNavigator.SetPosition(304, new Vector2(num68, num69) + texture2D3.Size() / 4f);
                                if (flag8)
                                {
                                    Main.hoverItemName = Language.GetTextValue("LegacyInterface.19");
                                    if (!(bool)MouseReforge.GetValue(null))
                                    Main.PlaySound(SoundID.MenuTick);

                                    MouseReforge.SetValue(null, true);
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.mouseLeftRelease && Main.mouseLeft && Main.player[Main.myPlayer].CanBuyItem(num62))
                                    {
                                        ItemLoader.PreReforge(Main.reforgeItem);
                                        Main.reforgeItem.position.X = Main.player[Main.myPlayer].position.X + (float)(Main.player[Main.myPlayer].width / 2.0) - (float)(Main.reforgeItem.width / 2.0);
                                        Main.reforgeItem.position.Y = Main.player[Main.myPlayer].position.Y + (float)(Main.player[Main.myPlayer].height / 2.0) - (float)(Main.reforgeItem.height / 2.0);
                                        ItemText.NewText(Main.reforgeItem, Main.reforgeItem.stack, true);
                                        Main.PlaySound(SoundID.Item37);

                                    }
                                }
                                else
                                {
                                    MouseReforge.SetValue(null, false);
                                }
                            }
                            else
                            {
                                text2 = Language.GetTextValue("LegacyInterface.20");
                            }

                            ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, Main.fontMouseText, text2, new Vector2(num60 + 50, num61), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f,
                                Vector2.Zero, Vector2.One);
                            if (Main.mouseX >= num60 && Main.mouseX <= num60 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num61 && Main.mouseY <= num61 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                Main.craftingHide = true;
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                {
                                    ItemSlot.LeftClick(ref Main.reforgeItem, 5);
                                    Recipe.FindRecipes();
                                }
                                else
                                {
                                    ItemSlot.RightClick(ref Main.reforgeItem, 5);
                                }

                                ItemSlot.MouseHover(ref Main.reforgeItem, 5);
                            }

                            ItemSlot.Draw(Main.spriteBatch, ref Main.reforgeItem, 5, new Vector2(num60, num61));
                        }
                    }
                    // Craft menu du Guide 

                    else if (Main.InGuideCraftMenu)
                    {
                        if (Main.player[Main.myPlayer].chest != -1 || Main.npcShop != 0 || Main.player[Main.myPlayer].talkNPC == -1 || Main.InReforgeMenu)
                        {
                            Main.InGuideCraftMenu = false;
                            Main.player[Main.myPlayer].dropItemCheck();
                            Recipe.FindRecipes();
                        }
                        else
                        {
                            int num70 = 73;
                            int num71 = 331;
                            num71 += num58;
                            string text4;
                            if (Main.guideItem.type > ItemID.None)
                            {
                                text4 = Language.GetTextValue("LegacyInterface.21") + " " + Main.guideItem.Name;
                                Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.22"), new Vector2(num70, num71 + 118), color3, 0f, default, 1f, SpriteEffects.None, 0f);
                                int num72 = Main.focusRecipe;
                                int num73 = 0;
                                int num74 = 0;
                                while (num74 < Recipe.maxRequirements)
                                {
                                    int num75 = (num74 + 1) * 26;
                                    if (Main.recipe[Main.availableRecipe[num72]].requiredTile[num74] == -1)
                                    {
                                        if (num74 == 0 && !Main.recipe[Main.availableRecipe[num72]].needWater && !Main.recipe[Main.availableRecipe[num72]].needHoney && !Main.recipe[Main.availableRecipe[num72]].needLava &&
                                            !Main.recipe[Main.availableRecipe[num72]].needSnowBiome)
                                            Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.23"), new Vector2(num70, num71 + 118 + num75), color3, 0f, default, 1f, SpriteEffects.None, 0f);

                                        break;
                                    }

                                    num73++;
                                    Main.spriteBatch.DrawString(Main.fontMouseText, Lang.GetMapObjectName(MapHelper.TileToLookup(Main.recipe[Main.availableRecipe[num72]].requiredTile[num74], 0)), new Vector2(num70, num71 + 118 + num75), color3, 0f,
                                        default, 1f, SpriteEffects.None, 0f);
                                    num74++;
                                }

                                if (Main.recipe[Main.availableRecipe[num72]].needWater)
                                {
                                    int num76 = (num73 + 1) * 26;
                                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.53"), new Vector2(num70, num71 + 118 + num76), color3, 0f, default, 1f, SpriteEffects.None, 0f);
                                }

                                if (Main.recipe[Main.availableRecipe[num72]].needHoney)
                                {
                                    int num77 = (num73 + 1) * 26;
                                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.58"), new Vector2(num70, num71 + 118 + num77), color3, 0f, default, 1f, SpriteEffects.None, 0f);
                                }

                                if (Main.recipe[Main.availableRecipe[num72]].needLava)
                                {
                                    int num78 = (num73 + 1) * 26;
                                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.56"), new Vector2(num70, num71 + 118 + num78), color3, 0f, default, 1f, SpriteEffects.None, 0f);
                                }

                                if (Main.recipe[Main.availableRecipe[num72]].needSnowBiome)
                                {
                                    int num79 = (num73 + 1) * 26;
                                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.123"), new Vector2(num70, num71 + 118 + num79), color3, 0f, default, 1f, SpriteEffects.None, 0f);
                                }
                            }
                            else
                            {
                                text4 = Language.GetTextValue("LegacyInterface.24");
                            }

                            Main.spriteBatch.DrawString(Main.fontMouseText, text4, new Vector2(num70 + 50, num71 + 12), new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default, 1f, SpriteEffects.None,
                                0f);
                            new Color(100, 100, 100, 100);
                            if (Main.mouseX >= num70 && Main.mouseX <= num70 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num71 && Main.mouseY <= num71 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                Main.craftingHide = true;
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                {
                                    ItemSlot.LeftClick(ref Main.guideItem, 7);
                                    Recipe.FindRecipes();
                                }
                                else
                                {
                                    ItemSlot.RightClick(ref Main.guideItem, 7);
                                }

                                ItemSlot.MouseHover(ref Main.guideItem, 7);
                            }

                            ItemSlot.Draw(Main.spriteBatch, ref Main.guideItem, 7, new Vector2(num70, num71));
                        }
                    }

                    // La scroll bar pour les recipes

                    if (!Main.InReforgeMenu)
                    {
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                        if (Main.numAvailableRecipes > 0)
                            Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.25"), new Vector2(76f, 414 + num58), color3, 0f, default, 1f, SpriteEffects.None, 0f);

                        for (int Scale = 0; Scale < Recipe.maxRecipes; Scale++)
                        {
                            Main.inventoryScale = 100f / (Math.Abs(Main.availableRecipeY[Scale]) + 100f);
                            if (Main.inventoryScale < 0.75)
                                Main.inventoryScale = 0.75f;

                            if (Main.recFastScroll)
                                Main.inventoryScale = 0.75f;

                            if (Main.availableRecipeY[Scale] < (Scale - Main.focusRecipe) * 65)
                            {
                                if (Math.Abs(Main.availableRecipeY[Scale]) < .01 && !Main.recFastScroll)
                                    Main.PlaySound(SoundID.MenuTick);


                                Main.availableRecipeY[Scale] += 6.5f;
                                if (Main.recFastScroll)
                                    Main.availableRecipeY[Scale] += 130000f;

                                if (Main.availableRecipeY[Scale] > (Scale - Main.focusRecipe) * 65f)
                                    Main.availableRecipeY[Scale] = (Scale - Main.focusRecipe) * 65f;
                            }
                            else if (Main.availableRecipeY[Scale] > (Scale - Main.focusRecipe) * 65f)
                            {
                                if (Math.Abs(Main.availableRecipeY[Scale]) < .01 && !Main.recFastScroll)
                                Main.PlaySound(SoundID.MenuTick);

                                Main.availableRecipeY[Scale] -= 6.5f;
                                if (Main.recFastScroll)
                                    Main.availableRecipeY[Scale] -= 130000f;

                                if (Main.availableRecipeY[Scale] < (Scale - Main.focusRecipe) * 65f)
                                    Main.availableRecipeY[Scale] = (Scale - Main.focusRecipe) * 65f;
                            }
                            else
                            {
                                Main.recFastScroll = false;
                            }

                            if (Scale < Main.numAvailableRecipes && Math.Abs(Main.availableRecipeY[Scale]) <= num59)
                            {
                                int num81 = (int)(46f - 26f * Main.inventoryScale);

                            // position scrollbar recipe

                                int Position = (int)(500f + Main.availableRecipeY[Scale] * Main.inventoryScale - 30f * Main.inventoryScale + num58);
                                double num83 = Main.inventoryBack.A + 50;
                                double num84 = 255.0;
                                if (Math.Abs(Main.availableRecipeY[Scale]) > num59 - 100f)
                                {
                                    num83 = 150f * (100f - (Math.Abs(Main.availableRecipeY[Scale]) - (num59 - 100f))) * 0.01;
                                    num84 = 255f * (100f - (Math.Abs(Main.availableRecipeY[Scale]) - (num59 - 100f))) * 0.01;
                                }

                                new Color((byte)num83, (byte)num83, (byte)num83, (byte)num83);
                                Color lightColor = new Color((byte)num84, (byte)num84, (byte)num84, (byte)num84);
                                if (Main.mouseX >= num81 && Main.mouseX <= num81 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= Position && Main.mouseY <= Position + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                    !PlayerInput.IgnoreMouseInterface)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.focusRecipe == Scale && Main.guideItem.type == ItemID.None && Main.LocalPlayer.itemTime == 0 && Main.LocalPlayer.itemAnimation == 0)
                                    {
                                        if ((Main.mouseItem.type == ItemID.None || Main.mouseItem.IsTheSameAs(Main.recipe[Main.availableRecipe[Scale]].createItem) &&
                                             Main.mouseItem.stack + Main.recipe[Main.availableRecipe[Scale]].createItem.stack <= Main.mouseItem.maxStack) && !Main.player[Main.myPlayer].IsStackingItems())
                                        {
                                            if (Main.mouseLeftRelease && Main.mouseLeft)
                                            {
                                                RecipeHelper.CraftItem(Main.recipe[Main.availableRecipe[Scale]]);
                                            }
                                            else if (Main.stackSplit <= 1 && Main.mouseRight && (Main.mouseItem.stack < Main.mouseItem.maxStack || Main.mouseItem.type == ItemID.None))
                                            {
                                                Main.stackSplit = Main.stackSplit == 0 ? 15 : Main.stackDelay;
                                                RecipeHelper.CraftItem(Main.recipe[Main.availableRecipe[Scale]]);
                                            }
                                        }
                                    }
                                    else if (Main.mouseLeftRelease && Main.mouseLeft)
                                    {
                                        Main.focusRecipe = Scale;
                                    }

                                    Main.craftingHide = true;
                                    Main.hoverItemName = Main.recipe[Main.availableRecipe[Scale]].createItem.Name;
                                    Main.HoverItem = Main.recipe[Main.availableRecipe[Scale]].createItem.Clone();
                                    if (Main.recipe[Main.availableRecipe[Scale]].createItem.stack > 1)
                                    {
                                        object obj5 = Main.hoverItemName;
                                        Main.hoverItemName = string.Concat(obj5, " (", Main.recipe[Main.availableRecipe[Scale]].createItem.stack, ")");
                                    }
                                }

                                if (Main.numAvailableRecipes <= 0)
                                    continue;
                                num83 -= 50.0;
                                if (num83 < 0.0)
                                    num83 = 0.0;

                                if (Scale == Main.focusRecipe)
                                    UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = 0;
                                else
                                    UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;

                                Color color4 = Main.inventoryBack;
                                Main.inventoryBack = new Color((byte)num83, (byte)num83, (byte)num83, (byte)num83);
                                ItemSlot.Draw(Main.spriteBatch, ref Main.recipe[Main.availableRecipe[Scale]].createItem, 22, new Vector2(num81, Position), lightColor);
                                Main.inventoryBack = color4;
                            }
                        }

                        if (Main.numAvailableRecipes > 0)
                        {
                            UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                            UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                            for (int num85 = 0; num85 < Recipe.maxRequirements; num85++)
                            {
                                if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.None)
                                {
                                    UILinkPointNavigator.Shortcuts.CRAFT_CurrentIngridientsCount = num85 + 1;
                                    break;
                                }

                                // Position recipe en focus

                                int FocusRecipeX = 80 + num85 * 40;
                                int FocusRecipeY = 480 + num58;
                                double num88 = Main.inventoryBack.A + 50.0;
                                Color white3 = Color.White;
                                Color white4 = Color.White;
                                num88 = Main.inventoryBack.A + 50 - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f;
                                double num89 = 255f - Math.Abs(Main.availableRecipeY[Main.focusRecipe]) * 2f;
                                if (num88 < 0.0)
                                    num88 = 0.0;

                                if (num89 < 0.0)
                                    num89 = 0.0;


                                white3.R = white3.G = white3.B = white3.A = (byte)num88;
                                white4.R = white4.G = white4.B = white4.A = (byte)num89;

                                Main.inventoryScale = 0.6f;
                                if (num88 == 0.0)
                                    break;

                                if (Main.mouseX >= FocusRecipeX && Main.mouseX <= FocusRecipeX + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= FocusRecipeY && Main.mouseY <= FocusRecipeY + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                    !PlayerInput.IgnoreMouseInterface)
                                {
                                    Main.craftingHide = true;
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    Main.hoverItemName = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].Name;
                                    Main.HoverItem = Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].Clone();
                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].ProcessGroupsForText(Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type, out string nameOverride))
                                        Main.HoverItem.SetNameOverride(nameOverride);

                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].anyIronBar && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.IronBar)
                                        Main.HoverItem.SetNameOverride(Language.GetTextValue("LegacyInterface.37") + " " + Lang.GetItemNameValue(22));
                                    else if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].anyWood && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.Wood)
                                        Main.HoverItem.SetNameOverride(Language.GetTextValue("LegacyInterface.37") + " " + Lang.GetItemNameValue(9));
                                    else if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].anySand && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.SandBlock)
                                        Main.HoverItem.SetNameOverride(Language.GetTextValue("LegacyInterface.37") + " " + Lang.GetItemNameValue(169));
                                    else if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].anyFragment && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.FragmentSolar)
                                        Main.HoverItem.SetNameOverride(Language.GetTextValue("LegacyInterface.37") + " " + Language.GetTextValue("LegacyInterface.51"));
                                    else if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].anyPressurePlate && Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].type == ItemID.GrayPressurePlate)
                                        Main.HoverItem.SetNameOverride(Language.GetTextValue("LegacyInterface.37") + " " + Language.GetTextValue("LegacyInterface.38"));

                                    if (Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].stack > 1)
                                    {
                                        object obj6 = Main.hoverItemName;
                                        Main.hoverItemName = string.Concat(obj6, " (", Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85].stack, ")");
                                    }
                                }

                                num88 -= 50.0;
                                if (num88 < 0.0)
                                    num88 = 0.0;

                                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = 1 + num85;
                                Color color5 = Main.inventoryBack;
                                Main.inventoryBack = new Color((byte)num88, (byte)num88, (byte)num88, (byte)num88);
                                ItemSlot.Draw(Main.spriteBatch, ref Main.recipe[Main.availableRecipe[Main.focusRecipe]].requiredItem[num85], 22, new Vector2(FocusRecipeX, FocusRecipeY));
                                Main.inventoryBack = color5;
                            }
                        }

                        if (Main.numAvailableRecipes == 0)
                        {
                            Main.recBigList = false;
                        }
                        else
                        {
                            int num90 = 94;
                            int num91 = 450 + num58;
                            if (Main.InGuideCraftMenu)
                                num91 -= 150;

                            bool flag9 = Main.mouseX > num90 - 15 && Main.mouseX < num90 + 15 && Main.mouseY > num91 - 15 && Main.mouseY < num91 + 15 && !PlayerInput.IgnoreMouseInterface;
                            int num92 = Main.recBigList.ToInt() * 2 + flag9.ToInt();
                            Main.spriteBatch.Draw(Main.craftToggleTexture[num92], new Vector2(num90, num91), null, Color.White, 0f, Main.craftToggleTexture[num92].Size() / 2f, 1f, SpriteEffects.None, 0f);
                            if (flag9)
                            {
                                main.MouseText(Language.GetTextValue("GameUI.CraftingWindow"));
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseLeft && Main.mouseLeftRelease)
                                {
                                Main.PlaySound(SoundID.MenuTick);
                                Main.recBigList = !Main.recBigList;
                                }
                            }
                        }
                    }

                    if (Main.recBigList)
                    {
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = -1;
                        UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeSmall = -1;
                        int num93 = 42;
                        if (Main.inventoryScale < 0.75)
                            Main.inventoryScale = 0.75f;

                        int num94 = Main.instance.invBottom + 48; //340;
                        int num95 = 310;
                        int num96 = (Main.screenWidth - num95 - 280) / num93;
                        int num97 = (Main.screenHeight - num94 - 20) / num93;
                        UILinkPointNavigator.Shortcuts.CRAFT_IconsPerRow = num96;
                        UILinkPointNavigator.Shortcuts.CRAFT_IconsPerColumn = num97;
                        int num98 = 0;
                        int num99 = 0;
                        int num100 = num95;
                        int num101 = num94;
                        int num102 = num95 - 20;
                        int num103 = num94 + 2;
                        if (Main.recStart > Main.numAvailableRecipes - num96 * num97)
                        {
                            Main.recStart = Main.numAvailableRecipes - num96 * num97;
                            if (Main.recStart < 0)
                                Main.recStart = 0;
                        }

                        if (Main.recStart > 0)
                        {
                            if (Main.mouseX >= num102 && Main.mouseX <= num102 + Main.craftUpButtonTexture.Width && Main.mouseY >= num103 && Main.mouseY <= num103 + Main.craftUpButtonTexture.Height && !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                {
                                    Main.recStart -= num96;
                                    if (Main.recStart < 0)
                                        Main.recStart = 0;

                                    Main.PlaySound(SoundID.MenuTick);
                                    Main.mouseLeftRelease = false;
                                }
                            }

                                Main.spriteBatch.Draw(Main.craftUpButtonTexture, new Vector2(num102, num103), new Rectangle(0, 0, Main.craftUpButtonTexture.Width, Main.craftUpButtonTexture.Height), new Color(200, 200, 200, 200), 0f, default, 1f,
                                SpriteEffects.None, 0f);
                        }

                        if (Main.recStart < Main.numAvailableRecipes - num96 * num97)
                        {
                            num103 += 20;
                            if (Main.mouseX >= num102 && Main.mouseX <= num102 + Main.craftUpButtonTexture.Width && Main.mouseY >= num103 && Main.mouseY <= num103 + Main.craftUpButtonTexture.Height && !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                {
                                    Main.recStart += num96;
                                    Main.PlaySound(SoundID.MenuTick);
                                    if (Main.recStart > Main.numAvailableRecipes - num96)
                                        Main.recStart = Main.numAvailableRecipes - num96;

                                    Main.mouseLeftRelease = false;
                                }
                            }

                            Main.spriteBatch.Draw(Main.craftDownButtonTexture, new Vector2(num102, num103), new Rectangle(0, 0, Main.craftUpButtonTexture.Width, Main.craftUpButtonTexture.Height), new Color(200, 200, 200, 200), 0f, default, 1f,
                                SpriteEffects.None, 0f);
                        }

                        int num104 = Main.recStart;
                        while (num104 < Recipe.maxRecipes && num104 < Main.numAvailableRecipes)
                        {
                            int num105 = num100;
                            int num106 = num101;
                            double num107 = Main.inventoryBack.A + 50;
                            double num108 = 255.0;
                            new Color((byte)num107, (byte)num107, (byte)num107, (byte)num107);
                            new Color((byte)num108, (byte)num108, (byte)num108, (byte)num108);
                            if (Main.mouseX >= num105 && Main.mouseX <= num105 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num106 && Main.mouseY <= num106 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                !PlayerInput.IgnoreMouseInterface)
                            {
                                Main.player[Main.myPlayer].mouseInterface = true;
                                if (Main.mouseLeftRelease && Main.mouseLeft)
                                {
                                    Main.focusRecipe = num104;
                                    Main.recFastScroll = true;
                                    Main.recBigList = false;
                                    Main.PlaySound(SoundID.MenuTick);
                                    Main.mouseLeftRelease = false;
                                    if (PlayerInput.UsingGamepadUI)
                                        UILinkPointNavigator.ChangePage(9);
                                }

                                Main.craftingHide = true;
                                Main.hoverItemName = Main.recipe[Main.availableRecipe[num104]].createItem.Name;
                                Main.HoverItem = Main.recipe[Main.availableRecipe[num104]].createItem.Clone();
                                if (Main.recipe[Main.availableRecipe[num104]].createItem.stack > 1)
                                {
                                    object obj7 = Main.hoverItemName;
                                    Main.hoverItemName = string.Concat(obj7, " (", Main.recipe[Main.availableRecipe[num104]].createItem.stack, ")");
                                }
                            }

                            if (Main.numAvailableRecipes > 0)
                            {
                                num107 -= 50.0;
                                if (num107 < 0.0)
                                    num107 = 0.0;

                                UILinkPointNavigator.Shortcuts.CRAFT_CurrentRecipeBig = num104 - Main.recStart;
                                Color color6 = Main.inventoryBack;
                                Main.inventoryBack = new Color((byte)num107, (byte)num107, (byte)num107, (byte)num107);
                                ItemSlot.Draw(Main.spriteBatch, ref Main.recipe[Main.availableRecipe[num104]].createItem, 22, new Vector2(num105, num106));
                                Main.inventoryBack = color6;
                            }

                            num100 += num93;
                            num98++;
                            if (num98 >= num96)
                            {
                                num100 = num95;
                                num101 += num93;
                                num98 = 0;
                                num99++;
                                if (num99 >= num97)
                                    break;
                            }

                            num104++;
                        }
                    }
                    // COINS SLOTS

                    Vector2 vector2 = Main.fontMouseText.MeasureString("Coins");
                    Vector2 vector3 = Main.fontMouseText.MeasureString(Language.GetTextValue("LegacyInterface.26"));
                    float num109 = vector2.X / vector3.X;

                   // emplacement du texte "coins"                                                                      -21 = position horizontal-position Vertical  (+10 que les slots)
                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.26"), new Vector2(-21 + (int)(600 * Scale), 11f + (vector2.Y - vector2.Y * num109) / 1.5f),
                        new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default, 0.75f * num109, SpriteEffects.None, 0f);
                    Main.inventoryScale = 0.7f;

                    // nombre de coins slot
                    for (int num110 = 0; num110 < 4; num110++)
                    {
                        // emplacement de la case coins 
                        //     (X=)  40 = (int)(600 * scale);
                        int num111 = -22 + (int)(600 * Scale);
                        //            (Y=) 140f + num110 * 56 * Main.inventoryScale + 20f;
                        int num112 = (int)(1f + num110 * 56 * Main.inventoryScale + 30f);
                        int slot = num110 + 50;
                        new Color(100, 100, 100, 100);
                        if (Main.mouseX >= num111 && Main.mouseX <= num111 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num112 && Main.mouseY <= num112 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                            !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            ItemSlot.OverrideHover(Main.player[Main.myPlayer].inventory, 1, slot);
                            if (Main.mouseLeftRelease && Main.mouseLeft)
                            {
                                ItemSlot.LeftClick(Main.player[Main.myPlayer].inventory, 1, slot);
                                Recipe.FindRecipes();
                            }
                            else
                            {
                                ItemSlot.RightClick(Main.player[Main.myPlayer].inventory, 1, slot);
                            }

                            ItemSlot.MouseHover(Main.player[Main.myPlayer].inventory, 1, slot);
                        }

                        ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].inventory, 1, slot, new Vector2(num111, num112));
                    }
                    // AMMO SLOTS

                    Vector2 vector4 = Main.fontMouseText.MeasureString("Ammo");
                    Vector2 vector5 = Main.fontMouseText.MeasureString(Language.GetTextValue("LegacyInterface.27"));
                    float num113 = vector4.X / vector5.X;
                    // emplacement du texte " Ammo"                                                    Defaults:         horizontal                   vertical                               
                    Main.spriteBatch.DrawString(Main.fontMouseText, Language.GetTextValue("LegacyInterface.27"), new Vector2(-20 + (int)(600 * Scale), 190f + (vector4.Y - vector4.Y * num113) / 2f),
                        new Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default, 0.75f * num113, SpriteEffects.None, 0f);
                    Main.inventoryScale = 0.7f;
                    //nombre de ammo slots
                    for (int num114 = 0; num114 < 4; num114++)
                    {
                        //       (X=)78 + (int)(600 * Scale); 
                        int num115 = -22 + (int)(600 * Scale);
                        //      (Y=) (int)(140f + num114 * 56 * Main.inventoryScale + 20f); //was 85
                        int num116 = (int)(180f + num114 * 56 * Main.inventoryScale + 30f); //was 85
                        int slot2 = 54 + num114;
                        new Color(100, 100, 100, 100);
                        if (Main.mouseX >= num115 && Main.mouseX <= num115 + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= num116 && Main.mouseY <= num116 + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                            !PlayerInput.IgnoreMouseInterface)
                        {
                            Main.player[Main.myPlayer].mouseInterface = true;
                            ItemSlot.OverrideHover(Main.player[Main.myPlayer].inventory, 2, slot2);
                            if (Main.mouseLeftRelease && Main.mouseLeft)
                            {
                                ItemSlot.LeftClick(Main.player[Main.myPlayer].inventory, 2, slot2);
                                Recipe.FindRecipes();
                            }
                            else
                            {
                                ItemSlot.RightClick(Main.player[Main.myPlayer].inventory, 2, slot2);
                            }

                            ItemSlot.MouseHover(Main.player[Main.myPlayer].inventory, 2, slot2);
                        }

                        ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].inventory, 2, slot2, new Vector2(num115, num116));
                    }
                    // NPC SHOP

                    if (Main.npcShop > 0 && (!Main.playerInventory || Main.player[Main.myPlayer].talkNPC == -1))
                        Main.npcShop = 0;

                    if (Main.npcShop > 0 && !Main.recBigList)
                    {
                        Utils.DrawBorderStringFourWay(Main.spriteBatch, Main.fontMouseText, Language.GetTextValue("LegacyInterface.28"), 550f, main.invBottom, Color.Yellow * (Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, Scale / 1.4f);
                        ItemSlot.DrawSavings(Main.spriteBatch, 550f, main.invBottom);

                        // Scale des items slot dans le shop
                        Main.inventoryScale = 0.85f;
                        if (Main.mouseX > 73 && Main.mouseX < (int)(73f + 560f * Main.inventoryScale) && Main.mouseY > main.invBottom && Main.mouseY < (int)(main.invBottom + 100f * Main.inventoryScale*2) && !PlayerInput.IgnoreMouseInterface)
                            Main.player[Main.myPlayer].mouseInterface = true;

                        // nombre de slot (10 x 4 )
                        for (int VerticalSlot = 0; VerticalSlot < 10; VerticalSlot++)
                            for (int HorizontalSlot = 0; HorizontalSlot < 4; HorizontalSlot++)
                            {
                                // X si on augment le nombre, ca se deplace vers la droite
                                int ShopX = (int)(75f + VerticalSlot * 56 * Main.inventoryScale); //616
                                // Y si on augmente le nombre, ca se deplace vers le haut  // -318
                                int ShopY = (int)((main.invBottom - 35) + HorizontalSlot * 56 * Main.inventoryScale);

                                // ne pas oublier de changer "10" pour "4" si je reduit le nombre de slot par ligne du shop
                                int slot3 = VerticalSlot + HorizontalSlot * 10;
                                new Color(100, 100, 100, 100);
                                if (Main.mouseX >= ShopX && Main.mouseX <= ShopX + Main.inventoryBackTexture.Width * Main.inventoryScale && Main.mouseY >= ShopY && Main.mouseY <= ShopY + Main.inventoryBackTexture.Height * Main.inventoryScale &&
                                    !PlayerInput.IgnoreMouseInterface)
                                {
                                    Main.player[Main.myPlayer].mouseInterface = true;
                                    if (Main.mouseLeftRelease && Main.mouseLeft)
                                        ItemSlot.LeftClick(main.shop[Main.npcShop].item, 15, slot3);
                                    else
                                        ItemSlot.RightClick(main.shop[Main.npcShop].item, 15, slot3);

                                    ItemSlot.MouseHover(main.shop[Main.npcShop].item, 15, slot3);
                                }

                                ItemSlot.Draw(Main.spriteBatch, main.shop[Main.npcShop].item, 15, slot3, new Vector2(ShopX, ShopY), Color.Transparent);


                        }
                    }

                    if (Main.player[Main.myPlayer].chest > -1 && !Main.tileContainer[Main.tile[Main.player[Main.myPlayer].chestX, Main.player[Main.myPlayer].chestY].type])
                    {
                        Main.player[Main.myPlayer].chest = -1;
                        Recipe.FindRecipes();
                    }

                    int offsetDown = 0;
                    if (!PlayerInput.UsingGamepad)
                        offsetDown = 9999;

                    UIVirtualKeyboard.OffsetDown = offsetDown;
                    ChestUI.Draw(Main.spriteBatch);

                   // Stack chest icon
                    if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
                    {
                        int num121 = 0;
                        int ChestStackX = 420; //470 
                        int ChestStackY = (int)Math.Round(300f * Scale) + 40;  // +90
                        int width = Main.chestStackTexture[num121].Width;
                        int height = Main.chestStackTexture[num121].Height;
                        UILinkPointNavigator.SetPosition(301, new Vector2(ChestStackX + width * 0.75f, ChestStackY + height * 0.75f));
                        if (Main.mouseX >= ChestStackX && Main.mouseX <= ChestStackX + width && Main.mouseY >= ChestStackY && Main.mouseY <= ChestStackY + height && !PlayerInput.IgnoreMouseInterface)
                        {
                            num121 = 1;
                            if (!(bool)AllChestStackHover.GetValue(null))
                            {
                            Main.PlaySound(SoundID.MenuTick);
                            AllChestStackHover.SetValue(null, true);
                            }

                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                Main.mouseLeftRelease = false;
                                Main.player[Main.myPlayer].QuickStackAllChests();
                                Recipe.FindRecipes();
                            }

                            Main.player[Main.myPlayer].mouseInterface = true;
                        }
                        else if ((bool)AllChestStackHover.GetValue(null))
                        {
                        Main.PlaySound(SoundID.MenuTick);
                        AllChestStackHover.SetValue(null, false);
                        }

                        Main.spriteBatch.Draw(Main.chestStackTexture[num121], new Vector2(ChestStackX, ChestStackY), new Rectangle(0, 0, Main.chestStackTexture[num121].Width, Main.chestStackTexture[num121].Height), Color.White, 0f, default, 1f, SpriteEffects.None,
                            0f);
                        if (!Main.mouseText && num121 == 1)
                            main.MouseText(Language.GetTextValue("Stacks to nearby chests"));
                    }

                    // sort inventory
                    if (Main.player[Main.myPlayer].chest == -1 && Main.npcShop == 0)
                    {
                        int num124 = 0;
                        int SortInvX = 465;
                        int SortInvY = (int)Math.Round(300f * Scale) + 40;
                        int num127 = 30;
                        int num128 = 30;
                        UILinkPointNavigator.SetPosition(302, new Vector2(SortInvX + num127 * 0.75f, SortInvY + num128 * 0.75f));
                        bool flag10 = false;
                        if (Main.mouseX >= SortInvX && Main.mouseX <= SortInvX + num127 && Main.mouseY >= SortInvY && Main.mouseY <= SortInvY + num128 && !PlayerInput.IgnoreMouseInterface)
                        {
                            num124 = 1;
                            flag10 = true;
                            Main.player[Main.myPlayer].mouseInterface = true;
                            if (Main.mouseLeft && Main.mouseLeftRelease)
                            {
                                Main.mouseLeftRelease = false;
                                ItemSorting.SortInventory();
                                Recipe.FindRecipes();
                            }
                        }

                        if (flag10 != (bool)InventorySortMouseOver.GetValue(null))
                        {
                        Main.PlaySound(SoundID.MenuTick);
                        InventorySortMouseOver.SetValue(null, flag10);
                        }

                        Texture2D texture2 = Main.inventorySortTexture[(bool)InventorySortMouseOver.GetValue(null) ? 1 : 0];
                        Main.spriteBatch.Draw(texture2, new Vector2(SortInvX, SortInvY), null, Color.White, 0f, default, 1f, SpriteEffects.None, 0f);
                        if (!Main.mouseText && num124 == 1)
                            main.MouseText(Language.GetTextValue("GameUI.SortInventory"));
                    }
                }
            catch (SystemException e)
            {
                Main.NewText(e.ToString());
            }
        }
    }
}