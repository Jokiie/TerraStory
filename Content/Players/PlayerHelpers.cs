﻿using TerraStory.Content.SFX;
using TerraStory.Enums;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Achievements;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerraStory.Content.Players
{
    public static class PlayerHelpers
    {
        public static Item GetItem(this Player player, Item newItem)
        {
            Main.instance.MouseTextHackZoom("");

            int plr = player.whoAmI;

            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();

            bool flag = newItem.type >= ItemID.CopperCoin && newItem.type <= ItemID.PlatinumCoin;

            Item item = newItem;

            int num = 50;

            if (newItem.noGrabDelay > 0)
                return item;

            int num2 = 0;

            if (newItem.uniqueStack && player.HasItem(newItem.type))
                return item;

            if (newItem.type == ItemID.CopperCoin || newItem.type == ItemID.GoldCoin || newItem.type == ItemID.PlatinumCoin || newItem.type == ItemID.SilverCoin)
            {
                num2 = -4;
                num = 54;
            }

            if ((item.ammo > 0 || item.bait > 0) && !item.notAmmo || item.type == ItemID.Wire)
            {
                item = player.FillAmmo(plr, item);
                if (item.type == ItemID.None || item.stack == 0)
                    return new Item();
            }

            for (int i = num2; i < 50; i++)
            {
                int num3 = i;
                if (num3 < 0)
                    num3 = 54 + i;
                Item x = player.inventory[num3];
                if (x.type <= ItemID.None || x.stack <= 0 || x.stack >= x.maxStack || !item.IsTheSameAs(x))
                    continue;
                SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);

                if (item.stack + x.stack <= x.maxStack)
                {
                    x.stack += item.stack;
                    ItemText.NewText(newItem, item.stack);
                    player.DoCoins(num3);
                    if (plr == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return new Item();
                }

                AchievementsHelper.NotifyItemPickup(player, item, x.maxStack - x.stack);
                item.stack -= x.maxStack - x.stack;
                ItemText.NewText(newItem, x.maxStack - x.stack);
                x.stack = x.maxStack;
                player.DoCoins(num3);
                if (plr == Main.myPlayer)
                    Recipe.FindRecipes();
            }


            for (int inventoryPageId = 0; inventoryPageId < character.Inventories.Length; inventoryPageId += 1)
            {

                if (character.ActiveInvPage == inventoryPageId)
                {

                    continue;
                }

                for (int inventorySlotId = 0; inventorySlotId < character.Inventories[inventoryPageId].Length; inventorySlotId += 1)
                {
                    Item x = character.Inventories[inventoryPageId][inventorySlotId];

                    if (x.type <= ItemID.None || x.stack >= x.maxStack || !item.IsTheSameAs(x))
                        continue;

                    SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);

                    if (item.stack + x.stack <= x.maxStack)
                    {
                        TerraStory.LogMessage("Stack size small than max");
                        x.stack += item.stack;
                        ItemText.NewText(newItem, item.stack);
                        AchievementsHelper.NotifyItemPickup(player, item);
                        return new Item();
                    }

                    AchievementsHelper.NotifyItemPickup(player, item, x.maxStack - x.stack);
                    item.stack -= x.maxStack - x.stack;
                    ItemText.NewText(newItem, x.maxStack - x.stack);
                    x.stack = x.maxStack;
                    if (plr == Main.myPlayer)
                        Recipe.FindRecipes();
                }
            }

            if (newItem.type != ItemID.CopperCoin && newItem.type != ItemID.SilverCoin && newItem.type != ItemID.GoldCoin && newItem.type != ItemID.PlatinumCoin && newItem.useStyle > (int)UseStyles.None)
                for (int j = 0; j < 10; j++)
                {
                    if (player.inventory[j].type != ItemID.None)
                        continue;
                    player.inventory[j] = item;
                    ItemText.NewText(newItem, newItem.stack);
                    player.DoCoins(j);
                    SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);

                    if (plr == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return new Item();
                }

            if (TSConfig.ClientSide.SmartInventory)
            {
                if (character.ActiveInvPage == 2)
                    for (int inventorySlotId = 10; inventorySlotId < 50; inventorySlotId += 1)
                    {
                        if (player.inventory[inventorySlotId].type != ItemID.None)
                            continue;
                        player.inventory[inventorySlotId] = item;
                        ItemText.NewText(newItem, newItem.stack);
                        SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);
                        AchievementsHelper.NotifyItemPickup(player, item);
                        return new Item();
                    }
                else
                    for (int inventorySlotId = 0; inventorySlotId < character.Inventories[2].Length; inventorySlotId += 1)
                    {
                        if (character.Inventories[2][inventorySlotId].type != ItemID.None)
                            continue;
                        character.Inventories[2][inventorySlotId] = item;
                        ItemText.NewText(newItem, newItem.stack);
                        SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);
                        AchievementsHelper.NotifyItemPickup(player, item);
                        return new Item();
                    }
            }

            if (newItem.favorited)
            {
                for (int k = 0; k < num; k++)
                {
                    if (player.inventory[k].type != ItemID.None || TSConfig.ClientSide.ManualInventory && character.ActiveInvPage != 0)
                        continue;
                    player.inventory[k] = item;
                    ItemText.NewText(newItem, newItem.stack);
                    player.DoCoins(k);
                    SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);
                    if (plr == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return new Item();
                }
            }
            else
            {
                for (int inventorySlotId = num - 1; inventorySlotId >= 0; inventorySlotId--)
                {
                    if (player.inventory[inventorySlotId].type != ItemID.None || TSConfig.ClientSide.ManualInventory && character.ActiveInvPage != 0 && !flag)
                        continue;
                    player.inventory[inventorySlotId] = item;
                    ItemText.NewText(newItem, newItem.stack);
                    player.DoCoins(inventorySlotId);
                    SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);
                    if (plr == Main.myPlayer)
                        Recipe.FindRecipes();
                    AchievementsHelper.NotifyItemPickup(player, item);
                    return new Item();
                }

                for (int inventoryPageId = 0; inventoryPageId < character.Inventories.Length; inventoryPageId += 1)
                {
                    if (character.ActiveInvPage == inventoryPageId)
                        continue;

                    for (int inventorySlotId = 0; inventorySlotId < character.Inventories[inventoryPageId].Length; inventorySlotId += 1)
                    {
                        if (character.Inventories[inventoryPageId][inventorySlotId].type != ItemID.None)
                            continue;
                        character.Inventories[inventoryPageId][inventorySlotId] = item;
                        ItemText.NewText(newItem, newItem.stack);
                        SoundManager.PlaySound(flag ? Sounds.CoinPickup : Sounds.Grab, player.position);
                        AchievementsHelper.NotifyItemPickup(player, item);
                        return new Item();
                    }
                }
            }

            return item;
        }

        public static void ApiQuickBuff(this Player player)
        {
            if (player.noItems)
                return;
            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();

            if (player.CountBuffs() == 22)
                return;

            for (int i = 0; i < 58; i += 1)
            {
                Item item = player.inventory[i];
                APIQuickBuff_TryItem(player, item);
            }

            for (int i = 0; i < character.Inventories.Length; i += 1)
            {
                if (character.ActiveInvPage == i)
                    continue;
                for (int j = 0; j < character.Inventories[i].Length; j += 1)
                {
                    Item item = character.Inventories[i][j];
                    APIQuickBuff_TryItem(player, item);
                }
            }
        }

        private static void APIQuickBuff_TryItem(this Player player, Item item)
        {
            LegacySoundStyle legacySoundStyle = null;

            if (item.stack > 0 && item.type > ItemID.None && item.buffType > 0 && !item.summon && item.buffType != 90)
            {
                int num = item.buffType;
                bool flag = ItemLoader.CanUseItem(item, player);
                for (int b = 0; b < 22; b++)
                {
                    if (num == 27 && (player.buffType[b] == num || player.buffType[b] == 101 || player.buffType[b] == 102))
                    {
                        flag = false;
                        break;
                    }

                    if (player.buffType[b] == num)
                    {
                        flag = false;
                        break;
                    }

                    if (!Main.meleeBuff[num] || !Main.meleeBuff[player.buffType[b]])
                        continue;
                    flag = false;
                    break;
                }

                if (Main.lightPet[item.buffType] || Main.vanityPet[item.buffType])
                    for (int k = 0; k < 22; k++)
                    {
                        if (Main.lightPet[player.buffType[k]] && Main.lightPet[item.buffType])
                            flag = false;
                        if (Main.vanityPet[player.buffType[k]] && Main.vanityPet[item.buffType])
                            flag = false;
                    }

                if ((item.mana > 0) & flag)
                {
                    if (player.statMana >= (int)(item.mana * player.manaCost))
                    {
                        player.manaRegenDelay = (int)player.maxRegenDelay;
                        player.statMana -= (int)(item.mana * player.manaCost);
                    }
                    else
                    {
                        flag = false;
                    }
                }

                if (player.whoAmI == Main.myPlayer && item.type == ItemID.Carrot && !Main.cEd)
                    flag = false;

                if (num == 27)
                {
                    num = Main.rand.Next(3);
                    if (num == 0)
                        num = 27;
                    if (num == 1)
                        num = 101;
                    if (num == 2)
                        num = 102;
                }

                if (flag)
                {
                    ItemLoader.UseItem(item, player);
                    legacySoundStyle = item.UseSound;
                    int num2 = item.buffTime;
                    if (num2 == 0)
                        num2 = 3600;
                    player.AddBuff(num, num2);
                    if (item.consumable)
                    {
                        if (ItemLoader.ConsumeItem(item, player))
                            item.stack--;
                        if (item.stack <= 0)
                            item.TurnToAir();
                    }
                }
            }

            if (legacySoundStyle == null)
                return;
            Main.PlaySound(legacySoundStyle, player.position);
            Recipe.FindRecipes();
        }

        public static void ApiQuickHeal(this Player player)
        {
            if (player.noItems)
                return;
            if (player.statLife == player.statLifeMax2 || player.potionDelay > 0)
                return;
            Item item = player.APIQuickHeal_GetItemToUse();
            if (item == null)
                return;
            Main.PlaySound(item.UseSound, player.position);
            if (item.potion)
            {
                if (item.type == ItemID.RestorationPotion)
                {
                    player.potionDelay = player.restorationDelayTime;
                    player.AddBuff(BuffID.PotionSickness, player.potionDelay);
                }
                else
                {
                    player.potionDelay = player.potionDelayTime;
                    player.AddBuff(BuffID.PotionSickness, player.potionDelay);
                }
            }

            ItemLoader.UseItem(item, player);
            player.statLife += item.healLife;
            player.statMana += item.healMana;
            if (player.statLife > player.statLifeMax2)
                player.statLife = player.statLifeMax2;
            if (player.statMana > player.statManaMax2)
                player.statMana = player.statManaMax2;
            if (item.healLife > 0 && Main.myPlayer == player.whoAmI)
                player.HealEffect(item.healLife);
            if (item.healMana > 0)
                if (Main.myPlayer == player.whoAmI)
                    player.ManaEffect(item.healMana);
            if (ItemLoader.ConsumeItem(item, player))
                item.stack--;
            if (item.stack <= 0)
                item.TurnToAir();
            Recipe.FindRecipes();
        }
        
        public static Item APIQuickHeal_GetItemToUse(this Player player)
        {
            int num = player.statLifeMax2 - player.statLife;
            Item result = null;
            int num2 = -player.statLifeMax2;
            for (int i = 0; i < 58; i++)
            {
                Item item = player.inventory[i];
                if (item.stack <= 0 || item.type <= ItemID.None || !item.potion || item.healLife <= 0 || !ItemLoader.CanUseItem(item, player))
                    continue;
                int num3 = item.healLife - num;
                if (num2 < 0)
                {
                    if (num3 <= num2)
                        continue;
                    result = item;
                    num2 = num3;
                }
                else if (num3 < num2 && num3 >= 0)
                {
                    result = item;
                    num2 = num3;
                }
            }

            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();
            for (int i = 0; i < character.Inventories.Length; i += 1)
                if (character.ActiveInvPage != i)
                    for (int j = 0; j < character.Inventories[i].Length; j += 1)
                    {
                        Item item = character.Inventories[i][j];
                        if (item.stack <= 0 || item.type <= ItemID.None || !item.potion || item.healLife <= 0 || !ItemLoader.CanUseItem(item, player))
                            continue;
                        int num3 = item.healLife - num;
                        if (num2 < 0)
                        {
                            if (num3 <= num2)
                                continue;
                            result = item;
                            num2 = num3;
                        }
                        else if (num3 < num2 && num3 >= 0)
                        {
                            result = item;
                            num2 = num3;
                        }
                    }

            return result;
        }
        
        public static void ApiQuickMana(this Player player)
        {
            if (player.noItems)
                return;
            if (player.statMana == player.statManaMax2)
                return;
            for (int i = 0; i < 58; i++)
                APIQuickMana_TryItem(player, player.inventory[i]);

            PlayerCharacter character = player.GetModPlayer<PlayerCharacter>();
            for (int i = 0; i < character.Inventories.Length; i += 1)
                if (character.ActiveInvPage != i)
                    for (int j = 0; j < character.Inventories[i].Length; j += 1)
                        APIQuickMana_TryItem(player, character.Inventories[i][j]);
        }

        private static void APIQuickMana_TryItem(this Player player, Item item)
        {
            if (item.stack <= 0 || item.type <= ItemID.None || item.healMana <= 0 || player.potionDelay != 0 && item.potion || !ItemLoader.CanUseItem(item, player))
                return;
            Main.PlaySound(item.UseSound, player.position);
            if (item.potion)
            {
                if (item.type == ItemID.RestorationPotion)
                {
                    player.potionDelay = player.restorationDelayTime;
                    player.AddBuff(BuffID.PotionSickness, player.potionDelay);
                }
                else
                {
                    player.potionDelay = player.potionDelayTime;
                    player.AddBuff(BuffID.PotionSickness, player.potionDelay);
                }
            }

            ItemLoader.UseItem(item, player);
            player.statLife += item.healLife;
            player.statMana += item.healMana;
            if (player.statLife > player.statLifeMax2)
                player.statLife = player.statLifeMax2;
            if (player.statMana > player.statManaMax2)
                player.statMana = player.statManaMax2;
            if (item.healLife > 0 && Main.myPlayer == player.whoAmI)
                player.HealEffect(item.healLife);
            if (item.healMana > 0)
            {
                player.AddBuff(BuffID.ManaSickness, Player.manaSickTime);
                if (Main.myPlayer == player.whoAmI)
                    player.ManaEffect(item.healMana);
            }

            if (ItemLoader.ConsumeItem(item, player))
                item.stack--;
            if (item.stack <= 0)
                item.TurnToAir();
            Recipe.FindRecipes();
        }
    }
}