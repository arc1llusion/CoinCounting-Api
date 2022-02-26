namespace CoinCounting.Data
{
    public class User : EntityWithId
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        public ICollection<CoinDeposit> CoinDeposits { get; set; } = new HashSet<CoinDeposit>();
    }
}