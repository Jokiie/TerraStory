using System;
using System.Collections.Generic;
using TerraStory.Enums;

namespace TerraStory
{
    public static class Constants
    {
        public const float MaxScreenWidth = 3840f;
        public const float MaxScreenHeight = 2400f;
        public const string ModName = "TerraStory";
        public static Dictionary<string, JobAdvancement> jobadvancementByName = new Dictionary<string, JobAdvancement>
        {
            {"beginner", JobAdvancement.Beginner},
            {"warrior", JobAdvancement.Warrior},
            {"thief", JobAdvancement.Thief},
            {"magician", JobAdvancement.Magician},
            {"archer", JobAdvancement.Archer},
            {"pirate", JobAdvancement.Pirate},
            {"beastTamer", JobAdvancement.BeastTamer}
        };

        public static double Tau => Math.PI * 2;
    }
}