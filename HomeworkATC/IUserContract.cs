using HomeworkATC.Enums;

namespace HomeworkATC.Interfaces
{
    public interface IUserContract
    {
        Subscriber Subscriber { get; }
        int Number { get; }
        TariffTypes Tariff { get; }
        bool ChangeTariff(TariffType tariffType);
    }
}