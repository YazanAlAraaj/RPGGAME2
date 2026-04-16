using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Item
    {
        public int ID {  get; set; }
        public string Name { get; set; }
        public string NamePlural { get; set; }

        public Item RewardItem { get; set; }

        public Item (int ID, string Name, string NamePlural, Item RewardItem)
        {
            this.ID = ID;
            this.Name = Name;
            this.NamePlural = NamePlural;
            this.RewardItem = RewardItem;
        }
    }
}
