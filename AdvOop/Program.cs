using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvOop
{
    interface ICustomer1 {
        void TestPrint1();
    }

    interface ICustomer2
    {
        void TestPrint2();
    }

    class Cust : ICustomer1, ICustomer2
    {
        public void TestPrint1() {
            Console.WriteLine("Interface Print!.");
        }

        public void TestPrint2() {
            Console.WriteLine("Interface I2 Print!.");
        }
    }


    class Program : Customer
    {
             
        public static void ProgMain() {

            Cust c = new Cust();
            c.TestPrint1();


            FullTimeEmployee ft = new FullTimeEmployee()
            {
                ID = 101,
                FirstName = "Lal",
                LastName = "Badhusha",
                AnnualSalary = 15000
            };
            Console.WriteLine(ft.GetFullName());
            Console.WriteLine(ft.GetMonthlySal());
            Console.WriteLine("------------------");

            ContractEmployee ct = new ContractEmployee()
            {
                ID = 102,
                FirstName = "Lal",
                LastName = "Badhusha",
                HourlyPay=25,
                TotalHours=100
            };
            Console.WriteLine(ct.GetFullName());
            Console.WriteLine(ct.GetMonthlySal());
            Console.WriteLine("------------------");

        }

        public override void Print()
        {
            Console.WriteLine("Print Method!.");
        }
        static void Main(string[] args)
        {
            Customer c = new Program();
            c.Print();
        }



    }

    public abstract class Customer
    {
        public abstract void Print();

    }
}
