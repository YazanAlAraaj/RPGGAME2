using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGAME2
{
    internal class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Location(int ID, string name, string description)
        {
            this.ID = ID;
            this.Name = name;
            this.Description = description;
        }
    }
}
