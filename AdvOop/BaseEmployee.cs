using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvOop
{
   public abstract class BaseEmployee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GetFullName()
        {
            return this.FirstName + "" + LastName;
        }
        public abstract int GetMonthlySal();
    }
}
