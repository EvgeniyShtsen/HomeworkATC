using HomeworkATC.BillingSystem;
using HomeworkATC.Enums;

namespace HomeworkATC.Interfaces
{
    public interface IReportRender
    {
        void Render(Report report);
        IEnumerable<ReportRecord> SortCalls(Report report, TypeSort sortType);
    }
}