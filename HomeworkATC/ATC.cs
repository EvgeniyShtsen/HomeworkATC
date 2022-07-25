using HomeworkATC.Args;
using HomeworkATC.Enums;
using HomeworkATC.Interfaces;

namespace HomeworkATC.AutomaticTelephoneExchange
{
    public class ATC : IATC
    {
        private IDictionary<int, Tuple<Ports, IUserContract>> _usersData;
        private IList<CallInformation> _callList = new List<CallInformation>();
        public ATC()
        {
            _usersData = new Dictionary<int, Tuple<Ports, IUserContract>>();

        }

        public Terminal GetNewTerminal(IUserContract contract)
        {
            var newPort = new Ports();
            newPort.AnswerEvent += CallingTo;
            newPort.CallEvent += CallingTo;
            newPort.EndCallEvent += CallingTo;
            _usersData.Add(contract.Number, new Tuple<Ports, IUserContract>(newPort, contract));
            var newTerminal = new Terminal(contract.Number, newPort);
            return newTerminal;
        }

        public IUserContract RegisterContract(Subscriber subscriber, TariffType type)
        {
            var contract = new UserContract(subscriber, type);
            return contract;
        }

        public void CallingTo(object sender, ICalling e)
        {
            if ((_usersData.ContainsKey(e.TargetTelephoneNumber) && e.TargetTelephoneNumber != e.TelephoneNumber)
                || e is CallEnding)
            {
                CallInformation inf = null;
                Ports targetPort;
                Ports port;
                int number = 0;
                int targetNumber = 0;
                if (e is CallEnding)
                {
                    var callListFirst = _callList.First(x => x.Id.Equals(e.Id));
                    if (callListFirst.MyNumber == e.TelephoneNumber)
                    {
                        targetPort = _usersData[callListFirst.TargetNumber].Item1;
                        port = _usersData[callListFirst.MyNumber].Item1;
                        number = callListFirst.MyNumber;
                        targetNumber = callListFirst.TargetNumber;
                    }
                    else
                    {
                        port = _usersData[callListFirst.TargetNumber].Item1;
                        targetPort = _usersData[callListFirst.MyNumber].Item1;
                        targetNumber = callListFirst.MyNumber;
                        number = callListFirst.TargetNumber;
                    }
                }
                else
                {
                    targetPort = _usersData[e.TargetTelephoneNumber].Item1;
                    port = _usersData[e.TelephoneNumber].Item1;
                    targetNumber = e.TargetTelephoneNumber;
                    number = e.TelephoneNumber;
                }
                if (targetPort.State == PortState.Connect && port.State == PortState.Connect)
                {
                    var tuple = _usersData[number];
                    var targetTuple = _usersData[targetNumber];

                    if (e is AnswerToCall)
                    {

                        var answerArgs = (AnswerToCall)e;

                        if (!answerArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(answerArgs.Id)))
                        {
                            inf = _callList.First(x => x.Id.Equals(answerArgs.Id));
                        }

                        if (inf != null)
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall, inf.Id);
                        }
                        else
                        {
                            targetPort.AnswerCall(answerArgs.TelephoneNumber, answerArgs.TargetTelephoneNumber, answerArgs.StateInCall);
                        }
                    }
                    if (e is Calling)
                    {
                        if (tuple.Item2.Subscriber.Money > tuple.Item2.Tariff.CostOfCallPerMinute)
                        {
                            var callArgs = (Calling)e;

                            if (callArgs.Id.Equals(Guid.Empty))
                            {
                                inf = new CallInformation(
                                    callArgs.TelephoneNumber,
                                    callArgs.TargetTelephoneNumber,
                                    DateTime.Now);
                                _callList.Add(inf);
                            }

                            if (!callArgs.Id.Equals(Guid.Empty) && _callList.Any(x => x.Id.Equals(callArgs.Id)))
                            {
                                inf = _callList.First(x => x.Id.Equals(callArgs.Id));
                            }
                            if (inf != null)
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber, inf.Id);
                            }
                            else
                            {
                                targetPort.IncomingCall(callArgs.TelephoneNumber, callArgs.TargetTelephoneNumber);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Terminal with number {0} is not enough money to his terminal", e.TelephoneNumber);

                        }
                    }
                    if (e is CallEnding)
                    {
                        var args = (CallEnding)e;
                        inf = _callList.First(x => x.Id.Equals(args.Id));
                        inf.EndCall = DateTime.Now;
                        var sumOfCall = tuple.Item2.Tariff.CostOfCallPerMinute * TimeSpan.FromTicks((inf.EndCall - inf.BeginCall).Ticks).TotalMinutes;
                        inf.Cost = (int)sumOfCall;
                        targetTuple.Item2.Subscriber.RemoveMoney(inf.Cost);
                        targetPort.AnswerCall(args.TelephoneNumber, args.TargetTelephoneNumber, CallState.Rejected, inf.Id);
                    }
                }
            }
            else if (!_usersData.ContainsKey(e.TargetTelephoneNumber))
            {
                Console.WriteLine("You have calling to non-existent number");
            }
            else
            {
                Console.WriteLine("You can't call your number");
            }
        }

        public IList<CallInformation> GetInfoList()
        {
            return _callList;
        }
    }
}