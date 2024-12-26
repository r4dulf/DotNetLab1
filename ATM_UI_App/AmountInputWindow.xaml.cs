using System;
using System.Windows;

namespace ATM_UI_App
{
    public partial class AmountInputWindow : Window
    {
        public decimal Amount { get; private set; }

        public AmountInputWindow(string prompt)
        {
            InitializeComponent();
            PromptText.Text = prompt;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(AmountTextBox.Text, out decimal amount) && amount > 0)
            {
                Amount = amount;
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Please enter a valid positive amount.", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
