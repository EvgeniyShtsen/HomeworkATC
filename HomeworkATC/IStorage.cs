namespace HomeworkATC.Interfaces
{
    public interface IStorage<T>
    {
        IList<T> GetInfoList();
    }
}