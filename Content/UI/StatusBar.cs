using System;
using System.Reflection;
using TerraStory.Content.GFX;
using TerraStory.Content.UI.Base;
using TerraStory.Content.Players;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;
using TerraStory.Content.SFX;
using System.Diagnostics;

namespace TerraStory.Content.UI
{
    public class StatusBar : BaseUI
    {

        private const int BarLifeLength = 171;

        private const int BarLifeThickness = 13;

        private const int BarManaLength = 171;

        private const int BarManaThickness = 13;

        private const int BubblesLength = 132;

        private const int BubblesThickness = 22;

        private readonly Vector2 barLifeOrigin;

        private readonly Vector2 barManaOrigin;

        private readonly Vector2 bubblesOrigin;

        private static readonly MethodInfo DrawBuffIcon = typeof(Main).GetMethod("DrawBuffIcon", BindingFlags.NonPublic | BindingFlags.Static);

        private PlayerCharacter character;

        public StatusBar(PlayerCharacter character)
        {
            Main.player[Main.myPlayer].hbLocked = false;   //hb = hotbar
            this.character = character;
            // Emplacement de la bar de vie et de mana
            barLifeOrigin = new Vector2(22, 4);
            barManaOrigin = new Vector2(22, 20);
            bubblesOrigin = new Vector2(40, -60);

            AddButton(
                () => new Rectangle((int)PointsOrigin.X, (int)PointsOrigin.Y, (int)(GFX.GFX.UnspentPoints.Width * Scale),
                    (int)(GFX.GFX.UnspentPoints.Height * Scale)), delegate (Player player)
                    {
                        character.CloseUI();
                        SoundManager.PlaySound(Sounds.MenuClose);
                        character.InventoryUI.UIActive = player.GetModPlayer<PlayerCharacter>().UnspentPoints() && !Main.playerInventory;
                    }, delegate
                    {
                        Main.LocalPlayer.mouseInterface = true;
                        string s = Main.player[Main.myPlayer].GetModPlayer<PlayerCharacter>().UnspentPoints()
                            ? "Click here to allocate stat points"
                            : "You have no unspent stat points";
                        Main.instance.MouseText(s);
                    });
        }
        private static Vector2 PointsOrigin => new Vector2(Main.screenWidth / 2, Main.screenHeight / 2) * Scale;

        private static float Scale => Math.Min(1f, Main.screenWidth / Constants.MaxScreenWidth + 0.4f);

