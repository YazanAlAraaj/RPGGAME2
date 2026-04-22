using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    public class HealingPotion : Item
    {
        public int AmountToHeal { get; set; }

        public HealingPotion(int ID, string Name, String NamePlural, int AmountToHeal) : base(ID , Name ,NamePlural)
        {
            this.AmountToHeal = AmountToHeal;
        }
    }

}
