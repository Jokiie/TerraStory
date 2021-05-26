using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;
using TerraStory.Items.Accessories;

namespace TerraStory.Buffs
{
	public class JumpShurikenBuff : ModBuff
	{
		public Item item;

		public override void SetDefaults()
		{
			DisplayName.SetDefault("Ninja belt buff");
			Description.SetDefault("Jumping increase your thrown \n" +
				"damage, crit and velocity by 8% for 2 seconds");
			Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.thrownDamage += 0.8f;
			player.thrownVelocity += 0.8f;
			player.thrownCrit += 8;
		}
	}
}