        public static void DrawBuffs()
        {
            Main.instance.MouseTextHackZoom("");
            if (Main.playerInventory || Main.player[Main.myPlayer].ghost)
            {
                return;
            }
            // Position de la premiere ligne de buff
            int leftOffset = 20;
            // Grosseur des icons
            const int iconWidth = 38;
            // Nombre max de buff
            const int maxSlots = 21;

            int buffTypeId = -1;
            const int secondRowOfBuffsStartIndex = 11;

            for (int buffSlot = 0; buffSlot <= maxSlots; buffSlot++)

                if (Main.player[Main.myPlayer].buffType[buffSlot] > 0)
                {
                    int buff = Main.player[Main.myPlayer].buffType[buffSlot];
                    //position de la premiere ligne de buff
                    int xPosition = leftOffset + buffSlot * iconWidth;

                    int yPosition = 75; //The y offset for the first row...
                    if (buffSlot >= secondRowOfBuffsStartIndex)
                    {
                        // Position de la second ligne de buff
                        xPosition = leftOffset + (buffSlot - secondRowOfBuffsStartIndex) * iconWidth;
                        yPosition += 45; //put icon on second row.
                    }

                    buffTypeId = (int)DrawBuffIcon.Invoke(null,
                        new object[] { buffTypeId, buffSlot, buff, xPosition, yPosition }); // Main.DrawBuffIcon(num, i, b, x, num3);
                }
                else
                {
                    Main.buffAlpha[buffSlot] = 0.4f;
                }

            if (buffTypeId < 0)
                return;

            int buffId = Main.player[Main.myPlayer].buffType[buffTypeId];

            if (buffId <= 0)
                return;

            Main.buffString = Lang.GetBuffDescription(buffId);

            int itemRarity = 0;

            switch ((VanillaBuffId)buffId)
            {
                case VanillaBuffId.WellFed when Main.expertMode:
                    Main.buffString = Language.GetTextValue("BuffDescription.WellFed_Expert");
                    break;
                case VanillaBuffId.MonsterBanner:
                    Main.bannerMouseOver = true;
                    break;
                case VanillaBuffId.ManaSickness:
                    {
                        int percent = (int)(Main.player[Main.myPlayer].manaSickReduction * 100f) + 1;
                        Main.buffString = Main.buffString + percent + "%";
                        break;
                    }
            }

            if (Main.meleeBuff[buffId])
                itemRarity = -10;

            BuffLoader.ModifyBuffTip(buffId, ref Main.buffString, ref itemRarity);
        }
        /// <summary>
        ///     This function draws the hotbar in the upper left screen when the user DOES NOT have the inventory window open.
        /// </summary>
        private void DrawHotbar()
        {
            //DrawSelectedItemName();
            Main.instance.MouseTextHackZoom("");
            //Get the width of the screen, subtract 480 from it which will be the distance from the left we need to be able to SpellDraw the bar
            int leftOffset = 15;
            //For each slot
            for (int slotIndex = 0; slotIndex < 10; slotIndex++)
            {
                //This code provides the animation of the selected item growing and shrinking as they scroll through the hotbar
                //The selected item will grow from .75 to 1 in scale when it is selected
                //and it will shrink from 1 to .75 when deselected.

                //If this slot is selected by the player
                if (slotIndex == Main.player[Main.myPlayer].selectedItem)
                {
                    //if the hotbar scale is less that 1
                    if (Main.hotbarScale[slotIndex] < 1f)
                        //Add .05 to it to make it larger
                        Main.hotbarScale[slotIndex] += 0.05f;
                }
                //Otherwise it is a slot that isn't selected.
                //So we check if the scale is greater that .75
                else if (Main.hotbarScale[slotIndex] > 0.75)
                {
                    //We shrink the scale of the slot.
                    Main.hotbarScale[slotIndex] -= 0.05f;
                }

                float itemHotbarScale = Main.hotbarScale[slotIndex] ;

                int topOffset = (int)(20f + 22f * (1f - itemHotbarScale));

                //If the user is mousing over the slot.
                if (!Main.player[Main.myPlayer].hbLocked && !PlayerInput.IgnoreMouseInterface && Main.mouseX >= leftOffset &&
                    Main.mouseX <= leftOffset + Main.inventoryBackTexture.Width * Main.hotbarScale[slotIndex] && Main.mouseY >= topOffset &&
                    Main.mouseY <= topOffset + Main.inventoryBackTexture.Height * Main.hotbarScale[slotIndex] && !Main.player[Main.myPlayer].channel)
                {
                    Main.player[Main.myPlayer].mouseInterface = true;
                    Main.player[Main.myPlayer].showItemIcon = false;

                    //If the user clicked the left mouse button on the item...
                    if (Main.mouseLeft && !Main.player[Main.myPlayer].hbLocked && !Main.blockMouse)
                        //Change the user's active item slot to this slot.
                        Main.player[Main.myPlayer].changeItem = slotIndex;

                    //Not sure what affixname does, but this sets the mouse text = to the item in the slot that you are hovering over.
                    Main.hoverItemName = Main.player[Main.myPlayer].inventory[slotIndex].AffixName();

                    //If they have more than one, show in parens the amount.
                    if (Main.player[Main.myPlayer].inventory[slotIndex].stack > 1)
                        Main.hoverItemName = string.Concat(Main.hoverItemName, " (", Main.player[Main.myPlayer].inventory[slotIndex].stack, ")");

                    //If the item is rare, set the rare flag.
                    Main.rare = Main.player[Main.myPlayer].inventory[slotIndex].rare;
                }
                //save InventoryScale
                float originalInventoryScale = Main.inventoryScale;
                Main.inventoryScale = itemHotbarScale;
                ItemSlot.Draw(Main.spriteBatch, Main.player[Main.myPlayer].inventory, 13, slotIndex, new Vector2(leftOffset, topOffset), Color.White);

                //Restore Inventory Scale.
                Main.inventoryScale = originalInventoryScale;

                //Move to the left offset for the next button.
                leftOffset += (int)(Main.inventoryBackTexture.Width * Main.hotbarScale[slotIndex]) + 4;
            }
        }

