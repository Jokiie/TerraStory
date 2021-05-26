using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;
using TerraStory.Items.Accessories;

namespace TerraStory.Buffs
{
	public class BoneQuiverBuff : ModBuff
	{
		public Item item;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Bone quiver buff");
			Description.SetDefault("Not moving increase your\n" +
				"ranged damage and critical chance rate by 8%");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.rangedDamage += 0.8f;
			player.rangedCrit += 8;
		}
	}
}