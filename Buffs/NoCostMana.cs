using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;

namespace TerraStory.Buffs
{
	public class NoCostMana : ModBuff
	{
		public Item item;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("No mana cost.");
			Description.SetDefault("Your weapon cost no mana.");
			Main.buffNoTimeDisplay[Type] = true;
			Main.debuff[Type] = false;
			canBeCleared = false;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<TerraStoryPlayer>().NoCostMana = true;
		}
	}
}