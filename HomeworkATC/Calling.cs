namespace HomeworkATC.Args
{
    public class Calling : EventArgs, ICalling
    {
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        public Guid Id { get; private set; }
        public Calling(int number, int target)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
        }
        public Calling(int number, int target, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            Id = id;
        }
    }
}