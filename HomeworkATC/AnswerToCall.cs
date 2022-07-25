using HomeworkATC.Enums;

namespace HomeworkATC.Args
{
    public class AnswerToCall : EventArgs, ICalling
    {
        public int TelephoneNumber { get; private set; }
        public int TargetTelephoneNumber { get; private set; }
        public CallState StateInCall;
        public Guid Id { get; private set; }
        public AnswerToCall(int number, int target, CallState state)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
        }
        public AnswerToCall(int number, int target, CallState state, Guid id)
        {
            TelephoneNumber = number;
            TargetTelephoneNumber = target;
            StateInCall = state;
            Id = id;
        }


    }
}