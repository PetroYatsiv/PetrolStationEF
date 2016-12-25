using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace PetrolStation
{
    class AutoContext:DbContext
    {
        public AutoContext():base("DefaultConnection2")
        {
            
        }
        public DbSet<Auto> AutomobileSet { get; set; }
    }
}
