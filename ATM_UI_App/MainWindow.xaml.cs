using System.Windows;
using ATMClassLibrary.Entities;

namespace ATM_UI_App
{
    public partial class MainWindow : Window
    {
        private Bank bank;
        private Account currentAccount;

        public MainWindow()
        {
            InitializeComponent();

            // Ініціалізація банку та клієнтів
            InitializeBank();

            // Відображення вікна авторизації
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                // Перевірка аутентифікації
                if (bank.Authenticate(loginWindow.CardNumber, loginWindow.Pin))
                {
                    currentAccount = bank.GetAccount(loginWindow.CardNumber);
                    InfoScreen.Text = $"Welcome, {currentAccount.Owner}! Please select an option.";
                }
                else
                {
                    MessageBox.Show("Invalid card number or PIN. Access denied.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close(); // Закриваємо головне вікно при неуспішному вході
                }
            }
            else
            {
                Close(); // Закриваємо головне вікно при скасуванні авторизації
            }
        }

        private void InitializeBank()
        {
            bank = new Bank("MyBank");

            // Додавання клієнтів
            bank.AddAccount(new Account("1234567890", "John Doe", 1234, 5000m));
            bank.AddAccount(new Account("9876543210", "Jane Smith", 5678, 10000m));

            // Додавання банкоматів
            bank.AddATM(new AutomatedTellerMachine("ATM-001", "123 Main Street", 20000m));
        }

        private void Withdrawal_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount == null) return;

            AmountInputWindow inputWindow = new AmountInputWindow("Enter the amount to withdraw:");
            if (inputWindow.ShowDialog() == true)
            {
                decimal amount = inputWindow.Amount;
                if (bank.Withdraw(currentAccount.CardNumber, amount))
                {
                    InfoScreen.Text = $"Withdrawal successful! Amount: ${amount}\nNew Balance: ${currentAccount.Balance}";
                }
                else
                {
                    InfoScreen.Text = "Withdrawal failed! Insufficient funds.";
                }
            }
        }

        private void Deposit_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount == null) return;

            AmountInputWindow inputWindow = new AmountInputWindow("Enter the amount to deposit:");
            if (inputWindow.ShowDialog() == true)
            {
                decimal amount = inputWindow.Amount;
                if (bank.Deposit(currentAccount.CardNumber, amount))
                {
                    InfoScreen.Text = $"Deposit successful! Amount: ${amount}\nNew Balance: ${currentAccount.Balance}";
                }
                else
                {
                    InfoScreen.Text = "Deposit failed!";
                }
            }
        }

        private void Balance_Click(object sender, RoutedEventArgs e)
        {
            if (currentAccount == null) return;

            InfoScreen.Text = $"Current Balance: ${currentAccount.Balance}";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thank you for using our ATM. Goodbye!", "Exit", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
