using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    public static class World
    {
        public static List<Item> Items = new List<Item>();
        public static List<Location> Locations = new List<Location>();
        public static List<Quest> Quests = new List<Quest>();
        public static List<Monster> Monsters = new List<Monster>();
        //--------------------------------------------------------------------------------------------------------------------
        //items start
        public const int Item_ID_Rusty_Sword = 1;
        public const int Item_ID_Rat_Tail = 2;
        public const int Item_ID_Healing_Potion = 3;
        //items end
        //--------------------------------------------------------------------------------------------------------------------
        //locations start
        
        //locations end
        //--------------------------------------------------------------------------------------------------------------------
        //monsters start

        //monsters end


        private static void PopulateItems()
        {
            Items.Add(new Weapon(Item_ID_Rusty_Sword, "Rusty Sword", "Rusty Swords",4,0));
            Items.Add(new Item(Item_ID_Rat_Tail, "Rat Tail", "Rat Tails"));
            Items.Add(new HealingPotion(Item_ID_Healing_Potion, "Healing Potion", "Healing Potions", 10));

        }

        private static void PopulateLocations()
        {

        }

        private static void PopulateMonsters()
        {
            
        }


    }
}
