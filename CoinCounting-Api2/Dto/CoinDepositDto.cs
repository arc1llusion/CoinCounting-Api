namespace CoinCounting_Api
{
    public class CoinDepositDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int Pennies { get; set; } = 0;
        public int Nickels { get; set; } = 0;
        public int Dimes { get; set; } = 0;
        public int Quarters { get; set; } = 0;
    }
}
