using HomeworkATC.Args;
using HomeworkATC.Enums;

namespace HomeworkATC.AutomaticTelephoneExchange
{
    public class Terminal
    {
        private int _number;
        public int Number
        {
            get
            {
                return _number;
            }
        }
        private Ports _terminalPort;
        private Guid _id;
        public event EventHandler<Calling> CallEvent;
        public event EventHandler<AnswerToCall> AnswerEvent;
        public event EventHandler<CallEnding> EndCallEvent;
        public Terminal(int number, Ports port)
        {
            _number = number;
            _terminalPort = port;
        }
        public virtual void RaiseCallEvent(int targetNumber)
        {
            if (CallEvent != null)
                CallEvent(this, new Calling(_number, targetNumber));
        }

        public virtual void RaiseAnswerEvent(int targetNumber, CallState state, Guid id)
        {
            if (AnswerEvent != null)
            {
                AnswerEvent(this, new AnswerToCall(_number, targetNumber, state, id));
            }
        }

        public virtual void RaiseEndCallEvent(Guid id)
        {
            if (EndCallEvent != null)
                EndCallEvent(this, new CallEnding(id, _number));
        }

        public void Call(int targetNumber)
        {
            RaiseCallEvent(targetNumber);
        }

        public void TakeIncomingCall(object sender, Calling e)
        {
            _id = e.Id;
            Console.WriteLine("Have incoming Call at number: {0} to terminal {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            Console.WriteLine();
            AnswerToCall(e.TelephoneNumber, CallState.Answered, e.Id);   
        }

        public void ConnectToPort()
        {
            if (_terminalPort.Connect(this))
            {
                _terminalPort.CallPortEvent += TakeIncomingCall;
                _terminalPort.AnswerPortEvent += TakeAnswer;
            }
        }

        public void AnswerToCall(int target, CallState state, Guid id)
        {
            RaiseAnswerEvent(target, state, id);
        }

        public void EndCall()
        {
            RaiseEndCallEvent(_id);
        }

        public void TakeAnswer(object sender, AnswerToCall e)
        {
            _id = e.Id;
            if (e.StateInCall == CallState.Answered)
            {
                Console.WriteLine("Terminal with number: {0}, have answer on call a number: {1}", e.TelephoneNumber, e.TargetTelephoneNumber);
            }
            else
            {
                Console.WriteLine("Terminal with number: {0}, have rejected call", e.TelephoneNumber);
            }
        }
    }
}