using System.Windows;

namespace ATM_UI_App
{
    public partial class LoginWindow : Window
    {
        public string CardNumber { get; private set; }
        public int Pin { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            CardNumber = CardNumberTextBox.Text;

            // Валідація PIN
            if (int.TryParse(PinPasswordBox.Password, out int pin))
            {
                Pin = pin;
                this.DialogResult = true; // Успішний логін
            }
            else
            {
                MessageBox.Show("Invalid PIN. Please enter a numeric value.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false; // Скасування входу
        }
    }
}
