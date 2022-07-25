namespace HomeworkATC.Args
{
    public interface ICalling
    {
        int TelephoneNumber { get; }
        int TargetTelephoneNumber { get; }
        Guid Id { get; }
    }
}