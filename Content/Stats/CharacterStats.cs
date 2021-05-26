using System;
using System.Linq;
using TerraStory.Content.GFX;
using TerraStory.Content.UI;
using TerraStory.Content.Players;
using TerraStory.Content.SFX;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;

namespace TerraStory.Content.Stats
{
    public class CharacterStats
    {


        public CharacterStats(InventoryUI inventoryUI, PlayerStats id, Func<Vector2> position, Texture2D texture)
        {
            InventoryUI = inventoryUI;
            Id = id;
            Position = position;
            Texture = texture;
        }

        private int Allocated
        {
            get => InventoryUI.allocated[Id];
            set => InventoryUI.allocated[Id] = value;
        }
        public bool StatPage { get; set; } = true;
        public int ActiveStatPage { get; set; }
        private PlayerStats Id { get; }
        private InventoryUI InventoryUI { get; }

        private Func<Vector2> Position { get; }
        private Texture2D Texture { get; }

        public bool CheckHover()
        {
            // > = PLUS GRAND QUE  // < = PLUS PETIT QUE
            return Main.mouseX >= Position().X && Main.mouseY >= Position().Y && Main.mouseX <= Position().X + Texture.Width * 6 &&
                   Main.mouseY <= Position().Y + 12;
        }

        public void Draw(SpriteBatch spriteBatch, Player player, float scale)
        {
            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();
            string text = (Allocated + character.BaseStats[Id]).ToString();
            float width = Main.fontItemStack.MeasureString(text).X;
            spriteBatch.DrawStringWithShadow(Main.fontItemStack, text, Position() + new Vector2(28f - width / 2f, 36f) * scale,
                Allocated > 0 ? Color.Lime : Color.White, scale);
        }
        public Vector2 Origin => new Vector2(2f, 10f) * Scale;
        private Vector2 UIPosition => new Vector2(Origin.X + 600, Origin.Y + 90);
        private static float Scale => 0.92f;

        public void Update(SpriteBatch spriteBatch, Player player)
        {

            int total = InventoryUI.allocated.Keys.Sum(stat => InventoryUI.allocated[stat]);

            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();
            if (Main.mouseX >= Position().X + 40 && Main.mouseX <= Position().X + 40 + Texture.Width * Scale && Main.mouseY >= Position().Y + 30 && Main.mouseY <= Position().Y + 30 + Texture.Height * Scale &&
            !PlayerInput.IgnoreMouseInterface)
            {
                Main.player[Main.myPlayer].mouseInterface = true;


                switch (Id)
                {
                    case PlayerStats.HP:
                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increase your life points by 5 per point.",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 30f), Color.White);

                        if (Allocated == 0)
                            // Click to allocate
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 33), Color.White);
                        else
                            // Allocated
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 33), Color.White);


                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 33), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)

                        {

                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMinPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 33), Color.White);
                        }
                        break;

                    case PlayerStats.MP:
                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increase your mana points by 5 per point.",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 50f), Color.White);
                        if (Allocated == 0)
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 53), Color.White);
                        else
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 53), Color.White);


                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 53), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 53), Color.White);
                        }
                        break;

                    case PlayerStats.STR:

                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increase your melee and cannon damage by 1% per point",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 72f), Color.White);
                        if (Allocated == 0)
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 75), Color.White);
                        else
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 75), Color.White);

                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 75), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 75), Color.White);
                        }

                        break;

                    case PlayerStats.DEX:

                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increase your ranged and thrown damage by 1% per point.",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 92f), Color.White);
                        if (Allocated == 0)
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 95), Color.White);
                        else
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 95), Color.White);


                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 95), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 95), Color.White);
                        }
                        break;

                    case PlayerStats.INT:

                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increases your magic damage by 1% per point.",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 110f), Color.White);
                        if (Allocated == 0)
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 113), Color.White);
                        else
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 113), Color.White);


                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 113), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 113), Color.White);
                        }
                        break;

                    case PlayerStats.LUK:
                        spriteBatch.DrawStringWithShadow(Main.fontMouseText, "Increase your minion damage and \n" +
                            "all critical rate chance by 1% per point.",
                            new Vector2(UIPosition.X + 200, UIPosition.Y + 128f), Color.White);
                        if (Allocated == 0)
                            spriteBatch.Draw(GFX.GFX.BlueMaxMouseOver,
                                 new Vector2(UIPosition.X + 157f, UIPosition.Y + 131), Color.White);
                        else
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed,
                                new Vector2(UIPosition.X + 157f, UIPosition.Y + 131), Color.White);


                        if (Main.mouseLeft && Main.mouseLeftRelease && total + character.PointsAllocated < character.Level - 1)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated += 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 131), Color.White);
                        }

                        if (Main.mouseRight && Main.mouseRightRelease && Allocated > 0)
                        {
                            //Main.PlaySound(SoundID.MenuTick);
                            SoundManager.PlaySound(Sounds.MenuTick);
                            Allocated -= 1;
                            spriteBatch.Draw(GFX.GFX.BlueMaxPressed, new Vector2(UIPosition.X + 157f, UIPosition.Y + 131), Color.White);
                        }
                        break;
                }
            }
        }
    }
}