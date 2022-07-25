using HomeworkATC.BillingSystem;

namespace HomeworkATC.Interfaces
{
    public interface IBillingSystem
    {
        Report GetReport(int telephoneNumber);
    }
}