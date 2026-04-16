using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class QuestCompletionItem
    {
        public Item Details { get; set; }
        public int Quantity { get; set; }

        

        public QuestCompletionItem(Item Details, int Quantity)
        {
            this.Details = Details;
            this.Quantity = Quantity;
        }
    }
}
