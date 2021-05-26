using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerraStory.NPCs.Bosses;

namespace TerraStory.Tiles.Boss
{
	public class ManoShell : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;

			Main.tileCut[Type] = false;

			Main.tileLavaDeath[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
			TileObjectData.newTile.Height = 4;
			TileObjectData.newTile.Width = 6;
			TileObjectData.newTile.CoordinateHeights = new int[]
			{
			16,
			16,
			16,
			16
			};
			TileObjectData.addTile(Type);
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Mano Shell");
			AddMapEntry(new Color(220, 20, 60), name);
		}
		public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height)
		{
			offsetY = 2;
		}
		public override void KillMultiTile(int i, int j, int frameX, int frameY)
		{
			{
				Main.NewText("Mano has awoken!", 175, 75, 255, true);
				int n = NPC.NewNPC((int)i * 16 - 30, (int)j * 16, ModContent.NPCType<Mano>(), 0, 2, 1, 0, 0, Main.myPlayer);
				Main.npc[n].netUpdate = true;
			}
			Main.PlaySound(SoundLoader.customSoundType, new Vector2((int)i * 16, (int)j * 16), mod.GetSoundSlot(SoundType.Custom, "Sounds/ManoSkill"));
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(4, 1));
		}
	}
}