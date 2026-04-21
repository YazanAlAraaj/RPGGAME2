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
        public const int Item_ID_Yazans_Sword = 99;
        //items end
        //--------------------------------------------------------------------------------------------------------------------
        //locations start
        public const int Location_ID_Home = 1;
        public const int Location_ID_TownSquare = 2;
        //locations end
        //--------------------------------------------------------------------------------------------------------------------
        //monsters start
        public const int Monster_ID_Rat = 1;

        //monsters end


        private static void PopulateItems()
        {
            //----------------------------------------------------------------------------------
            Items.Add(new Weapon(Item_ID_Rusty_Sword, "Rusty Sword", "Rusty Swords", 4, 1));
            //----------------------------------------------------------------------------------
            Items.Add(new Item(Item_ID_Rat_Tail, "Rat Tail", "Rat Tails"));
            //----------------------------------------------------------------------------------
            Items.Add(new HealingPotion(Item_ID_Healing_Potion, "Healing Potion", "Healing Potions", 10));
            //----------------------------------------------------------------------------------

        }

        private static void PopulateLocations()
        {
            //----------------------------------------------------------------------------------
            Location Home = new Location(Location_ID_Home,"Home", "This is your home");
            Locations.Add(Home);
            //----------------------------------------------------------------------------------
            Location TownSquare = new Location(Location_ID_TownSquare, "Town Square", "Welcome to the town square you see a fountain");
            Locations.Add(TownSquare);
            TownSquare.MonsterLivingHere = MonsterByID(Monster_ID_Rat);
            //----------------------------------------------------------------------------------
            Home.LocationToNorth = TownSquare;
            TownSquare.LocationToSouth = Home;
        }

        private static void PopulateMonsters()
        {
            //----------------------------------------------------------------------------------
            Monster rat = new Monster(Monster_ID_Rat, "Rat", 2, 5, 5, 5, 5);
            rat.LootTable.Add(new LootItem(ItemByID(Item_ID_Rat_Tail), 80, true));
            Monsters.Add(rat);
            //----------------------------------------------------------------------------------

        }

        public static Item ItemByID(int ID)
        {
            foreach (Item item in Items)
            {
                if (item.ID == ID)
                    return item;
            }
            return null;
        }

        public static Monster MonsterByID(int ID)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID.Equals(ID))
                    return monster;
            }
            return null;
        }

        
        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateLocations();
        }

    }
}
