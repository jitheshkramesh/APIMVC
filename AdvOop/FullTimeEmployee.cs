using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvOop
{
    class FullTimeEmployee:BaseEmployee
    {
        public int AnnualSalary { get; set; }
        public override int GetMonthlySal() {
           return this.AnnualSalary / 12;
        }
    }
}
