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
        public static List<Monster> Monsters = new List<Monster>();
        public static List<Quest> Quests = new List<Quest>();
        public static List<Location> Locations = new List<Location>();
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
        //quests start
        public const int Quest_ID_Clear_TownSquare = 1;
        //quests end
        //--------------------------------------------------------------------------------------------------------------------
        //monsters start
        public const int Monster_ID_Rat = 1;

        //monsters end
        //--------------------------------------------------------------------------------------------------------------------


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
        private static void PopulateQuests()
        {
            Quest ClearTownSquare = new Quest(Quest_ID_Clear_TownSquare, "ClearTownSquare", "Kill the rat to clear the town square", 15, 10);
            ClearTownSquare.QuestCompeltionItems.Add(new QuestCompletionItem(ItemByID(Item_ID_Rat_Tail),1));
            ClearTownSquare.RewardItem = ItemByID(Item_ID_Healing_Potion);
            
            
            Quests.Add(ClearTownSquare);
        }

        private static void PopulateLocations()
        {
            //---------------------------------------------------------------------------------- //Declare locations first
            Location Home = new Location(Location_ID_Home, "Home", "This is your home");
            Location TownSquare = new Location(Location_ID_TownSquare, "Town Square", "Welcome to the town square you see a fountain");
            //---------------------------------------------------------------------------------- //Location headings 2nd
            Home.LocationToNorth = TownSquare;
            TownSquare.LocationToSouth = Home;
            //---------------------------------------------------------------------------------- //Monsters in location 3rd
            TownSquare.MonsterLivingHere = MonsterByID(Monster_ID_Rat);
            TownSquare.QuestAvailableHere = QuestByID(Quest_ID_Clear_TownSquare);
            
            //---------------------------------------------------------------------------------- //Then we ADD them
            Locations.Add(Home);
            Locations.Add(TownSquare);
        }


        private static void PopulateMonsters()
        {
            //----------------------------------------------------------------------------------
            Monster rat = new Monster(Monster_ID_Rat, "Rat", 2, 5, 5, 5, 5);
            rat.LootTable.Add(new LootItem(ItemByID(Item_ID_Rat_Tail), 100, true));
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

        public static Quest QuestByID(int ID)
        {
            foreach (Quest quest in Quests)
            {
                if (quest.ID == ID)
                    return quest;
                
            }
            return null;
        }

        public static Monster MonsterByID(int ID)
        {
            foreach (Monster monster in Monsters)
            {
                if (monster.ID == ID)
                    return monster;
            }
            return null;
        }


        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

    }
}
