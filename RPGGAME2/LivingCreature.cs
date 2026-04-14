using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class LivingCreature
    {
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        
        public LivingCreature (int maxHP, int currentHP)
        {
            this.MaxHP = maxHP;
            this.CurrentHP = currentHP;
        }
    }
}
