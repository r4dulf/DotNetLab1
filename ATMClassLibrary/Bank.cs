
namespace ATMClassLibrary.Entities
{
    public class Bank
    {
        public string Name { get; private set; }
        private List<AutomatedTellerMachine> atms;
        private List<Account> accounts;

        public Bank(string name)
        {
            Name = name;
            atms = new List<AutomatedTellerMachine>();
            accounts = new List<Account>();
        }

        public void AddATM(AutomatedTellerMachine atm) => atms.Add(atm);
        public void AddAccount(Account account) => accounts.Add(account);

        public bool Authenticate(string cardNumber, int pinCode)
        {
            var account = GetAccount(cardNumber);
            return account != null && account.PIN == pinCode;
        }

        public Account GetAccount(string cardNumber) => accounts.Find(a => a.CardNumber == cardNumber);

        public bool Withdraw(string cardNumber, decimal amount)
        {
            var account = GetAccount(cardNumber);
            if (account != null && account.Balance >= amount)
            {
                account.Balance -= amount;
                return true;
            }
            return false;
        }

        public bool Deposit(string cardNumber, decimal amount)
        {
            var account = GetAccount(cardNumber);
            if (account != null)
            {
                account.Balance += amount;
                return true;
            }
            return false;
        }
    }
}
