using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvOop
{
 public class ContractEmployee:BaseEmployee
    {
        public int HourlyPay { get; set; }
        public int TotalHours { get; set; }
        public override int GetMonthlySal()
        {
            return this.HourlyPay * this.TotalHours;
        }
    }
}
