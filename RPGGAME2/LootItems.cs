using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class LootItems
    {
        public Item Details { get; set; }
        public int DropPercentage { get; set; }
        public bool DefaultItem {  get; set; }

        public Item (Item Details, int DropPercentage, bool DefaultItem)
        {
            this.Details = Details;
            this.DropPercentage = DropPercentage;
            this.DefaultItem = DefaultItem;
        }
    }
}
