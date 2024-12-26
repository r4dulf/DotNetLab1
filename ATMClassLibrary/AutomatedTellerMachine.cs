
namespace ATMClassLibrary.Entities
{
    public class AutomatedTellerMachine
    {
        public string Id { get; private set; }
        public string Address { get; private set; }
        public decimal CashAvailable { get; set; }

        public AutomatedTellerMachine(string id, string address, decimal cashAvailable)
        {
            Id = id;
            Address = address;
            CashAvailable = cashAvailable;
        }
    }

    
}
