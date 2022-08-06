
namespace BankAPI.Data.BankModels
{
    public class AccountsModel
    {
        public AccountsModel()
        {

        }

        public int Id { get; set; }
        public int AccountType { get; set; }
        public int? ClientId { get; set; }
        public decimal Balence { get; set; }
        public DateTime RegDate { get; set; }
    }
}
