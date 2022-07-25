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

            thirdTerminal.Call(secondTerminal.Number);  // third => second (in)
            Thread.Sleep(1700);
            thirdTerminal.EndCall();

            secondTerminal.Call(firstTerminal.Number);  // second => first (in)
            Thread.Sleep(2500);
            secondTerminal.EndCall();


            Console.WriteLine();
            Console.WriteLine($"Sorted records to {firstContact.Subscriber.FirstName}, number - {firstContact.Number}, tariff plan - {firstContact.Tariff.TariffType}:");
            foreach (var item in render.SortCalls(bs.GetReport(firstTerminal.Number), Enums.TypeSort.SortByNumber))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }
            Console.WriteLine($"Actually balance for number {firstContact.Number} - {firstContact.Subscriber.Money}"); // only outgoing calls minus the balance 


            Console.WriteLine();
            Console.WriteLine($"Sorted records to {secondContact.Subscriber.FirstName}, number - {secondContact.Number}, tariff plan - {secondContact.Tariff.TariffType}:");
            foreach (var item in render.SortCalls(bs.GetReport(secondTerminal.Number), Enums.TypeSort.SortByDate))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }
            Console.WriteLine($"Actually balance for number {secondContact.Number} - {secondContact.Subscriber.Money}"); // only outgoing calls minus the balance 

            Console.WriteLine();
            Console.WriteLine($"Sorted records to {thirdContact.Subscriber.FirstName}, number - {thirdContact.Number}, tariff plan - {thirdContact.Tariff.TariffType}:");
            foreach (var item in render.SortCalls(bs.GetReport(thirdTerminal.Number), Enums.TypeSort.SortByCost))
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    item.CallType, item.Date, item.Time.ToString("mm:ss"), item.Cost, item.Number);
            }
            Console.WriteLine($"Actually balance for number {thirdContact.Number} - {thirdContact.Subscriber.Money}"); // only outgoing calls minus the balance 

            Console.ReadKey();


        }
    }
}