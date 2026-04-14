using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int Level { get; set; }

        public Player(int gold, int experience, int level,int MaxHP,int CurrentHP) : base(MaxHP , CurrentHP)
        {
            this.Gold = gold;
            this.Experience = experience;
            this.Level = level;
            
        }

    }
}
