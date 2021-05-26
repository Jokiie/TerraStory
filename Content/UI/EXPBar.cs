using System;
using TerraStory.Content.GFX;
using TerraStory.Content.UI.Base;
using TerraStory.Content.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace TerraStory.Content.UI
{
    public class EXPBar : BaseUI
    {
        private int BarXpLength = Main.screenWidth;

        private int BarXpThickness = 7;

        private readonly Vector2 barXpOrigin;

        private PlayerCharacter character;

        //public static InterfaceScaleType GetInterfaceScaleType { get; set; }

        public EXPBar(PlayerCharacter character)
        {
            Main.player[Main.myPlayer].hbLocked = false;   //hb = hotbar
            this.character = character;

            barXpOrigin = new Vector2(20, -5);
        }
        //float leftOffset = Main.screenWidth / 2.5f;
        //float topOffset = Main.screenHeight - 60f;
        public static void DrawNumerals(SpriteBatch spriteBatch, int level, float scale)
        {
            Main.instance.MouseTextHackZoom("");
            spriteBatch.Draw(GFX.GFX.LevelLv, new Vector2(Main.screenWidth / 2.3f + 15, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
            SpriteEffects.None, 0f);
            if (level < 10)
            {
                spriteBatch.Draw(GFX.GFX.LevelNum[level], new Vector2(Main.screenWidth / 2.3f + 34, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
            }
            else if (level < 100)
            {
                spriteBatch.Draw(GFX.GFX.LevelNum[level / 10], new Vector2(Main.screenWidth / 2.3f + 34, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
                spriteBatch.Draw(GFX.GFX.LevelNum[level % 10], new Vector2(Main.screenWidth / 2.3f + 40, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
            }
            else if (level < 1000)
            {
                spriteBatch.Draw(GFX.GFX.LevelNum[level / 100], new Vector2(Main.screenWidth / 2.3f + 34, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
                spriteBatch.Draw(GFX.GFX.LevelNum[level % 100 / 10], new Vector2(Main.screenWidth / 2.3f + 40, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
                spriteBatch.Draw(GFX.GFX.LevelNum[level % 10], new Vector2(Main.screenWidth / 2.3f + 46, Main.screenHeight - 87f), null, Color.White, 0f, Vector2.Zero, scale * 1.2f,
                    SpriteEffects.None, 0f);
            }
            if (Main.player[Main.myPlayer].name.Length > 10)
            {
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, Main.player[Main.myPlayer].name, new Vector2(Main.screenWidth / 2.1f + Main.player[Main.myPlayer].name.Length, Main.screenHeight - 87f), Color.White, scale * 0.8f);
            }
            else
            {
                spriteBatch.DrawStringWithShadow(Main.fontMouseText, Main.player[Main.myPlayer].name, new Vector2(Main.screenWidth / 2.1f + 45 - Main.player[Main.myPlayer].name.Length, Main.screenHeight - 90f), Color.White, scale);
            }
        }

        // scale le status bar selon la grosseur du screen
        //private static float Scale => 0.90f;
        private static float Scale => 1f;

        public override void PostDraw(SpriteBatch spriteBatch, Player player)
        {

            character = player.GetModPlayer<PlayerCharacter>();
            Main.instance.MouseTextHackZoom("");
            int leftOffset = 5;

            float topOffset = Main.screenHeight - 10;

            //Draw the  exp bar background.  If you change the color it will only effect the exp bar.
            spriteBatch.Draw(GFX.GFX.ExpBarLayerBack, new Vector2(leftOffset, topOffset), null, Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            //calculate the exp bar length
            int currentXpLength = (int)Math.Round((decimal)character.Experience / character.ExperienceToLevel() * BarXpLength );

            //Draw the exp bar
            spriteBatch.Draw(GFX.GFX.ExpBarGauge, new Vector2(leftOffset, topOffset + 7) + barXpOrigin * Scale, new Rectangle((int)(barXpOrigin.X + BarXpLength - currentXpLength), (int)barXpOrigin.Y, currentXpLength, BarXpThickness), Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, 0f);

            //Draw the exp bar background.  If you change the color it will only effect the exp bar.
            spriteBatch.Draw(GFX.GFX.ExpBarLayerCover, new Vector2(leftOffset + 175, topOffset + 10), null, Color.White, 0f, Vector2.Zero, Scale * 0.90f, SpriteEffects.None, 0f) ;

            //This should SpellDraw roman numerals for the characters level.... but doesn't seem to work.
            DrawNumerals(spriteBatch, player.GetModPlayer<PlayerCharacter>().Level, Scale);

            //draw the current exp in numbers
            spriteBatch.DrawStringWithShadow(Main.fontMouseText, (decimal)character.Experience + " / " + character.ExperienceToLevel(), new Vector2(Main.screenWidth / 2.4f, topOffset + 2) + new Vector2(barXpOrigin.X * Scale + 100, barXpOrigin.Y * Scale), Color.White, 0.6f * Scale);
        }
    }
}