using HomeworkATC.AutomaticTelephoneExchange;
using HomeworkATC.Enums;
using HomeworkATC.Interfaces;

namespace HomeworkATC.BillingSystem
{
    public class BillingSystem : IBillingSystem
    {
        private IStorage<CallInformation> _storage;
        public BillingSystem(IStorage<CallInformation> storage)
        {
            _storage = storage;
        }

        public Report GetReport(int telephoneNumber)
        {
            var calls = _storage.GetInfoList().
                Where(x => x.MyNumber == telephoneNumber || x.TargetNumber == telephoneNumber).
                ToList();
            var report = new Report();

            foreach (var call in calls)
            {
                CallType callType;
                int number;
                if (call.MyNumber == telephoneNumber)
                {
                    callType = CallType.OutgoingCall;
                    number = call.TargetNumber;
                }
                else
                {
                    callType = CallType.IncomingCall;
                    number = call.MyNumber;
                }
                var record = new ReportRecord(callType, number, call.BeginCall, new DateTime((call.EndCall - call.BeginCall).Ticks), call.Cost); 
                report.AddRecord(record);
            }
            return report;
        }

    }
}