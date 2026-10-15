using System;
using System.Collections.Generic;
using System.Text;

namespace Ledger.Domain
{
    public static class SystemCategories
    {
        public static readonly Category Weapon = new("weapon", "Weapons");
        public static readonly Category Pistol = new("pistol", "Pistols");
        public static readonly Category Knife = new("knife", "Knives");
        public static readonly Category Helmet = new("helmet", "Helmets");
        public static readonly Category Character = new("character", "Characters");

        public static readonly IReadOnlyList<Category> All =
        [
            Weapon, Pistol, Knife, Helmet, Character
        ];
    }

}
