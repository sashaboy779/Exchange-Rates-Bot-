namespace ExchangeRateApi.Models.PrivatBankApi
{
    public class ExchangeRate
    {
        public string Currency { get; set; }
        public double SaleRateNb { get; set; }
        public double PurchaseRateNb { get; set; }
        public double SaleRate { get; set; }
        public double PurchaseRate { get; set; }
    }
}