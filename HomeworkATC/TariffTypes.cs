using HomeworkATC.Enums;

namespace HomeworkATC
{
    public class TariffTypes
    {
        public int CostOfMonth { get; private set; }
        public int CostOfCallPerMinute { get; private set; }
        public int LimitCallInMonth { get; private set; }
        public TariffType TariffType { get; set; }
        public TariffTypes(TariffType type)
        {
            TariffType = type;
            switch (TariffType)
            {
                case TariffType.Light:
                    {
                        CostOfMonth = 10000;
                        LimitCallInMonth = 150;
                        CostOfCallPerMinute = 300;
                        break;
                    }
                case TariffType.Standart:
                    {
                        CostOfMonth = 15000;
                        LimitCallInMonth = 190;
                        CostOfCallPerMinute = 200;
                        break;
                    }
                case TariffType.Pro:
                    {
                        CostOfMonth = 20000;
                        LimitCallInMonth = 250;
                        CostOfCallPerMinute = 150;
                        break;
                    }
                default:
                    {
                        CostOfMonth = 0;
                        LimitCallInMonth = 0;
                        CostOfCallPerMinute = 0;
                        break;
                    }
            }
        }
    }
}