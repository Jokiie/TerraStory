using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.UI;

namespace TerraStory.Content.UI.Base
{
    public class BaseUI
    {
        public BaseUI()
        {
            UIElements.Add(this);
        }
        public List<InterfaceButton> Buttons { get; set; } = new List<InterfaceButton>();

        public bool UIActive { get; set; }

        public static List<BaseUI> UIElements { get; set; } = new List<BaseUI>();

        public bool DragableUIPanel { get; set; }

        public virtual bool RemoveOnClose => false;

        public InterfaceButton AddButton(Func<Rectangle> position, Action<Player> pressAction)
        {
            InterfaceButton button = new InterfaceButton(position, pressAction);
            Buttons.Add(button);
            return button;
        }

        public InterfaceButton AddButton(Func<Rectangle> position, Action<Player> pressAction, Action<Player, SpriteBatch> hoverAction)
        {
            InterfaceButton button = new InterfaceButton(position, pressAction, hoverAction);
            Buttons.Add(button);
            return button;
        }

        public void CloseUI()
        {
            OnClose();
            UIActive = false;
            if (RemoveOnClose) UIElements.Remove(this);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Player player)
        {
            PostDraw(spriteBatch, player);

            foreach (InterfaceButton button in Buttons)
                button.Update(spriteBatch, player);
        }

        public virtual void OnClose()
        {
        }

        public virtual void PostDraw(SpriteBatch spriteBatch, Player player)
        {

        }

        public virtual bool PreDraw()
        {
            return UIActive;
        }

        public void RemoveButton(InterfaceButton button)
        {
            Buttons.Remove(button);
        }
		//public virtual void PostDraw(SpriteBatch spriteBatch, Player player) {}
	}
}
