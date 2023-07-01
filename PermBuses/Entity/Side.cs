using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermBuses.Entity
{
    internal class Side
    {
        public string name;

        public List<Stop> stopsList;

        public Side(string name, List<Stop> stopsList)
        {
            this.name = name;
            this.stopsList = stopsList;
        }
    }
}
