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
using static Terraria.ModLoader.ModContent;

namespace TerraStory.Items.Weapons
{
    public class TSWeapons : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool CloneNewInstances => true;

        public override void SetDefaults(Item item)
        {

        }
        
        public override void OnHitNPC(Item item, Player player, NPC target, int damage, float knockBack, bool crit)
        {

        }
        public override void UpdateEquip(Item item, Player player)
        {

        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {

        }       
    }
}
