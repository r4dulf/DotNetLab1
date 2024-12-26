
namespace ATMClassLibrary.Entities
{
    public class Account
    {
        public string CardNumber { get; private set; }
        public string Owner { get; private set; }
        public int PIN { get; private set; }
        public decimal Balance { get; set; }

        public Account(string cardNumber, string owner, int pin, decimal balance)
        {
            CardNumber = cardNumber;
            Owner = owner;
            PIN = pin;
            Balance = balance;
        }
    }
}