        /// <summary>
        ///     This happens after the SpellDraw event
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="player"></param>
        public override void Draw(SpriteBatch spriteBatch, Player player)
        {
            try
            {
                character = player.GetModPlayer<PlayerCharacter>();

                float leftOffset = Main.screenWidth / 2.3f;
                float topOffset = Main.screenHeight - 70f;

                spriteBatch.Draw(GFX.GFX.StatusBarsCover1, new Vector2(leftOffset, topOffset - 25), null, Color.White, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);
                //Draw le background de la barre de vie et de mana
                spriteBatch.Draw(GFX.GFX.StatusBarsBG, new Vector2(leftOffset, topOffset), null, Color.White, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);

                //Calcul la longueur actuelle de la barre de vie
                int currentLifeLength = (int)Math.Round(player.statLife / (decimal)player.statLifeMax2 * BarLifeLength);

                spriteBatch.Draw(GFX.GFX.HPLayer, new Vector2(leftOffset, topOffset) + barLifeOrigin * Scale * 1.5f, new Rectangle((int)(barLifeOrigin.X + BarLifeLength - currentLifeLength), //This how much of the bar should be blacked out.
                    (int)barLifeOrigin.Y, currentLifeLength, BarLifeThickness), Color.White, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);

                //Calcul la longueur actuelle de la barre de mana
                int currentManaLength = (int)Math.Round(player.statMana / (decimal)player.statManaMax2 * BarManaLength);

                spriteBatch.Draw(GFX.GFX.MPLayer, new Vector2(leftOffset, topOffset) + barManaOrigin * Scale * 1.5f, new Rectangle((int)(barManaOrigin.X + BarManaLength - currentManaLength), //How much of the bar should be blacked out.
                    (int)barManaOrigin.Y, currentManaLength, BarManaThickness), Color.White, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);

                //le text du la barre de vie
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, player.statLife + " / " + player.statLifeMax2, new Vector2(leftOffset, topOffset) + new Vector2(barLifeOrigin.X * Scale + 65, (barLifeOrigin.Y) * Scale * 1.5f), Color.White, Scale);

                //Le text de la barre demana
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, player.statMana + " / " + player.statManaMax2, new Vector2(leftOffset, topOffset) + new Vector2(barManaOrigin.X * Scale + 65, (barManaOrigin.Y) * Scale * 1.5f), Color.White, Scale);

                spriteBatch.Draw(GFX.GFX.StatusBarsCover, new Vector2(leftOffset, topOffset), null, Color.White, 0f, Vector2.Zero, Scale * 1.5f, SpriteEffects.None, 0f);

                //  Main.spriteBatch.End();

                if (player.lavaTime < player.lavaMax || player.breath < player.breathMax)
                {
                    // Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Main.UIScale  * 1.5fMatrix);

                    if (player.lavaTime < player.lavaMax)
                    {
                        int currentBubbles = (int)Math.Round((decimal)BubblesLength * player.lavaTime / player.lavaMax);
                        spriteBatch.Draw(GFX.GFX.BubblesLava, new Vector2(leftOffset, topOffset) + bubblesOrigin * 1.2f, new Rectangle(0, 0, currentBubbles, BubblesThickness), Color.White);
                    }

                    if (player.breath < player.breathMax)
                    {
                        int currentBubbles = (int)Math.Round((decimal)BubblesLength * player.breath / player.breathMax);
                        spriteBatch.Draw(GFX.GFX.BubblesWater, new Vector2(leftOffset, topOffset) + bubblesOrigin * 1.2f, new Rectangle(0, 0, currentBubbles, BubblesThickness), Color.White);
                    }
                }
                Main.buffString = "";
                Main.bannerMouseOver = false;
                if (!Main.recBigList)
                    Main.recStart = 0;
                if (!Main.ingameOptionsWindow && !Main.playerInventory && !Main.inFancyUI)
                    DrawBuffs();
                if(!Main.playerInventory)
                  DrawHotbar();

            }
            catch (Exception e)
            {
                TerraStory.LogMessage("StatusBar PostDraw Error: " + e);
            }
        }
    }
}



