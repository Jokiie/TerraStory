using TerraStory.Projectiles.Pets;
using TerraStory.Items;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using TerraStory.Projectiles.Armor;

namespace TerraStory.Buffs
{
	public class JungleArmorBuff : ModBuff
	{
		public Item item;
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Enchanted moonglow.");
			Description.SetDefault("Your armor summonned an enchanted \n" +
				"moonglow.");
			Main.buffNoSave[Type] = true;
			Main.buffNoTimeDisplay[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			TerraStoryPlayer modPlayer = player.GetModPlayer<TerraStoryPlayer>();
			if (player.ownedProjectileCounts[ModContent.ProjectileType<Moonglow>()] > 0)
			{
				modPlayer.moonGlow = true;
			}
			player.buffTime[buffIndex] = 60;
		}
	}
}