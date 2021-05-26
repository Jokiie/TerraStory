using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Items.Weapons.Cannoneer
{
	// This class stores necessary player info for our custom damage class, such as damage multipliers, additions to knockback and crit, and our custom resource that governs the usage of the weapons of this damage class.
	public class CannoneerPlayer : ModPlayer
	{
		public static CannoneerPlayer ModPlayer(Player player)
		{
			return player.GetModPlayer<CannoneerPlayer>();
		}

		// Vanilla only really has damage multipliers in code
		// And crit and knockback is usually just added to
		// As a modder, you could make separate variables for multipliers and simple addition bonuses
		public float cannonDamageAdd;
		public float cannonDamageMult = 1f;
		public float cannonKnockback;
		public float cannonVelocity;
		public int cannonCrit;


		public override void Initialize()
		{

		}

		public override void ResetEffects()
		{
			ResetVariables();
		}

		public override void UpdateDead()
		{
			ResetVariables();
		}

		private void ResetVariables()
		{
			cannonDamageAdd = 0f;
			cannonDamageMult = 1f;
			cannonKnockback = 0f;
			cannonVelocity = 0f;
			cannonCrit = 0;
		}
	}
}