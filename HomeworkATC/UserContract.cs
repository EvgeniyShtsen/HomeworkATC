using HomeworkATC.Enums;
using HomeworkATC.Interfaces;

namespace HomeworkATC
{
    public class UserContract : IUserContract
    {
        static Random rnd = new Random();

        public Subscriber Subscriber { get; private set; }
        public int Number { get; private set; }
        public TariffTypes Tariff { get; private set; }
        public DateTime LastTariffUpdateDate;


        public UserContract(Subscriber subscriber, TariffType tariffType)
        {
            LastTariffUpdateDate = DateTime.Now;
            Subscriber = subscriber;
            Number = rnd.Next(100, 200); // any variation types (styles) of telephon numbers
            Tariff = new TariffTypes(tariffType);
        }

        public bool ChangeTariff(TariffType tariffType)
        {
            if (DateTime.Now.AddMonths(-1) >= LastTariffUpdateDate)
            {
                LastTariffUpdateDate = DateTime.Now;
                Tariff = new TariffTypes(tariffType);
                Console.WriteLine("Tariff changed");
                return true;
            }
            else
            {
                Console.WriteLine("Sorry, but you can change you fariff only one time per mounth");
                return false;
            }

        }
    }
}