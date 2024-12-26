using ATMClassLibrary.Entities;

namespace ATMApplication
{
    class ConsoleApp
    {
        public static void Main()
        {
            // Ваша логіка для консольного додатка
            Bank bank = InitializeBank();
            Console.WriteLine("Welcome to the ATM!");

            // Авторизація
            Account account = Authenticate(bank);

            if (account != null)
            {
                Console.WriteLine($"Welcome, {account.Owner}!");
                RunATMMenu(bank, account);
            }
            else
            {
                Console.WriteLine("Invalid login. Goodbye!");
            }
        }

        static Bank InitializeBank()
        {
            Bank bank = new Bank("MyBank");

            // Додавання клієнтів
            bank.AddAccount(new Account("1234567890", "John Doe", 1234, 5000m));
            bank.AddAccount(new Account("9876543210", "Jane Smith", 5678, 10000m));

            // Додавання банкомату
            bank.AddATM(new AutomatedTellerMachine("ATM-001", "123 Main Street", 20000m));
            return bank;
        }

        static Account Authenticate(Bank bank)
        {
            Console.Write("Enter your card number: ");
            string cardNumber = Console.ReadLine();

            Console.Write("Enter your PIN: ");
            if (!int.TryParse(Console.ReadLine(), out int pin))
            {
                Console.WriteLine("Invalid PIN format.");
                return null;
            }

            if (bank.Authenticate(cardNumber, pin))
            {
                return bank.GetAccount(cardNumber);
            }
            return null;
        }

        static void RunATMMenu(Bank bank, Account account)
        {
            while (true)
            {
                Console.WriteLine("\nPlease select an option:");
                Console.WriteLine("1. Check Balance");
                Console.WriteLine("2. Withdraw");
                Console.WriteLine("3. Deposit");
                Console.WriteLine("4. Exit");

                Console.Write("Your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CheckBalance(account);
                        break;
                    case "2":
                        Withdraw(bank, account);
                        break;
                    case "3":
                        Deposit(bank, account);
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using our ATM. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void CheckBalance(Account account)
        {
            Console.WriteLine($"\nYour current balance is: ${account.Balance}");
        }

        static void Withdraw(Bank bank, Account account)
        {
            Console.Write("\nEnter the amount to withdraw: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    if (bank.Withdraw(account.CardNumber, amount))
                    {
                        Console.WriteLine($"Withdrawal successful! You withdrew ${amount}. New balance: ${account.Balance}");
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal failed. Insufficient funds.");
                    }
                }
                else
                {
                    Console.WriteLine("The amount must be greater than zero.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }

        static void Deposit(Bank bank, Account account)
        {
            Console.Write("\nEnter the amount to deposit: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                if (amount > 0)
                {
                    bank.Deposit(account.CardNumber, amount);
                    Console.WriteLine($"Deposit successful! You deposited ${amount}. New balance: ${account.Balance}");
                }
                else
                {
                    Console.WriteLine("The amount must be greater than zero.");
                }
            }
            else
            {
                Console.WriteLine("Invalid amount.");
            }
        }
    }
}