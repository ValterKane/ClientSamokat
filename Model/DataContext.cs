using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSamokat.Model
{
    public class DataContext
    {
        public List<Bicycle>? bicycles { get; set; } = new List<Bicycle>();

        public List<BicyclesLock>? bicyclesLock { get; set; } = new List<BicyclesLock>();

        public  Occ occs { get; set; }

        public List<Journal>? journals { get; set; } = new List<Journal>();

        public List<Courier> couriers { get; set; } = new List<Courier>();

        public List<StatusTable>? statusTables { get; set; } = new List<StatusTable>();

    }
}
