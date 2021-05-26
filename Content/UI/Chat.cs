using TerraStory.Content.UI.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Terraria.ID;
using System;
using TerraStory.Content.SFX;
using TerraStory.Enums;

namespace TerraStory.Content.UI
{

	public class Chat : BaseUI
	{
		public static SpriteBatch spriteBatch;
		public static float _uiScaleWanted = 1f;
		public static SpriteViewMatrix GameViewMatrix;
		public static SpriteViewMatrix BackgroundViewMatrix;
		public static Matrix _currentWantedZoomMatrix;
		public static float _uiScaleUsed = 1f;
		public static Matrix _uiScaleMatrix;
		public static Main instance;
		public static Chat InGameUI = new Chat();
		public int textBlinkerCount;
		public int textBlinkerState;
		public int AmountOfLines { get; set; }
		public string[] TextLines { get; set; }
		public string _originalText { get; set; }
		public int _lastScreenWidth { get; set; }
		public int _lastScreenHeight { get; set; }
		public static float ChatMenuBeginPos = 0f;
		public static int npcChatCornerItem = 0;
		public static bool mouseText;

		public void PrepareCache(string text)
		{
			if (((uint)(0 | ((Main.screenWidth != _lastScreenWidth) ? 1 : 0) | ((Main.screenHeight != _lastScreenHeight) ? 1 : 0)) | ((_originalText != text) ? 1u : 0u)) != 0)
			{
				_lastScreenWidth = Main.screenWidth;
				_lastScreenHeight = Main.screenHeight;
				_originalText = text;
				TextLines = Utils.WordwrapString(Main.npcChatText, GFX.GFX.MaplestoryBold, 460, 10, out int lineAmount);
				AmountOfLines = lineAmount;
			}
		}

