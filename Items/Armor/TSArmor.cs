using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using TerraStory.Buffs;
using TerraStory.Items.Weapons.Cannoneer;
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Armor
{
    public class TSArmor : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        // public TerraStory mod { get; set; }

        public int StatsChanges { get; internal set; }

        public override void SetDefaults(Item item)
        {
            /*
            item.width = 18;
            item.height = 18;
            item.value = 10000;
            item.rare = ItemRarityID.Blue;
            item.defense = 5;
            */
        }
        public override string IsArmorSet(Item head, Item body, Item legs)
        {
            if (head.type == ItemID.GoldHelmet && body.type == ItemID.GoldChainmail && legs.type == ItemID.GoldGreaves)
            {
                return "MyMod:GoldArmor";
            }
            if (head.type == ItemID.AncientGoldHelmet && body.type == ItemID.GoldChainmail && legs.type == ItemID.GoldGreaves)
            {
                return "MyMod:GoldArmor";
            }
            if (head.type == ItemID.PlatinumHelmet && body.type == ItemID.PlatinumChainmail && legs.type == ItemID.PlatinumGreaves)
            {
                return "MyMod:PlatinumArmor";
            }
            if (head.type == ItemID.AncientIronHelmet && body.type == ItemID.IronChainmail && legs.type == ItemID.IronGreaves)
            {
                return "MyMod:IronArmor";
            }
            if (head.type == ItemID.IronHelmet && body.type == ItemID.IronChainmail && legs.type == ItemID.IronGreaves)
            {
                return "MyMod:IronArmor";
            }
            if (head.type == ItemID.LeadHelmet && body.type == ItemID.LeadChainmail && legs.type == ItemID.LeadGreaves)
            {
                return "MyMod:IronArmor";
            }
            if (head.type == ItemID.SilverHelmet && body.type == ItemID.SilverChainmail && legs.type == ItemID.SilverGreaves)
            {
                return "MyMod:SilverArmor";
            }
            if (head.type == ItemID.TungstenHelmet && body.type == ItemID.TungstenChainmail && legs.type == ItemID.TungstenGreaves)
            {
                return "MyMod:SilverArmor";
            }
            if (head.type == ItemID.CopperHelmet && body.type == ItemID.CopperChainmail && legs.type == ItemID.CopperGreaves)
            {
                return "MyMod:CopperArmor";
            }
            if (head.type == ItemID.TinHelmet && body.type == ItemID.TinChainmail && legs.type == ItemID.TinGreaves)
            {
                return "MyMod:CopperArmor";
            }
            if (head.type == ItemID.MoltenHelmet && body.type == ItemID.MoltenBreastplate && legs.type == ItemID.MoltenGreaves)
            {
                return "MyMod:MoltenArmor";
            }
            if (head.type == ItemID.RainHat && body.type == ItemID.RainCoat)
            {
                return "MyMod:RainArmor";
            }
            if (head.type == ItemID.EskimoHood && body.type == ItemID.EskimoCoat && legs.type == ItemID.EskimoPants)
            {
                return "MyMod:EskimoArmor";
            }
            if (head.type == ItemID.PinkEskimoHood && body.type == ItemID.PinkEskimoCoat && legs.type == ItemID.PinkEskimoPants)
            {
                return "MyMod:EskimoArmor";
            }
            if (head.type == ItemID.PumpkinHelmet && body.type == ItemID.PumpkinBreastplate && legs.type == ItemID.PumpkinPants)
            {
                return "MyMod:PumpkinArmor";
            }
            if (head.type == ItemID.CactusHelmet && body.type == ItemID.CactusBreastplate && legs.type == ItemID.CactusLeggings)
            {
                return "MyMod:CactusArmor";
            }
            if (head.type == ItemID.ObsidianHelm && body.type == ItemID.ObsidianShirt && legs.type == ItemID.ObsidianPants)
            {
                return "MyMod:ObsidianArmor";
            }
            if (head.type == ItemID.JungleHat && body.type == ItemID.JungleShirt && legs.type == ItemID.JunglePants)
            {
                return "MyMod:JungleArmor";
            }
            if (head.type == ItemID.AncientCobaltHelmet && body.type == ItemID.AncientCobaltBreastplate && legs.type == ItemID.AncientCobaltLeggings)
            {
                return "MyMod:JungleArmor";
            }
            if (head.type == ItemID.FossilHelm && body.type == ItemID.FossilShirt && legs.type == ItemID.FossilPants)
            {
                return "MyMod:FossilArmor";
            }
            if (head.type == ItemID.NinjaHood && body.type == ItemID.NinjaShirt && legs.type == ItemID.NinjaPants)
            {
                return "MyMod:NinjaArmor";
            }
            if (head.type == ItemID.CrimsonHelmet && body.type == ItemID.CrimsonScalemail && legs.type == ItemID.CrimsonGreaves)
            {
                return "MyMod:CrimsonArmor";
            }
            if (head.type == ItemID.ShadowHelmet && body.type == ItemID.ShadowScalemail && legs.type == ItemID.ShadowGreaves)
            {
                return "MyMod:ShadowArmor";
            }
            if (head.type == ItemID.BeeHeadgear && body.type == ItemID.BeeBreastplate && legs.type == ItemID.BeeGreaves)
            {
                return "MyMod:BeeArmor";
            }
            return base.IsArmorSet(head, body, legs);
        }

        public override void UpdateArmorSet(Player player, string set)
        {
            if (set == "MyMod:GoldArmor")
            {
                player.GetModPlayer<TerraStoryPlayer>().goldPickaxe = true;
                player.setBonus = "20% increased Gold pickaxe speed";
            }
            if (set == "MyMod:PlatinumArmor")
            {
                player.GetModPlayer<TerraStoryPlayer>().platinumPickaxe = true;
                player.setBonus = "25% increased Platinum pickaxe speed";
            }
            if (set == "MyMod:SilverArmor")
            {
                player.GetModPlayer<TerraStoryPlayer>().silverPickaxe = true;
                player.setBonus = "15% increased Silver or Tungsten pickaxe speed";
            }
            if (set == "MyMod:IronArmor")
            {
                player.GetModPlayer<TerraStoryPlayer>().ironPickaxe = true;
                player.setBonus = "10% increased Iron and Lead pickaxe speed";
            }
            if (set == "MyMod:CopperArmor")
            {
                player.GetModPlayer<TerraStoryPlayer>().copperPickaxe = true;
                player.setBonus = "5% increased Copper or Tin pickaxe speed";
            }
            if (set == "MyMod:MoltenArmor")
            {
                player.meleeDamage -= 0.17f;
                player.meleeSpeed += 0.12f;
                player.GetModPlayer<TerraStoryPlayer>().moltenPickaxe = true;
                player.setBonus = "12% increased melee speed\n" +
                    "+ 40% molten pickaxe speed";
            }
            if (set == "MyMod:RainArmor")
            {
                player.fishingSkill += 5;
                player.setBonus = "5% increased fishing skill";
            }
            if (set == "MyMod:EskimoArmor")
            {
                if (player.ZoneSnow)
                {
                    player.allDamage += 0.10f;
                    player.AddBuff(124, 2, true); // warmth
                    player.setBonus = "Reduces damage from cold sources \n" +
                        "and 10% increased damage\n" +
                        "in snow biome";
                }
            }
            if (set == "MyMod:PumpkinArmor")
            {
                player.allDamage -= 0.10f;
                player.AddBuff(207, 2, true); // well fed
                player.setBonus = "Add the pumpkin pie buff";
            }
            if (set == "MyMod:CactusArmor")
            {
                player.AddBuff(14 , 2,true); // thorns
                player.setBonus = "1 defense \n" +
                    "Attackers also take damage";
            }
            if (set == "MyMod:ObsidianArmor")
            {
                player.lavaMax += 900;
                player.pickSpeed += 0.15f;
                player.setBonus = "Grants immunity to lava for 15 seconds\n" +
                    "Stacks with lava charm.\n" +
                    "15% increased pick axe speed.";
            }
            if (set == "MyMod:JungleArmor")
            {
                player.manaCost += 0.16f;
                player.rangedCrit += 10;
                player.magicCrit += 10;
                player.meleeCrit += 10;
                player.thrownCrit += 10;
                CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
                modPlayer.cannonCrit += 10;
                player.GetModPlayer<TerraStoryPlayer>().ZoneJungle = true;
                player.setBonus = "10% increased critical strike chance\n" +
                    "You can see more clearly in" +
                    "the dark when in the jungle.";
            }
            if (set == "MyMod:FossilArmor")
            {
                player.thrownCost50 = false;
                player.allDamage += 0.04f;
                player.rangedCrit += 5;
                player.magicCrit += 5;
                player.meleeCrit += 5;
                player.thrownCrit += 5;
                CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
                modPlayer.cannonCrit += 5;
                player.maxMinions++;
                player.setBonus = "4% increased damage \n" +
                    "5% increased critical strike chance\n" +
                    "+1 max minion slot";
            }
            if (set == "MyMod:NinjaArmor")
            {
                player.thrownVelocity += 0.05f;
                player.setBonus = "33% chance to not consumme thrown items\n" +
                "5% increased throwing velocity";
            }
            if (set == "MyMod:CrimsonArmor")
            {
                player.meleeSpeed += 0.07f;
                player.setBonus = "Greatly increase life regeneration\n" +
                "7% increased melee speed";
            }
            if (set == "MyMod:ShadowArmor")
            {
                player.meleeSpeed += 0.07f;
                player.setBonus = "increase movement speed by 15%\n" +
                "7% increased melee speed";
            }
            if (set == "MyMod:BeeArmor")
            {
                player.minionDamage -= 0.10f;
                player.slotsMinions += 2;
                player.setBonus = "increase your max minion slot by 2";
            }
        }
        public override void UpdateEquip(Item item, Player player)
        {
            switch (item.type)
            {
                case ItemID.EskimoPants:
                    player.statDefense -= 1;
                    if (player.ZoneSnow)
                    {
                        player.statDefense += 1;
                    }
                    return;
                case ItemID.EskimoCoat:
                    player.statDefense -= 2;
                    if (player.ZoneSnow)
                    {
                        player.statDefense += 2;
                    }
                    return;
                case ItemID.EskimoHood:
                    player.statDefense -= 1;
                    if (player.ZoneSnow)
                    {
                        player.statDefense += 1;
                    }
                    return;
                case ItemID.RainHat:
                    player.fishingSkill += 2;
                    return;
                case ItemID.RainCoat:
                    player.fishingSkill += 3;
                    return;
                case ItemID.BeeGreaves:
                    //Vanilla = 5% minion damage
                    player.magicDamage += 0.05f;
                    player.thrownDamage += 0.05f;
                    player.meleeDamage += 0.05f;
                    player.rangedDamage += 0.05f;
                    return;
                case ItemID.BeeBreastplate:
                    //Vanilla = 4% minion damage , +1 minion slot
                    player.magicDamage += 0.04f;
                    player.thrownDamage += 0.04f;
                    player.meleeDamage += 0.04f;
                    player.rangedDamage += 0.04f;
                    player.maxMinions -= 1;
                    return;
                case ItemID.NecroGreaves:
                    player.magicDamage += 0.05f;
                    player.thrownDamage += 0.05f;
                    player.meleeDamage += 0.05f;
                    player.minionDamage += 0.05f;
                    return;
                case ItemID.NecroBreastplate:
                    player.magicDamage += 0.05f;
                    player.thrownDamage += 0.05f;
                    player.meleeDamage += 0.05f;
                    player.minionDamage += 0.05f;
                    return;
                case ItemID.NinjaHood:
                    player.thrownDamage += 0.03f;
                    return;
                case ItemID.NinjaShirt:
                    player.magicDamage += 0.02f;
                    player.thrownDamage -= 0.13f;
                    player.meleeDamage += 0.02f;
                    player.minionDamage += 0.02f;
                    player.rangedDamage += 0.02f;
                    player.magicCrit += 3;
                    player.meleeCrit += 3;
                    player.rangedCrit += 3;
                    player.thrownCrit += 3;
                    return;
                case ItemID.NinjaPants:
                    player.magicDamage += 0.02f;
                    player.thrownDamage += 0.02f;
                    player.meleeDamage += 0.02f;
                    player.minionDamage += 0.02f;
                    player.rangedDamage += 0.02f;
                    player.magicCrit += 2;
                    player.meleeCrit += 2;
                    player.rangedCrit += 2;
                    player.thrownCrit -= 8;
                    return;
                case ItemID.CrimsonScalemail:
                    player.AddBuff(13, 2, true); // Battle
                    player.allDamage -= 0.02f;
                    return;
                case ItemID.CrimsonGreaves:
                    player.allDamage += 0.03f;
                    return;
                case ItemID.CrimsonHelmet:
                    player.meleeDamage += 0.04f;
                    player.allDamage -= 0.02f;
                    return;
                case ItemID.ShadowScalemail:
                case ItemID.AncientShadowScalemail:
                    player.AddBuff(106, 2, true); // Calm
                    player.meleeSpeed -= 0.07f;
                    return;
                case ItemID.ShadowGreaves:
                case ItemID.AncientShadowGreaves:
                    player.allDamage += 0.05f;
                    player.meleeSpeed -= 0.07f;
                    return;
                case ItemID.ShadowHelmet:
                case ItemID.AncientShadowHelmet:
                    player.meleeDamage += 0.04f;
                    return;
                case ItemID.SilverHelmet:
                case ItemID.TungstenHelmet:
                    player.meleeDamage += 0.03f;
                    return;
                case ItemID.CopperHelmet:
                case ItemID.TinHelmet:
                    player.meleeCrit += 1;
                    return;
                case ItemID.PlatinumHelmet:
                    player.rangedCrit += 5;
                    player.meleeCrit += 5;
                    player.magicCrit += 5;
                    player.thrownCrit += 5;
                    return;
                case ItemID.GoldHelmet:
                case ItemID.AncientGoldHelmet:
                    player.rangedCrit += 5;
                    return;
                case ItemID.IronHelmet:
                case ItemID.LeadHelmet:
                    player.meleeCrit += 2;
                    return;
                case ItemID.JungleHat:
                case ItemID.AncientCobaltHelmet:
                    player.statManaMax2 -= 40;
                    return;
                case ItemID.JungleShirt:
                case ItemID.AncientCobaltBreastplate:
                    player.meleeCrit += 4;
                    player.rangedCrit += 4;
                    player.thrownCrit += 4;
                    CannoneerPlayer modPlayer = CannoneerPlayer.ModPlayer(player);
                    modPlayer.cannonCrit += 4;
                    player.statManaMax2 -= 20;
                    return;
                case ItemID.JunglePants:
                case ItemID.AncientCobaltLeggings:
                    player.meleeCrit += 4;
                    player.rangedCrit += 4;
                    player.thrownCrit += 4;
                    CannoneerPlayer modPlayer2 = CannoneerPlayer.ModPlayer(player);
                    modPlayer2.cannonCrit += 4;
                    player.statManaMax2 -= 20;
                    return;
                case ItemID.MeteorHelmet:
                    player.magicDamage -= 0.03f;
                    return;
                case ItemID.MeteorSuit:
                    player.rangedDamage += 0.04f;
                    player.meleeDamage += 0.04f;
                    player.thrownDamage += 0.04f;
                    player.minionDamage += 0.04f;
                    player.magicDamage -= 0.03f;
                    return;
                case ItemID.MeteorLeggings:
                    player.rangedDamage += 0.04f;
                    player.meleeDamage += 0.04f;
                    player.thrownDamage += 0.04f;
                    player.minionDamage += 0.04f;
                    player.magicDamage -= 0.03f;
                    return;
                case ItemID.MoltenBreastplate:
                case ItemID.MoltenGreaves:
                    player.allDamage += 0.05f;
                    return;
                case ItemID.MoltenHelmet:
                    player.meleeDamage += 0.07f;
                    return;
                case ItemID.ObsidianHelm:
                    player.allDamage += 0.04f;
                    player.rangedCrit += 3;
                    player.thrownCrit += 3;
                    player.magicCrit += 3;
                    player.meleeCrit += 3;
                    CannoneerPlayer modPlayer3 = CannoneerPlayer.ModPlayer(player);
                    modPlayer3.cannonCrit += 3;
                    player.maxMinions += 1;
                    return;
                case ItemID.ObsidianShirt:
                case ItemID.ObsidianPants:
                    player.allDamage += 0.03f;
                    player.rangedCrit += 3;
                    player.thrownCrit += 3;
                    player.magicCrit += 3;
                    player.meleeCrit += 3;
                    CannoneerPlayer modPlayer4 = CannoneerPlayer.ModPlayer(player);
                    modPlayer4.cannonCrit += 3;
                    return;
                case ItemID.FossilHelm:
                    player.thrownVelocity -= 0.20f;
                    player.allDamage += 0.02f;
                    return;
                case ItemID.FossilShirt:
                    player.thrownDamage -= 0.20f;
                    player.allDamage += 0.02f;
                    return;
                case ItemID.FossilPants:
                    player.allDamage += 0.02f;
                    player.thrownCrit -= 10;
                    player.rangedCrit += 5;
                    player.meleeCrit += 5;
                    player.magicCrit += 5;
                    CannoneerPlayer modPlayer5 = CannoneerPlayer.ModPlayer(player);
                    modPlayer5.cannonCrit += 5;
                    return;
            }
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.type == ItemID.BeeGreaves)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "5% increased damage";
                    }
                }
            }
            if (item.type == ItemID.BeeBreastplate)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased damage";
                    }
                }
            }
            if (item.type == ItemID.BeeBreastplate)
            {
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip1")
                    {
                        line2.text = "";
                    }

                }
            }
            if (item.type == ItemID.BeeHeadgear)
            {
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip1")
                    {
                        line2.text = "Increase your max number of minion by 2.";
                    }
                }
            }
            if (item.type == ItemID.NecroBreastplate)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "5% increased damage";
                    }
                }
            }
            if (item.type == ItemID.NecroGreaves)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "5% increased damage";
                    }
                }
            }
            if (item.type == ItemID.NinjaHood)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "3% increased throwing damage \n" +
                            "15% increased throwing velocity";
                    }
                }
            }
            if (item.type == ItemID.NinjaShirt)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "2% increased damage \n" +
                            "3% increased critical strike chance";
                    }
                }
            }
            if (item.type == ItemID.NinjaPants)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "2% increased damage \n" +
                            "2% increased critical strike chance";
                    }
                }
            }
            if (item.type == ItemID.CrimsonScalemail)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "Add the Battle potion buff.";
                    }
                }
            }
            if (item.type == ItemID.CrimsonGreaves)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "5% increased damage";
                    }
                }
            }
            if (item.type == ItemID.CrimsonHelmet)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased melee damage";
                    }
                }
            }
            if (item.type == ItemID.ShadowGreaves || item.type == ItemID.AncientShadowGreaves)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "5% increased damage";
                    }
                }
            }
            if (item.type == ItemID.ShadowScalemail || item.type == ItemID.AncientShadowScalemail)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "Add the calm potion buff";
                    }
                }
            }
            if (item.type == ItemID.ShadowHelmet || item.type == ItemID.AncientShadowHelmet)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased melee damage";
                    }
                }
            }
            if (item.type == ItemID.PlatinumHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "5% increased crit chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.GoldHelmet || item.type == ItemID.AncientGoldHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "5% increased ranged crit chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.SilverHelmet || item.type == ItemID.TungstenHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "3% increased melee crit chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.IronHelmet || item.type == ItemID.LeadHelmet || item.type == ItemID.AncientIronHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "2% increased melee crit chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.CopperHelmet || item.type == ItemID.TinHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "1% increased melee crit chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.RainCoat)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "3% increased fishing skill")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.RainHat)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "2% increased fishing skill")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.EskimoHood || item.type == ItemID.PinkEskimoHood)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Defense")
                    {
                        line1.text = "+1 defense \n" +
                            "in snow biome";
                    }
                }
            }
            if (item.type == ItemID.EskimoCoat || item.type == ItemID.PinkEskimoCoat)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Defense")
                    {
                        line1.text = "+2 defense \n" +
                            "in snow biome";
                    }
                }
            }
            if (item.type == ItemID.EskimoPants || item.type == ItemID.PinkEskimoPants)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Defense")
                    {
                        line1.text = "+1 defense \n" +
                            "in snow biome";
                    }
                }
            }
            if (item.type == ItemID.AncientCobaltHelmet || item.type == ItemID.JungleHat)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased magic \n" +
                            " critical strike chance";
                    }
                }
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip1")
                    {
                        line2.text = "";
                    }
                }
            }
            if (item.type == ItemID.AncientCobaltLeggings || item.type == ItemID.JunglePants)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased critical strike chance";
                    }
                }
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip1")
                    {
                        line2.text = "";
                    }
                }
            }
            if (item.type == ItemID.AncientCobaltBreastplate || item.type == ItemID.JungleShirt)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased critical strike chance";
                    }
                }
                foreach (TooltipLine line2 in tooltips)
                {
                    if (line2.mod == "Terraria" && line2.Name == "Tooltip1")
                    {
                        line2.text = "";
                    }
                }
            }
            if (item.type == ItemID.MeteorHelmet)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased magic damage";
                    }
                }
            }
            if (item.type == ItemID.MeteorSuit || item.type == ItemID.MeteorLeggings)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased damage";
                    }
                }
            }
            if (item.type == ItemID.MoltenGreaves || item.type == ItemID.MoltenBreastplate)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "5% increased damage")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.MoltenHelmet)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "7% increased melee damage")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.ObsidianHelm)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "4% increased damage \n" +
                    "4% increased critical strike chance \n" +
                    "+1 max minion slot")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.ObsidianShirt || item.type == ItemID.ObsidianPants)
            {
                TooltipLine line = new TooltipLine(mod, "damage", "3% increased damage \n" +
                    "3% increased critical strike chance")
                {
                    overrideColor = Color.White
                };
                tooltips.Add(line);
            }
            if (item.type == ItemID.FossilHelm)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "4% increased damage \n" +
                            "5% increased critical strike chance";
                    }
                }
            }
            if (item.type == ItemID.FossilShirt)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "2% increased damage";
                    }
                }
            }
            if (item.type == ItemID.FossilPants)
            {
                foreach (TooltipLine line1 in tooltips)
                {
                    if (line1.mod == "Terraria" && line1.Name == "Tooltip0")
                    {
                        line1.text = "2% increased damage \n" +
                            "5% increased critical strike chance";
                    }
                }
            }
        }
    }
}
