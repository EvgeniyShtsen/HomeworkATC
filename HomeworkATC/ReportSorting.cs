using HomeworkATC.Enums;
using HomeworkATC.Interfaces;

namespace HomeworkATC.BillingSystem
{
    public class ReportSorting : IReportRender
    {
        public ReportSorting()
        {

        }
        public void Render(Report report)
        {
            foreach (var record in report.GetRecords())
            {
                Console.WriteLine("Calls:\n Type {0} |\n Date: {1} |\n Duration: {2} | Cost: {3} | Telephone number: {4}",
                    record.CallType, record.Date, record.Time.ToString("mm:ss"), record.Cost, record.Number);
            }
        }
        public IEnumerable<ReportRecord> SortCalls(Report report, TypeSort sortType)
        {
            var rep = report.GetRecords();
            switch (sortType)
            {
                case TypeSort.SortByCallType:
                    return rep = rep.
                        OrderBy(x => x.CallType).
                        ToList();

                case TypeSort.SortByDate:
                    return rep = rep.
                        OrderBy(x => x.Date).
                        ToList();

                case TypeSort.SortByCost:
                    return rep = rep
                        .OrderBy(x => x.Cost)
                        .ToList();

                case TypeSort.SortByNumber:
                    return rep = rep.
                        OrderBy(x => x.Number).
                        ToList();

                default:
                    return rep;
            }
        }
    }
}