using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        public Location CurrentLocation { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> PlayerQuests { get; set; }

        public Player(int gold, int experience, int level, int MaxHP, int CurrentHP) : base(MaxHP, CurrentHP)
        {
            this.Gold = gold;
            this.Experience = experience;
            this.Level = level;
            Inventory = new List<InventoryItem>();
            PlayerQuests = new List<PlayerQuest>();

        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.ItemRequiredToEnter == null)
            {
                return true;

            }

            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerquest in PlayerQuests)
            {
                if (playerquest.Details.ID == quest.ID)
                {
                    return true;

                }
            }
            return false;
        }

        public bool HasCompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerquest in PlayerQuests)
            {
                if (playerquest.Details.ID == quest.ID)
                {
                    return playerquest.IsCompleted;
                }
            }
            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompeltionItems)
            {
                bool FoundItemInPlayerInventory = false;
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        FoundItemInPlayerInventory = true;
                        if (ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }
                    }

                }
                if (!FoundItemInPlayerInventory)
                {
                    return false;
                }
            }
            return true;
        }

        public void RemoveQuestCompletionItem(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.QuestCompeltionItems)
            {
                foreach (InventoryItem ii in Inventory)
                {
                    if (ii.Details.ID == qci.Details.ID)
                    {
                        ii.Quantity -= qci.Quantity;
                        break;
                    }

                }
            }
        }

        public void AddItemToInventory(Item ItemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if (ii.Details.ID == ItemToAdd.ID)
                {
                    ii.Quantity++;
                    return;
                }
            }
        }

        public void MarkThisQuestCompleted(Quest quest)
        {

            foreach(PlayerQuest pq in PlayerQuests)
            {
                if(pq.Details.ID == quest.ID)
                {
                    pq.IsCompleted = true;
                    return;
                }
            }
            
            
        }
    }
}
