using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using TerraStory.Buffs;
using TerraStory.Items.Accessories;
using TerraStory.Items.Weapons.Mage;
using TerraStory.NPCs.TownNPCs;

namespace TerraStory
{
    public class TerraStoryPlayer : ModPlayer
	{
		public Item item;

		public static int jumpHeight = 15;

		public int totalFrames;

		public int standingFrameStart;

		public int standingFrameCount;

		public int standingFrameDelay;

		public int runningFrameStart;

		public int runningFrameCount;

		public int runningFrameDelay;

		public int flyingFrameStart;

		public int flyingFrameCount;

		public int flyingFrameDelay;

		public int inAirFrameStart;

		public int inAirFrameCount;

		public int inAirFrameDelay;

		public int idleFrameStart;

		public int idleFrameCount;

		public int idleFrameDelay;

		public bool idleFrameLoop;

		public const int FrameStanding = 0;

		public const int FrameRunning = 1;

		public bool NoCostMana = false;
		public float manaCost = 1f;
		public bool beeGun = false;
		public bool crimsonstaff = false;
		public bool corruptedstaff = false;
		public bool copperPickaxe = false;
		public bool silverPickaxe = false;
		public bool ironPickaxe = false;
		public bool goldPickaxe = false;
		public bool platinumPickaxe = false;
		public bool moltenPickaxe = false;
		public bool ZoneUnderworld = false;
		public bool ZoneJungle = false;
		public bool jungleArmorBuff = false;
		public bool snailmorph = false;
		public bool Slow = false;
		public int heroLives;
		public bool FrostBurnSummon;
		public bool Kino = false;
		public bool BabyDragon = false;
		public bool FennecFox = false;
		public bool BlackDragon = false;
		public bool GreenDragon = false;
		public bool RedDragon = false;
		public bool Mir = false;
		public bool Snail = false;
		public bool JrReaper = false;
		public bool CorruptedMors = false;
		public bool necroStaff = false;
		public bool CrimsonMetus = false;
		public bool SoldierHong = false;
		public bool MapleGreenSlime = false;
		public bool MinionQuiBouge = false;
		public bool KevinTheCutePig = false;
		public bool OrangeMushroom = false;
		//region Debuffs
		public bool SnailMorphdebuff = false;
		public bool ZombieMushmomJump = false;
		public bool BlueMushmomJump = false;
		public bool MushmomJump = false;
		public bool TooSharp = false;
		public bool PoisonedShuriken = false;
		public bool DeepCut = false;
		public bool moonGlow = false;
		public bool sandGun = false;
		public bool manaCost80;
		public bool manaCost75;
		public bool manaCost50;
		public bool minionSlot50;
		public bool noGravity = false;
		public bool ZoneLudibrium;
		public bool JumpShurikenBuff = false;
		public bool BoneQuiverBuff = false;

