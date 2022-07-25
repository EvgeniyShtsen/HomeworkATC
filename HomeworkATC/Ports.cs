using HomeworkATC.Args;
using HomeworkATC.Enums;

namespace HomeworkATC.AutomaticTelephoneExchange
{

    public class Ports
    {
        public PortState State;
        public bool Flag;

        public event EventHandler<Calling> CallPortEvent;
        public event EventHandler<AnswerToCall> AnswerPortEvent;
        public event EventHandler<Calling> CallEvent;
        public event EventHandler<AnswerToCall> AnswerEvent;
        public event EventHandler<CallEnding> EndCallEvent;

        public Ports()
        {
            State = PortState.Disconnect;
        }

        public bool Connect(Terminal terminal)
        {
            if (State == PortState.Disconnect)
            {
                State = PortState.Connect;
                terminal.CallEvent += CallingTo;
                terminal.AnswerEvent += AnswerTo;
                terminal.EndCallEvent += EndCall;
                Flag = true;
            }
            return Flag;
        }

        public bool Disconnect(Terminal terminal)
        {
            if (State == PortState.Connect)
            {
                State = PortState.Disconnect;
                terminal.CallEvent -= CallingTo;
                terminal.AnswerEvent -= AnswerTo;
                terminal.EndCallEvent -= EndCall;
                Flag = false;
            }
            return false;
        }

        public virtual void RaiseIncomingCallEvent(int number, int targetNumber)
        {
            if (CallPortEvent != null)
            {
                CallPortEvent(this, new Calling(number, targetNumber));
            }
        }
        public virtual void RaiseIncomingCallEvent(int number, int targetNumber, Guid id)
        {
            if (CallPortEvent != null)
            {
                CallPortEvent(this, new Calling(number, targetNumber, id));
            }
        }
        public virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state)
        {
            if (AnswerPortEvent != null)
            {
                AnswerPortEvent(this, new AnswerToCall(number, targetNumber, state));
            }
        }
        public virtual void RaiseAnswerCallEvent(int number, int targetNumber, CallState state, Guid id)
        {
            if (AnswerPortEvent != null)
            {
                AnswerPortEvent(this, new AnswerToCall(number, targetNumber, state, id));
            }
        }

        public virtual void RaiseCallingToEvent(int number, int targetNumber)
        {
            if (CallEvent != null)
            {
                CallEvent(this, new Calling(number, targetNumber));
            }
        }

        public virtual void RaiseAnswerToEvent(AnswerToCall eventArgs)
        {
            if (AnswerEvent != null)
            {
                AnswerEvent(this, new AnswerToCall(
                    eventArgs.TelephoneNumber,
                    eventArgs.TargetTelephoneNumber,
                    eventArgs.StateInCall,
                    eventArgs.Id));
            }
        }

        public virtual void RaiseEndCallEvent(Guid id, int number)
        {
            if (EndCallEvent != null)
            {
                EndCallEvent(this, new CallEnding(id, number));
            }
        }

        public void CallingTo(object sender, Calling e)
        {
            RaiseCallingToEvent(e.TelephoneNumber, e.TargetTelephoneNumber);
        }

        public void AnswerTo(object sender, AnswerToCall e)
        {
            RaiseAnswerToEvent(e);
        }

        public void EndCall(object sender, CallEnding e)
        {
            RaiseEndCallEvent(e.Id, e.TelephoneNumber);
        }

        public void IncomingCall(int number, int targetNumber)
        {
            RaiseIncomingCallEvent(number, targetNumber);
        }
        public void IncomingCall(int number, int targetNumber, Guid id)
        {
            RaiseIncomingCallEvent(number, targetNumber, id);
        }

        public void AnswerCall(int number, int targetNumber, CallState state)
        {
            RaiseAnswerCallEvent(number, targetNumber, state);
        }
        public void AnswerCall(int number, int targetNumber, CallState state, Guid id)
        {
            RaiseAnswerCallEvent(number, targetNumber, state, id);
        }


    }
}