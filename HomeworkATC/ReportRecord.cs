using HomeworkATC.Enums;

namespace HomeworkATC.BillingSystem
{
    public class ReportRecord
    {
        public CallType CallType { get; private set; }
        public int Number { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime Time { get; private set; }
        public int Cost { get; private set; }

        public ReportRecord(CallType callType, int number, DateTime date, DateTime time, int cost)
        {
            CallType = callType;
            Number = number;
            Date = date;
            Time = time;
            Cost = cost;
        }
    }
}