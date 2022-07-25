using HomeworkATC.Args;
using HomeworkATC.AutomaticTelephoneExchange;
using HomeworkATC.Enums;

namespace HomeworkATC.Interfaces
{
    public interface IATC : IStorage<CallInformation>
    {
        Terminal GetNewTerminal(IUserContract contract);
        IUserContract RegisterContract(Subscriber subscriber, TariffType type);
        void CallingTo(object sender, ICalling e);
    }
}