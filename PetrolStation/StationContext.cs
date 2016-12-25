using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PetrolStation
{
    class StationContext:DbContext
    {
        public StationContext():base("DefaultConnection")
        {
            
        }
        public DbSet<Station> Stations { get; set; }
    }
}
