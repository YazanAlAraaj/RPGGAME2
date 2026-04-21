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

        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> PlayerQuests { get; set; }

        public Player(int gold, int experience, int level,int MaxHP,int CurrentHP) : base(MaxHP , CurrentHP)
        {
            this.Gold = gold;
            this.Experience = experience;
            this.Level = level;
            Inventory = new List<InventoryItem>();
            PlayerQuests = new List<PlayerQuest>();
            
        }

    }
}
