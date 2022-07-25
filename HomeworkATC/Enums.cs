namespace HomeworkATC.Enums
{
    public enum CallState
    {
        Answered,
        Rejected
    }

    public enum CallType
    {
        IncomingCall,
        OutgoingCall
    }

    public enum PortState
    {
        Connect,
        Disconnect,
        InCall
    }

    public enum TariffType
    {
        Light,
        Standart,
        Pro
    }

    public enum TypeSort
    {
        SortByCallType,
        SortByDate,
        SortByCost,
        SortByNumber
    }
}