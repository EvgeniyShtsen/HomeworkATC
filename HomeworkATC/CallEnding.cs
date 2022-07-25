namespace HomeworkATC.Args
{
    public class CallEnding : EventArgs, ICalling
    {
        public Guid Id { get; private set; }
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }

        public CallEnding(Guid id, int number)
        {
            Id = id;
            TelephoneNumber = number;
        }
    }
}