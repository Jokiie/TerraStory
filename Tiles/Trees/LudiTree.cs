using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerraStory.Dusts;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Tiles.Trees
{
	public class LudiTree : ModTree
	{
		private Mod mod => ModLoader.GetMod("TerraStory");

		public override int CreateDust() {
			return DustType<Sparkle4>();
		}

		public override int GrowthFXGore() {
			return mod.GetGoreSlot("Gores/LudiTreeFX");
		}

		public override int DropWood() {
			return ItemType<Items.Placeable.LudiTreeBlock>();
		}

		public override Texture2D GetTexture() {
			return mod.GetTexture("Tiles/Trees/LudiTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset) {
			return mod.GetTexture("Tiles/Trees/LudiTree_Tops");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame) {
			return mod.GetTexture("Tiles/Trees/LudiTree_Branches");
		}
	}
} 