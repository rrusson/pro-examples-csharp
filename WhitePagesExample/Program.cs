using System;

namespace WhitePagesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var idCheck = new IdentityCheck();
            idCheck.Check();

            var leadVerify = new LeadVerify();
            leadVerify.Verify();

            var phoneSearch = new PhoneSearch();
            phoneSearch.Search("6464806649");

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("-press any key to continue-");
            Console.ReadKey();
        }
    }
}
