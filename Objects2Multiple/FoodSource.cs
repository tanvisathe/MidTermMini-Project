using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objects2Multiple
{
    class FoodSource
    {
       public string Name { get; }
       public int Supply { get;}
        
        public FoodSource(string name, int supply)
        {
            this.Name = name;
            this.Supply = supply;
        }
    }
}
