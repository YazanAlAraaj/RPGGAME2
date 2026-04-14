using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Weapon : Item
    {
        public int MaxDamage { get; set; }
        public int MinDamage { get; set; }

        public Weapon(int ID, string Name, String NamePlural, int MaxDamage, int MinDamage) : base(ID, Name, NamePlural)
        {
            this.MaxDamage = MaxDamage;
            this.MinDamage = MinDamage;
        }
    }
}