		public override void ResetEffects()
		{
			MinionQuiBouge = false;
			FrostBurnSummon = false;
			Kino = false;
			BabyDragon = false;
			FennecFox = false;
			BlackDragon = false;
			GreenDragon = false;
			RedDragon = false;
			Mir = false;
			Snail = false;
			JrReaper = false;
			CorruptedMors = false;
			CrimsonMetus = false;
			SoldierHong = false;
			MapleGreenSlime = false;
			KevinTheCutePig = false;
			OrangeMushroom = false;
			TooSharp = false;
			PoisonedShuriken = false;
			DeepCut = false;
			corruptedstaff = false;
			crimsonstaff = false;
			NoCostMana = false;
			beeGun = false;
			necroStaff = false;
			copperPickaxe = false;
			ironPickaxe = false;
			silverPickaxe = false;
			goldPickaxe = false;
			platinumPickaxe = false;
			moltenPickaxe = false;
			ZoneUnderworld = false;
			ZoneJungle = false;
			jungleArmorBuff = false;
			moonGlow = false;
			sandGun = false;
			manaCost80 = false;
			manaCost75 = false;
			manaCost50 = false;
			minionSlot50 = false;
			snailmorph = false;
			noGravity = false;
			Slow = false;
			SnailMorphdebuff = false;
			ZombieMushmomJump = false;
			BlueMushmomJump = false;
			MushmomJump = false;
			JumpShurikenBuff = false;
			BoneQuiverBuff = false;
		}
		public Projectile projectile;

        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
		{
			if (item.magic || manaCost80)
			{
				if (manaCost80 && Main.rand.Next(5) == 0)
				{
					mult = 0;
				}
			}
			if (item.magic || manaCost75)
			{
				if (manaCost75 && Main.rand.Next(4) == 0)
			    {
		  		    mult = 0;
			    }
			}
			if (item.magic || manaCost50)
			{
				if (manaCost50 && Main.rand.Next(1) == 0)
				{
					mult = 0;
				}
			}
			if (corruptedstaff && item.type == ModContent.ItemType<CorruptedStaff>())
			{
				mult = 0;
			}
			if (crimsonstaff && item.type == ModContent.ItemType<CrimsonStaff>())
			{
				mult = 0;
			}
			if (beeGun && item.type == ItemID.BeeGun)
			{
				mult = 0;
			}
			// This item's mana usage changes through the day, peaking at 1.5x mana usage at noon, and 0.5x mana usage at midnight.
			if (necroStaff && item.type == ModContent.ItemType<NecroStaff>())
			{
				double currentTime = Main.time;
				// The time at which it changes from day to night and vice versa.
				double maxTime = Main.dayTime ? Main.dayLength : Main.nightLength;
				// More mana during day, less at night
				int direction = Main.dayTime ? 1 : -1;
				// Sine goes from 0 to 1 to 0 over a period of pi, so we match that to the length of the day/night.
				float timeMult = (float)Math.Sin(currentTime / maxTime * Math.PI);
				// Then we multiply by direction so it goes between 1 and -1 through the entire day, then multiply by 0.5 and add 1 to make it go between 1.5 and 0.5.
				timeMult = 0.5f + timeMult * direction * 0f;
				// Last, we multiply the current mana cost multiplier of the item by our multiplier.
				mult *= timeMult;
			}
			if (copperPickaxe || item.type == ItemID.CopperPickaxe || item.type == ItemID.TinPickaxe)
			{
				player.pickSpeed += 0.05f;
			}
			if (ironPickaxe || item.type == ItemID.IronPickaxe || item.type == ItemID.LeadPickaxe)
			{
				player.pickSpeed += 0.10f;
			}
			if (silverPickaxe || item.type == ItemID.SilverPickaxe || item.type == ItemID.TungstenPickaxe)
			{
				player.pickSpeed += 0.15f;
			}
			if (goldPickaxe || item.type == ItemID.GoldPickaxe)
			{
				player.pickSpeed += 0.20f;
			}
			if (platinumPickaxe || item.type == ItemID.PlatinumPickaxe)
			{
				player.pickSpeed += 0.25f;
			}
			if (moltenPickaxe || item.type == ItemID.MoltenPickaxe)
			{
				player.pickSpeed += 0.40f;
			}

		}
        public override void PostUpdateBuffs()
        {
			if (ZoneJungle || player.ZoneJungle)
			{
				player.AddBuff(12, 60, true);
			}
			if (snailmorph)
            {
				player.AddBuff(ModContent.BuffType<SnailMorphBuff>(), 2, true);
            }
			if (SnailMorphdebuff)
            {
				player.AddBuff(ModContent.BuffType<SnailMorphDebuff>(), 1, true);
			}
		}
		public override void ProcessTriggers(TriggersSet triggersSet)
		{
			if (JumpShurikenBuff)
			{
				player.AddBuff(ModContent.BuffType<JumpShurikenBuff>(), 121, true);
			}
			if (BoneQuiverBuff)
            {
				player.AddBuff(ModContent.BuffType<BoneQuiverBuff>(), 2, true);
            }
		}

		public override void UpdateBadLifeRegen()
        {
            if (TooSharp)
            {
				player.lifeRegen -= 10;
            }
			if (PoisonedShuriken)
            {
				player.lifeRegen -= 20;
            }
			if (DeepCut)
            {
				player.lifeRegen -= 20;
            }
			if (ZombieMushmomJump)
			{
				player.lifeRegen -= 40;
			}
			if (MushmomJump)
            {
				player.lifeRegen -= 20;
            }
            if (BlueMushmomJump)
            {
				player.lifeRegen -= 60;
            }
		}
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
		{
			if ((proj.minion || ProjectileID.Sets.MinionShot[proj.type]) && FrostBurnSummon && !proj.noEnchantments)
			{
				target.AddBuff(BuffID.Frostburn, 60 * Main.rand.Next(5, 15), false);
			}
		}

        public override void UpdateBiomes()
		{
			ZoneLudibrium = World.LudibriumTiles > 200;
		}

		public override void SendCustomBiomes(BinaryWriter writer)
	    {
		BitsByte flags = new BitsByte();
		flags[0] = ZoneLudibrium;
		writer.Write(flags);
	    }

	    public override void ReceiveCustomBiomes(BinaryReader reader)
	    {
		BitsByte flags = reader.ReadByte();
			ZoneLudibrium = flags[0];
	    }
	    public override Texture2D GetMapBackgroundImage()
		{
			if (ZoneLudibrium)
			{
				return mod.GetTexture("LudiMapBackground");
			}
			return null;
		}
	}
}
