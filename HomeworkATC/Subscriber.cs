namespace HomeworkATC
{
    public class Subscriber
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Money { get; set; }

        public Subscriber(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Money = 0;
        }

        public void AddMoney(int money)
        {
            Money += money;
        }

        public void RemoveMoney(int money)
        {
            Money -= money;
        }
    }
}