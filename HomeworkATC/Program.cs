using HomeworkATC.AutomaticTelephoneExchange;
using HomeworkATC.BillingSystem;
using HomeworkATC.Interfaces;

namespace HomeworkATC
{
    class Program
    {
        static void Main(string[] args)
        {
            IATC ate = new ATC();
            IReportRender render = new ReportSorting();
            IBillingSystem bs = new BillingSystem.BillingSystem(ate);

            IUserContract firstContact = ate.RegisterContract(new Subscriber("Emanuel", "Macron"), Enums.TariffType.Light);
            IUserContract secondContact = ate.RegisterContract(new Subscriber("Vladimir", "Puding"), Enums.TariffType.Pro);
            IUserContract thirdContact = ate.RegisterContract(new Subscriber("Francisco", "Michlen"), Enums.TariffType.Standart);

            firstContact.Subscriber.AddMoney(5500);
            var firstTerminal = ate.GetNewTerminal(firstContact);
            firstTerminal.ConnectToPort();

            secondContact.Subscriber.AddMoney(15000);
            var secondTerminal = ate.GetNewTerminal(secondContact);
            secondTerminal.ConnectToPort();

            thirdContact.Subscriber.AddMoney(9800);
            var thirdTerminal = ate.GetNewTerminal(thirdContact);
            thirdTerminal.ConnectToPort();

            firstTerminal.Call(secondTerminal.Number); // first => second (out)
            Thread.Sleep(1500);
            firstTerminal.EndCall();

            firstTerminal.Call(thirdTerminal.Number);  // first => third (out)
            Thread.Sleep(2400);
            firstTerminal.EndCall();

            thirdTerminal.Call(firstTerminal.Number);  // third => first (in)
            Thread.Sleep(3200);
            thirdTerminal.EndCall();

            firstContact.ChangeTariff(Enums.TariffType.Pro);





            Console.WriteLine();
            Console.WriteLine("Sorted records:");
            foreach (var item in render.SortCalls(bs.GetReport(firstTerminal.Number), Enums.TypeSort.SortByDate))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }

            Console.ReadKey();


        }
    }
}