		public static void DrawNPCChatBubble(int i)
		{
			int num = -(Main.npc[i].width / 2 + 8);
			float num2 = Main.npc[i].position.Y - (float)GFX.GFX.Chat.Height - (float)(int)Main.screenPosition.Y;
			if (Main.npc[i].type == 637 && Main.npc[i].ai[0] == 5f)
			{
				num2 -= 18f;
			}
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Main.npc[i].spriteDirection == -1)
			{
				spriteEffects = SpriteEffects.FlipHorizontally;
				num = Main.npc[i].width / 2 + 8;
			}
			if (Main.player[Main.myPlayer].gravDir != 1f)
			{
				spriteEffects |= SpriteEffects.FlipVertically;
				num2 = (float)Main.screenHeight - num2 - (float)GFX.GFX.Chat.Height;
			}
			Vector2 position = new Vector2(Main.npc[i].position.X + (float)(Main.npc[i].width / 2) - Main.screenPosition.X - (float)(GFX.GFX.Chat.Width / 2) - (float)num, num2);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, GameViewMatrix.ZoomMatrix);
			PlayerInput.SetZoom_UI();
			spriteBatch.Draw(GFX.GFX.Chat, position, new Rectangle(0, 0, GFX.GFX.Chat.Width, GFX.GFX.Chat.Height), new Microsoft.Xna.Framework.Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 1f, spriteEffects, 0f);
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, _uiScaleMatrix);
		}
		public static void DrawSignTileBubble()
		{

			if (Main.signBubble)
			{
				int num = (int)((float)Main.signX - Main.screenPosition.X);
				int num2 = (int)((float)Main.signY - Main.screenPosition.Y);
				if (Main.player[Main.myPlayer].gravDir == -1f)
				{
					num2 = Main.screenHeight - (int)((float)Main.signY - Main.screenPosition.Y) - 32;
				}
				SpriteEffects effects = SpriteEffects.None;
				if ((float)Main.signX > Main.player[Main.myPlayer].position.X + (float)Main.player[Main.myPlayer].width)
				{
					effects = SpriteEffects.FlipHorizontally;
					num += -8 - GFX.GFX.Chat2.Width;
				}
				else
				{
					num += 8;
				}
				num2 -= 22;
				spriteBatch.Draw(GFX.GFX.Chat2, new Vector2(num, num2), new Rectangle(0, 0, GFX.GFX.Chat2.Width, GFX.GFX.Chat2.Height), new Microsoft.Xna.Framework.Color(Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor, Main.mouseTextColor), 0f, default(Vector2), 1f, effects, 0f);
				Main.signBubble = false;
			}
		}
		protected void GUIChatDrawInner()
		{
			if (Main.player[Main.myPlayer].talkNPC < 0 && Main.player[Main.myPlayer].sign == -1)
			{
				Main.npcChatText = "";
				return;
			}
			if (Main.netMode == NetmodeID.SinglePlayer && Main.autoPause && Main.player[Main.myPlayer].talkNPC >= 0)
			{
				{
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.BoundGoblin)
					{
						Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(107);
					}
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.BoundWizard)
					{
						Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(108);
					}
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.BoundMechanic)
					{
						Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(124);
					}
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.WebbedStylist)
					{
						Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(353);
					}
					if (Main.npc[Main.player[Main.myPlayer].talkNPC].type == NPCID.SleepingAngler)
					{
						Main.npc[Main.player[Main.myPlayer].talkNPC].Transform(369);
					}

				}
                Color color = new Color(200, 200, 200, 200);
				int num = (Main.mouseTextColor * 2 + 255) / 3;
                Color textColor = new Color(num, num, num, num);
				int num12;
				string[] array = Utils.WordwrapString(Main.npcChatText, Main.fontMouseText, 460, 10, out num12);
				if (Main.editSign)
				{
					textBlinkerCount++;
					if (textBlinkerCount >= 20)
					{
						if (textBlinkerState == 0)
						{
							textBlinkerState = 1;
						}
						else
						{
							textBlinkerState = 0;
						}
						textBlinkerCount = 0;
					}
					if (textBlinkerState == 1)
					{
						string[] array2;
						IntPtr intPtr;
						(array2 = array)[(int)(intPtr = (IntPtr)num12)] = array2[(int)intPtr] + "|";
					}
				}
				num12++;
				spriteBatch.Draw(Main.chatBackTexture, new Vector2(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2, 100f), new Rectangle(0, 0, Main.chatBackTexture.Width, (num12 + 1) * 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				spriteBatch.Draw(Main.chatBackTexture, new Vector2(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2, 100 + (num12 + 1) * 30), new Rectangle(0, Main.chatBackTexture.Height - 30, Main.chatBackTexture.Width, 30), color, 0f, default(Vector2), 1f, SpriteEffects.None, 0f);
				for (int i = 0; i < num12; i++)
				{
					Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, array[i], 170 + (Main.screenWidth - 800) / 2, 120 + i * 30, textColor, Color.Black, Vector2.Zero);
				}
				Rectangle rectangle = new Rectangle(Main.screenWidth / 2 - Main.chatBackTexture.Width / 2, 100, Main.chatBackTexture.Width, (num12 + 2) * 30);
				ChatMenuBeginPos = (float)((num12 + 2) * 30) + 100f;
				if (npcChatCornerItem != 0)
				{
					Vector2 vector = new Vector2(Main.screenWidth / 2 + Main.chatBackTexture.Width / 2, 100 + (num12 + 1) * 30 + 30);
					vector -= Vector2.One * 8f;
					Item item = new Item();
					item.netDefaults(npcChatCornerItem);
					float num23 = 1f;
					Texture2D texture2D = Main.itemTexture[item.type];
					if (texture2D.Width > 32 || texture2D.Height > 32)
					{
						num23 = ((texture2D.Width <= texture2D.Height) ? (32f / (float)texture2D.Height) : (32f / (float)texture2D.Width));
					}
					spriteBatch.Draw(texture2D, vector, null, item.GetAlpha(Color.White), 0f, new Vector2(texture2D.Width, texture2D.Height), num23, SpriteEffects.None, 0f);
					if (item.color != default(Color))
					{
						spriteBatch.Draw(texture2D, vector, null, item.GetColor(item.color), 0f, new Vector2(texture2D.Width, texture2D.Height), num23, SpriteEffects.None, 0f);
					}
					if (new Rectangle((int)vector.X - (int)((float)texture2D.Width * num23), (int)vector.Y - (int)((float)texture2D.Height * num23), (int)((float)texture2D.Width * num23), (int)((float)texture2D.Height * num23)).Contains(new Point(Main.mouseX, Main.mouseY)))
					{
						Main.instance.MouseText(item.Name, -11, 0);
					}
				}
				int num10 = 180 + (Main.screenWidth - 800) / 2;
				int num11 = 130 + num12 * 30;
				float scale = 0.9f;
				string text = "";
				if (Main.mouseX > num10 && (float)Main.mouseX < (float)num10 + Main.fontMouseText.MeasureString(text).X && Main.mouseY > num11 && (float)Main.mouseY < (float)num11 + Main.fontMouseText.MeasureString(text).Y)
				{
					Main.player[Main.myPlayer].mouseInterface = true;
					scale = 1.1f;
					if (!Main.npcChatFocus2)
					{
						SoundManager.PlaySound(Sounds.MenuTick);
					}
					Main.npcChatFocus2 = true;
					Main.player[Main.myPlayer].releaseUseItem = false;
				}
				else
				{
					if (Main.npcChatFocus2)
					{
						SoundManager.PlaySound(Sounds.MenuTick);
					}
					Main.npcChatFocus2 = false;
				}
				Vector2 origin = Main.fontMouseText.MeasureString(text) * 0.5f;
				Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text, (float)num10 + origin.X, (float)num11 + origin.Y, textColor, Color.Black, origin, scale);
				string text5 = "Close";
				textColor = new Color(num, (int)((double)num / 1.1), num / 2, num);
				num10 = num10 + (int)Main.fontMouseText.MeasureString(text).X + 20;
				int num13 = num10 + (int)Main.fontMouseText.MeasureString(text5).X;
				num11 = 130 + num12 * 30;
				scale = 0.9f;
				if (Main.mouseX > num10 && (float)Main.mouseX < (float)num10 + Main.fontMouseText.MeasureString(text5).X && Main.mouseY > num11 && (float)Main.mouseY < (float)num11 + Main.fontMouseText.MeasureString(text5).Y)
				{
					scale = 1.1f;
					if (!Main.npcChatFocus1)
					{
						SoundManager.PlaySound(Sounds.MenuTick);
					}
					Main.npcChatFocus1 = true;
					Main.player[Main.myPlayer].releaseUseItem = false;
					Main.player[Main.myPlayer].controlUseItem = false;
				}
				else
				{
					if (Main.npcChatFocus1)
					{
						SoundManager.PlaySound(Sounds.MenuTick);
					}
					Main.npcChatFocus1 = false;
				}
				for (int i = 0; i < Main.player[Main.myPlayer].name.Length; i++)
				{
					string text2 = Main.player[Main.myPlayer].name.Substring(i, 1);
					origin = Main.fontMouseText.MeasureString(text5) * 0.5f;
					Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text5, (float)num10 + origin.X, (float)num11 + origin.Y, textColor, Color.Black, origin, scale);
					if (text2 != "")
					{
						num10 = num13 + (int)Main.fontMouseText.MeasureString(text2).X / 3;
						num11 = 130 + num12 * 30;
						scale = 0.9f;
						if (Main.mouseX > num10 && (float)Main.mouseX < (float)num10 + Main.fontMouseText.MeasureString(text2).X && Main.mouseY > num11 && (float)Main.mouseY < (float)num11 + Main.fontMouseText.MeasureString(text2).Y)
						{
							Main.player[Main.myPlayer].mouseInterface = true;
							scale = 1.1f;
							if (!Main.npcChatFocus3)
							{
								SoundManager.PlaySound(Sounds.MenuTick);
							}
							Main.npcChatFocus3 = true;
							Main.player[Main.myPlayer].releaseUseItem = false;
						}
						else
						{
							if (Main.npcChatFocus3)
							{
								SoundManager.PlaySound(Sounds.MenuTick);
							}
							Main.npcChatFocus3 = false;
						}
						origin = Main.fontMouseText.MeasureString(text2) * 0.5f;
						Utils.DrawBorderStringFourWay(spriteBatch, Main.fontMouseText, text2, (float)num10 + origin.X, (float)num11 + origin.Y, textColor, Color.Black, origin, scale);
					}
					if (rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
					{
						Main.player[Main.myPlayer].mouseInterface = true;
					}
					if (!Main.mouseLeft || !Main.mouseLeftRelease || !rectangle.Contains(new Point(Main.mouseX, Main.mouseY)))
					{
						return;
					}
					Main.mouseLeftRelease = false;
					Main.player[Main.myPlayer].releaseUseItem = false;
					Main.player[Main.myPlayer].mouseInterface = true;
					if (Main.npcChatFocus1)
					{
						Main.player[Main.myPlayer].talkNPC = -1;
						Main.player[Main.myPlayer].sign = -1;
						npcChatCornerItem = 0;
						Main.editSign = false;
						Main.npcChatText = "";
						SoundManager.PlaySound(Sounds.MenuClose);
					}
					else if (Main.npcChatFocus2)
					{
						if (Main.player[Main.myPlayer].sign != -1)
						{
							if (!Main.editSign)
							{
								SoundManager.PlaySound(Sounds.MenuTick);
								Main.editSign = true;
								Main.clrInput();
								return;
							}
							SoundManager.PlaySound(Sounds.MenuTick);
							int num14 = Main.player[Main.myPlayer].sign;
							Sign.TextSign(num14, Main.npcChatText);
							Main.editSign = false;
							if (Main.netMode == NetmodeID.MultiplayerClient)
							{
								NetMessage.SendData(MessageID.ReadSign, -1, -1);
							}
						}
					}
				}
			}
		}
	}
}